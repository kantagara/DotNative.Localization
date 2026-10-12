using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace DotNative.Localization.Compiler;

public static class ArbCompiler
{
    private static readonly HashSet<string> ConfigKeys = new HashSet<string> { "arb-dir", "template-arb-file", "output-class", "namespace", "nullable-getter", "header", "preferred-supported-locales", "untranslated-messages-file", "use-escaping", "relax-syntax", "required-resource-attributes", "use-named-parameters", "suppress-warnings" };
    internal static readonly HashSet<string> DateFormats = new HashSet<string>("d E EEEE LLL LLLL M Md MEd MMM MMMd MMMEd MMMM MMMMd MMMMEEEEd y yM yMd yMEd yMMM yMMMd yMMMEd yMMMM yMMMMd yMMMMEEEEd QQQ QQQQ yQQQ yQQQQ H Hm Hms j jm jms jmv jmz jv jz m ms s".Split(' '));
    private static readonly HashSet<string> NumberFormats = new HashSet<string>("decimalPattern decimalPatternDigits currency simpleCurrency percentPattern decimalPercentPattern scientificPattern compact compactLong compactCurrency compactSimpleCurrency".Split(' '));
    public static Result Compile(Input config, IEnumerable<Input> inputs)
    {
        var result = new Result(); string file = config.Path;
        try
        {
            var cfg = Read(config.Text);
            foreach (var p in cfg.Properties()) if (!ConfigKeys.Contains(p.Name)) throw new Failure("configuration", "Unknown or unsupported option '" + p.Name + "'.");
            string root = Path.GetDirectoryName(Path.GetFullPath(config.Path))!;
            string dir = Path.GetFullPath(Path.Combine(root, Text(cfg, "arb-dir", "l10n")));
            string templateName = Text(cfg, "template-arb-file", "app_en.arb");
            string className = Text(cfg, "output-class", "AppLocalizations");
            string ns = Text(cfg, "namespace", "Localizations");
            if (!Identifier(className) || ns.Split('.').Any(x => !Identifier(x))) throw new Failure("configuration", "output-class and namespace must be C# identifiers (keywords may be escaped with @).");
            bool escaping = Boolean(cfg, "use-escaping", false), nullable = Boolean(cfg, "nullable-getter", false);
            string header = Text(cfg, "header", "");
            if (cfg["untranslated-messages-file"] != null) result.ReportPath = Path.GetFullPath(Path.Combine(root, Text(cfg, "untranslated-messages-file", "")));
            var bundles = new List<Bundle>();
            foreach (var input in inputs.OrderBy(x => x.Path, StringComparer.Ordinal))
            {
                if (!input.Path.EndsWith(".arb", StringComparison.OrdinalIgnoreCase) || Path.GetDirectoryName(Path.GetFullPath(input.Path)) != dir) continue;
                file = input.Path;
                var json = string.IsNullOrWhiteSpace(input.Text) ? new JObject() : Read(input.Text);
                string locale = Text(json, "@@locale", "");
                if (locale.Length == 0) { var match = Regex.Match(Path.GetFileNameWithoutExtension(input.Path), @"(?:^|_)([a-z]{2,3}(?:_[A-Z][a-z]{3})?(?:_(?:[A-Z]{2}|[0-9]{3}))?)$"); if (!match.Success) throw new Failure("@@locale", "Cannot infer locale from filename."); locale = match.Groups[1].Value; }
                var filenameLocale = Regex.Match(Path.GetFileNameWithoutExtension(input.Path), @"(?:^|_)([a-z]{2,3}(?:_[A-Z][a-z]{3})?(?:_(?:[A-Z]{2}|[0-9]{3}))?)$");
                if (json["@@locale"] != null && filenameLocale.Success && Normalize(filenameLocale.Groups[1].Value) != Normalize(locale)) throw new Failure("@@locale", "Locale in @@locale conflicts with filename.");
                locale = Normalize(locale);
                if (!Regex.IsMatch(locale, @"^[a-z]{2,3}(?:-[A-Z][a-z]{3})?(?:-(?:[A-Z]{2}|[0-9]{3}))?$")) throw new Failure("@@locale", "Expected language[-Script][-COUNTRY], got '" + locale + "'.");
                if (bundles.Any(b => b.Locale == locale)) throw new Failure("@@locale", "Duplicate locale '" + locale + "'.");
                bundles.Add(new Bundle { Path = input.Path, Locale = locale, Json = json });
            }
            var template = bundles.SingleOrDefault(b => Path.GetFileName(b.Path) == templateName);
            if (template == null) throw new Failure("configuration", "Template '" + templateName + "' was not supplied as an AdditionalFile under arb-dir.");
            foreach (var language in bundles.Select(b => b.Locale.Split('-')[0]).Distinct())
                if (!bundles.Any(b => b.Locale == language)) throw new Failure("@@locale", "Missing base-language ARB for '" + language + "'.");
            bool relaxed = Boolean(cfg, "relax-syntax", false), required = Boolean(cfg, "required-resource-attributes", false);
            foreach (var property in template.Json.Properties().Where(p => !p.Name.StartsWith("@", StringComparison.Ordinal)))
            {
                file = template.Path; string key = property.Name;
                try
                {
                    if (!Regex.IsMatch(key, @"^[a-z$][a-zA-Z0-9_$]*$") || DartKeywords.Contains(key)) throw new Failure(key, "Invalid Dart resource identifier '" + key + "'. Flutter cannot generate a valid member for this key.");
                    if (property.Value.Type != JTokenType.String) throw new Failure(key, "Template message must be a JSON string.");
                    var templateSpecs = Specs(template.Json, key, required);
                    var parsers = new Dictionary<Bundle, Parser>(); var messages = new Dictionary<Bundle, Message>();
                    var specs = new Dictionary<Bundle, JObject>();
                    foreach (var bundle in bundles)
                    {
                        file = bundle.Path; specs[bundle] = Specs(bundle.Json, key, false);
                        var value = bundle.Json[key];
                        if (value == null || value.Type == JTokenType.Null) continue;
                        if (value.Type != JTokenType.String) throw new Failure(key, "Message must be a JSON string.");
                        var parser = new Parser((string)value!, key, escaping, relaxed, templateSpecs.Properties().Select(x => x.Name));
                        parsers[bundle] = parser;
                        messages[bundle] = new Message { Key = key, Parts = parser.Parse() };
                    }
                    var names = templateSpecs.Properties().Select(x => x.Name).ToList();
                    names.AddRange(parsers.Values.SelectMany(x => x.Names).Distinct().Where(x => !names.Contains(x) && !specs.Values.Any(o => o[x] != null)).OrderBy(x => x, StringComparer.Ordinal));
                    // Inference walks all translations. Locale-only declared placeholders do not add template parameters.
                    var global = new Parser("", key, escaping);
                    foreach (var parser in parsers.Values)
                    {
                        global.Plurals.UnionWith(parser.Plurals); global.Selects.UnionWith(parser.Selects); global.Dates.UnionWith(parser.Dates); global.Interpolations.UnionWith(parser.Interpolations);
                    }
                    var api = new List<Placeholder>();
                    foreach (var param in names)
                    {
                        if (!Regex.IsMatch(param, @"^[A-Za-z_][A-Za-z0-9_]*$") || DartKeywords.Contains(param)) throw new Failure(key, "Invalid placeholder identifier '" + param + "'.");
                        api.Add(ParseParameter(param, templateSpecs[param] as JObject ?? new JObject(), key, global));
                    }
                    foreach (var bundle in messages.Keys)
                    {
                        file = bundle.Path; var message = messages[bundle];
                        foreach (var param in api)
                        {
                            var selected = param;
                            if (bundle != template && specs[bundle][param.Name] is JObject local)
                            {
                                selected = ParseParameter(param.Name, local, key, parsers[bundle]);
                                if (selected.Type != param.Type) throw new Failure(key, "Translation changes type of '" + param.Name + "' from " + param.Type + " to " + selected.Type + ".");
                            }
                            message.Parameters.Add(selected);
                        }
                        foreach (string param in parsers[bundle].Names) if (!names.Contains(param)) throw new Failure(key, "Locale-only declared placeholder '" + param + "' is not a template parameter.");
                        bundle.Messages[key] = message;
                    }
                }
                catch (Failure ex) { result.Problems.Add(new Problem(file, key, ex.Message)); }
            }
            if (result.Problems.Count > 0) return result;
            file = config.Path;
            if (new[] { "ForLocale", "SupportedLocales", "Locale" }.Contains(className.TrimStart('@')) || bundles.Any(b => "L_" + b.Locale.Replace('-', '_') == className.TrimStart('@'))) throw new Failure("output-class", "Class name collides with generated infrastructure.");
            var reserved = new HashSet<string> { className.TrimStart('@'), "ForLocale", "SupportedLocales", "Locale", "Equals", "GetHashCode", "ToString", "GetType", "MemberwiseClone", "Finalize" };
            foreach (var message in template.Messages.Values) message.Member = Pascal(message.Key);
            foreach (var group in template.Messages.Values.GroupBy(m => m.Member).ToArray())
                if (group.Count() > 1 || reserved.Contains(group.Key)) foreach (var m in group) m.Member += "__" + Hex(m.Key);
            var locales = bundles.OrderBy(b => b.Locale, StringComparer.Ordinal).Select(b => b.Locale).ToList();
            if (cfg["preferred-supported-locales"] != null)
            {
                if (!(cfg["preferred-supported-locales"] is JArray preferred) || preferred.Any(p => p.Type != JTokenType.String)) throw new Failure("configuration", "preferred-supported-locales must be an array of strings.");
                var pref = preferred.Select(p => Normalize((string)p!)).Distinct().ToList();
                if (pref.Any(p => !locales.Contains(p))) throw new Failure("configuration", "A preferred locale has no ARB file.");
                locales = pref.Concat(locales.Where(p => !pref.Contains(p))).ToList();
            }
            var report = new JObject();
            foreach (var b in bundles.Where(b => b != template)) { var missing = template.Messages.Keys.Where(k => !b.Messages.ContainsKey(k)).ToArray(); if (missing.Length > 0) report[b.Locale.Replace('-', '_')] = new JArray(missing); }
            result.Report = report.ToString(Formatting.Indented) + "\n";
            result.Source = Emit(template, bundles, locales, className, ns, nullable, header);
        }
        catch (Failure ex) { result.Problems.Add(new Problem(file, ex.Key, ex.Message)); }
        catch (JsonException ex) { result.Problems.Add(new Problem(file, "JSON", ex.Message)); }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException || ex is OverflowException) { result.Problems.Add(new Problem(file, "configuration", ex.Message)); }
        return result;
    }
    private static JObject Read(string json) => JObject.Parse(json, new JsonLoadSettings { DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace });
    private static string Text(JObject obj, string key, string fallback) { var t = obj[key]; if (t == null || t.Type == JTokenType.Null) return fallback; if (t.Type != JTokenType.String) throw new Failure(key, "Expected a string."); return (string)t!; }
    private static bool Boolean(JObject obj, string key, bool fallback) { var t = obj[key]; if (t == null || t.Type == JTokenType.Null) return fallback; if (t.Type != JTokenType.Boolean) throw new Failure(key, "Expected a boolean."); return (bool)t; }
    private static bool Identifier(string s) => Regex.IsMatch(s, @"^@?[A-Za-z_][A-Za-z0-9_]*$") && (s.StartsWith("@", StringComparison.Ordinal) || !Keywords.Contains(s));
    private static readonly HashSet<string> Keywords = new HashSet<string>("abstract as base bool break byte case catch char checked class const continue decimal default delegate do double else enum event explicit extern false finally fixed float for foreach goto if implicit in int interface internal is lock long namespace new null object operator out override params private protected public readonly ref return sbyte sealed short sizeof stackalloc static string struct switch this throw true try typeof uint ulong unchecked unsafe ushort using virtual void volatile while record".Split(' '));
    public static string Normalize(string locale) { var parts = locale.Replace('_', '-').Split('-'); for (int i = 0; i < parts.Length; i++) parts[i] = i == 0 ? parts[i].ToLowerInvariant() : parts[i].Length == 4 ? char.ToUpperInvariant(parts[i][0]) + parts[i].Substring(1).ToLowerInvariant() : parts[i].ToUpperInvariant(); return string.Join("-", parts); }
    private static string Hex(string s) => string.Concat(Encoding.UTF8.GetBytes(s).Select(b => b.ToString("x2", CultureInfo.InvariantCulture)));
    private static string Pascal(string key) { var words = Regex.Split(key, @"[^A-Za-z0-9]+").Where(s => s.Length > 0); string name = string.Concat(words.Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1))); if (name.Length == 0 || char.IsDigit(name[0])) name = "Key" + name; return name; }
    private static readonly HashSet<string> DartKeywords = new HashSet<string>("assert break case catch class const continue default do else enum extends false final finally for if in is new null rethrow return super switch this throw true try var void while with".Split(' '));
    private static JObject Specs(JObject bundle, string key, bool required)
    {
        var value = bundle["@" + key];
        if (value == null || value.Type == JTokenType.Null) { if (required) throw new Failure(key, "Required resource attribute @" + key + " was not found."); return new JObject(); }
        if (!(value is JObject meta)) throw new Failure(key, "Resource metadata must be an object.");
        if (meta["description"] != null && meta["description"]!.Type != JTokenType.String && meta["description"]!.Type != JTokenType.Null) throw new Failure(key, "Description must be a string.");
        var p = meta["placeholders"];
        if (p == null || p.Type == JTokenType.Null) return new JObject();
        if (!(p is JObject specs)) throw new Failure(key, "placeholders must be an object.");
        foreach (var prop in specs.Properties()) if (!(prop.Value is JObject)) throw new Failure(key, "Placeholder '" + prop.Name + "' must be an object.");
        return specs;
    }
    private static Placeholder ParseParameter(string name, JObject spec, string key, Parser parser)
    {
        foreach (string field in new[] { "type", "format", "example" }) if (spec[field] != null && spec[field]!.Type != JTokenType.Null && (spec[field]!.Type != JTokenType.String || (string)spec[field]! == "")) throw new Failure(key, field + " of '" + name + "' must be a nonempty string.");
        var p = new Placeholder { Name = name, Type = Text(spec, "type", parser.Plurals.Contains(name) ? "num" : parser.Selects.Contains(name) ? "String" : parser.Dates.Contains(name) ? "DateTime" : "Object"), Format = Text(spec, "format", "") };
        var custom = spec["isCustomDateFormat"];
        if (custom != null && custom.Type != JTokenType.Null)
        {
            if (custom.Type == JTokenType.Boolean) p.CustomDate = (bool)custom;
            else if (custom.Type == JTokenType.String && ((string)custom! == "true" || (string)custom! == "false")) p.CustomDate = (string)custom! == "true";
            else throw new Failure(key, "isCustomDateFormat must be boolean.");
        }
        if (!new[] { "String", "int", "num", "double", "DateTime", "Object" }.Contains(p.Type)) throw new Failure(key, "Unknown type '" + p.Type + "'.");
        if ((parser.Plurals.Contains(name) ? 1 : 0) + (parser.Selects.Contains(name) ? 1 : 0) + (parser.Dates.Contains(name) ? 1 : 0) > 1) throw new Failure(key, "Placeholder is used as plural/select/datetime in certain languages.");
        if (parser.Plurals.Contains(name) && p.Type != "int" && p.Type != "num") throw new Failure(key, "Plural placeholders must be num or int.");
        if (parser.Selects.Contains(name) && p.Type != "String") throw new Failure(key, "Select placeholders must be String.");
        if (parser.Dates.Contains(name) && p.Type != "DateTime") throw new Failure(key, "Date/time placeholders must be DateTime.");
        p.FormatDate = p.Type == "DateTime" && parser.Interpolations.Contains(name);
        if (p.FormatDate && p.Format.Length == 0) throw new Failure(key, "DateTime interpolation requires format.");
        if (p.FormatDate && !p.CustomDate && p.Format.Split('+').Any(f => !DateFormats.Contains(f))) throw new Failure(key, "Unsupported DateTime format '" + p.Format + "'.");
        if (Numeric(p.Type) && p.Format.Length > 0 && !NumberFormats.Contains(p.Format)) throw new Failure(key, "Unsupported number format '" + p.Format + "'.");
        if (spec["optionalParameters"] != null && spec["optionalParameters"]!.Type != JTokenType.Null && !(spec["optionalParameters"] is JObject)) throw new Failure(key, "optionalParameters must be an object.");
        p.Options = spec["optionalParameters"] as JObject ?? new JObject();
        // gen-l10n forwards options only to named factories. Positional factories ignore them.
        if (!new[] { "currency", "simpleCurrency", "decimalPatternDigits", "decimalPercentPattern", "compact", "compactLong", "compactCurrency", "compactSimpleCurrency" }.Contains(p.Format)) p.Options = new JObject();
        foreach (var opt in p.Options.Properties())
        {
            bool currency = new[] { "currency", "simpleCurrency", "compactCurrency", "compactSimpleCurrency" }.Contains(p.Format);
            bool digits = opt.Name == "decimalDigits" && (currency || p.Format == "decimalPatternDigits" || p.Format == "decimalPercentPattern");
            bool text = (opt.Name == "name" && currency) || (opt.Name == "symbol" && (p.Format == "currency" || p.Format == "compactCurrency")) || (opt.Name == "customPattern" && p.Format == "currency");
            if (!digits && !text) throw new Failure(key, "Unsupported optional parameter '" + opt.Name + "' for '" + p.Format + "'.");
            if (digits && opt.Value.Type != JTokenType.Integer) throw new Failure(key, "decimalDigits must be an integer.");
            if (text && opt.Value.Type != JTokenType.String) throw new Failure(key, opt.Name + " must be a string.");
        }
        return p;
    }
    private static bool Numeric(string type) => type == "int" || type == "num" || type == "double";
    private static string Q(string s) => JsonConvert.ToString(s).Replace("\u0085", "\\u0085").Replace("\u2028", "\\u2028").Replace("\u2029", "\\u2029");
    private static string Type(string type) { switch (type) { case "String": return "string"; case "int": return "long"; case "num": return "global::DotNative.Localization.L10nNumber"; case "double": return "double"; case "DateTime": return "global::System.DateTime"; default: return "object"; } }
    private static string Emit(Bundle template, List<Bundle> bundles, List<string> locales, string name, string ns, bool nullable, string header)
    {
        var s = new StringBuilder("// <auto-generated/>\n#nullable enable\n#pragma warning disable CS0219 // Eager evaluation matches Flutter branch arguments.\n");
        foreach (var line in header.Replace("\r", "").Replace("\u0085", "\n").Replace("\u2028", "\n").Replace("\u2029", "\n").Split('\n')) if (line.Length > 0) s.Append("// ").AppendLine(line);
        s.Append("namespace ").Append(ns).AppendLine(" {");
        s.Append("public abstract class ").Append(name).AppendLine(" {");
        s.Append("private protected ").Append(name).AppendLine("(string locale) { Locale = locale; }");
        s.AppendLine("public string Locale { get; }");
        s.Append("public static global::System.Collections.Generic.IReadOnlyList<string> SupportedLocales { get; } = global::System.Array.AsReadOnly(new string[] {").Append(string.Join(",", locales.Select(Q))).AppendLine("});");
        s.Append("public static ").Append(name).AppendLine(" ForLocale(global::System.Globalization.CultureInfo culture) => ForLocale(culture.Name);");
        s.Append("public static ").Append(name).AppendLine(" ForLocale(string locale) {");
        s.AppendLine("if (locale is null) throw new global::System.ArgumentNullException(nameof(locale));");
        s.AppendLine("var tag = locale.Replace('_','-').ToLowerInvariant();");
        s.AppendLine("switch(tag) {");
        foreach (var b in bundles) s.Append("case ").Append(Q(b.Locale.ToLowerInvariant())).Append(": return new L_").Append(b.Locale.Replace('-', '_')).AppendLine("();");
        s.AppendLine("} var parts=tag.Split('-'); var language=parts[0]; var script=global::System.Array.Find(parts,p=>p.Length==4); var country=global::System.Array.Find(parts,p=>p!=language && (p.Length==2 || p.Length==3));");
        foreach (var variant in bundles.Where(b => b.Locale.Contains("-") && b.Locale.Split('-').Length == 2 && b.Locale.Split('-')[1].Length == 4))
            s.Append("if(language==").Append(Q(variant.Locale.Split('-')[0])).Append(" && script==").Append(Q(variant.Locale.Split('-')[1].ToLowerInvariant())).Append(") return new L_").Append(variant.Locale.Replace('-', '_')).AppendLine("();");
        foreach (var variant in bundles.Where(b => b.Locale.Contains("-") && b.Locale.Split('-').Length == 2 && b.Locale.Split('-')[1].Length != 4))
            s.Append("if(language==").Append(Q(variant.Locale.Split('-')[0])).Append(" && country==").Append(Q(variant.Locale.Split('-')[1].ToLowerInvariant())).Append(") return new L_").Append(variant.Locale.Replace('-', '_')).AppendLine("();");
        s.AppendLine("switch(language) {");
        foreach (var b in bundles.Where(b => !b.Locale.Contains("-"))) s.Append("case ").Append(Q(b.Locale)).Append(": return new L_").Append(b.Locale).AppendLine("();");
        s.AppendLine("default: throw new global::System.ArgumentException(\"Unsupported locale: \"+locale,nameof(locale)); } }");
        foreach (var m in template.Messages.Values)
        {
            s.Append("[global::DotNative.Localization.ArbKey(").Append(Q(m.Key)).AppendLine(")]");
            s.Append("public abstract string").Append("").Append(' ').Append(m.Member);
            if (m.Parameters.Count == 0) s.AppendLine(" { get; }"); else s.Append('(').Append(string.Join(", ", m.Parameters.Select(p => Type(p.Type) + " @" + p.Name))).AppendLine(");");
        }
        foreach (var b in bundles)
        {
            s.Append("private sealed class L_").Append(b.Locale.Replace('-', '_')).Append(" : ").Append(name).AppendLine(" {");
            s.Append("public L_").Append(b.Locale.Replace('-', '_')).Append("() : base(").Append(Q(b.Locale)).AppendLine(") {}");
            foreach (var api in template.Messages.Values)
            {
                bool own = b.Messages.TryGetValue(api.Key, out var m);
                if (!own)
                {
                    var parent = bundles.First(x => x.Locale == b.Locale.Split('-')[0]);
                    m = parent.Messages.TryGetValue(api.Key, out var inherited) ? inherited : api;
                }
                string locale = b.Locale;
                s.Append("[global::DotNative.Localization.ArbKey(").Append(Q(api.Key)).AppendLine(")]");
                s.Append("public override string").Append("").Append(' ').Append(api.Member);
                if (api.Parameters.Count > 0) s.Append('(').Append(string.Join(", ", api.Parameters.Select(p => Type(p.Type) + " @" + p.Name))).Append(')');
                var emitter = new ExpressionEmitter(m!, locale);
                var expression = emitter.Message(m!.Parts);
                if (api.Parameters.Count == 0) s.Append(" { get ");
                s.AppendLine(" {");
                s.Append(emitter.Statements);
                s.Append("return ").Append(expression).AppendLine(";");
                s.AppendLine(api.Parameters.Count == 0 ? "} }" : "}");
            }
            s.AppendLine("}");
        }
        return s.AppendLine("} }").ToString();
    }
    private sealed class ExpressionEmitter
    {
        private readonly Message message; private readonly string locale;
        private int counter;
        public readonly StringBuilder Statements = new StringBuilder();
        private readonly Dictionary<string, string> formatted = new Dictionary<string, string>();
        public ExpressionEmitter(Message message, string locale)
        {
            this.message = message; this.locale = locale.Replace('-', '_');
            foreach (var p in message.Parameters)
            {
                string arg = "@" + p.Name;
                if (p.FormatDate) formatted[p.Name] = Temporary("global::DotNative.Localization.Formatting.Date(" + arg + ", " + Q(this.locale) + ", " + Q(p.Format) + ", " + (p.CustomDate && p.Format.Split('+').Any(f => !DateFormats.Contains(f)) ? "true" : "false") + ")");
                else if (Numeric(p.Type) && p.Format.Length > 0) formatted[p.Name] = Temporary("global::DotNative.Localization.Formatting.Number(" + arg + ", " + Q(this.locale) + ", " + Q(p.Format) + ", " + (p.Options["decimalDigits"]?.ToString() ?? "null") + ", " + Option(p, "name") + ", " + Option(p, "symbol") + ", " + Option(p, "customPattern") + ")");
            }
        }
        private static string Option(Placeholder p, string name) => p.Options[name] == null ? "null" : Q((string)p.Options[name]!);
        private string Temporary(string value) { string name = "__l10n" + counter++; while (message.Parameters.Any(p => p.Name == name)) name = "__l10n" + counter++; Statements.Append("var ").Append(name).Append(" = ").Append(value).AppendLine(";"); return name; }
        public string Message(List<Part> parts) => parts.Count == 0 ? "\"\"" : string.Join(" + ", parts.Select(Part));
        private string Part(Part p)
        {
            if (p.Kind == "text") return Q(p.Value);
            string arg = "@" + p.Value;
            if (p.Kind == "arg") return formatted.TryGetValue(p.Value, out var f) ? f : "global::DotNative.Localization.Formatting.Raw(" + arg + ")";
            if (p.Kind == "date") return Temporary("global::DotNative.Localization.Formatting.Date(" + arg + ", " + Q(locale) + ", " + Q(p.Format) + ")");
            // Flutter emits nested helpers first and eagerly evaluates every branch argument.
            var cases = new Dictionary<string, string>();
            foreach (var branch in (p.Kind == "plural" ? p.Cases.AsEnumerable().Reverse() : p.Cases))
            {
                if (p.Kind == "plural" && cases.ContainsKey(branch.Key)) continue;
                cases[branch.Key] = Temporary(Message(branch.Value));
            }
            string selector;
            if (p.Kind == "plural")
            {
                int mask = cases.Keys.Sum(k => k == "zero" ? 1 : k == "one" ? 2 : k == "two" ? 4 : k == "few" ? 8 : k == "many" ? 16 : 32);
                selector = Temporary("global::DotNative.Localization.PluralRules.Choose(" + Q(locale) + ", " + arg + ", " + mask + ")");
            }
            else
            {
                string exact = string.Join(" || ", cases.Keys.Select(k => arg + " == " + Q(k)));
                selector = Temporary("(" + exact + " ? " + arg + " : global::DotNative.Localization.Formatting.SelectKey(" + arg + "))");
            }
            string expression = cases["other"];
            foreach (var branch in cases.Where(c => c.Key != "other")) expression = "(" + selector + " == " + Q(branch.Key) + " ? " + branch.Value + " : " + expression + ")";
            return Temporary(expression);
        }
    }
}
