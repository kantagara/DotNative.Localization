using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DotNative.Localization;
using DotNative.Localization.Compiler;
using DotNative.Localization.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;
int passed = 0;
void Check(bool value, string message) { if (!value) throw new Exception(message); passed++; }
string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../"));
var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator).Select(p => MetadataReference.CreateFromFile(p)).ToList();
references.Add(MetadataReference.CreateFromFile(typeof(ArbKeyAttribute).Assembly.Location));
CSharpCompilation Compilation(string source) => CSharpCompilation.Create("Generated_" + Guid.NewGuid().ToString("N"), new[] { CSharpSyntaxTree.ParseText(source) }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
Result Compile(string arb, string options = "", string? translation = null) => ArbCompiler.Compile(new Input("/test/l10n.json", "{\"arb-dir\":\"arb\",\"namespace\":\"Tests\"" + options + "}"), new[] { new Input("/test/arb/app_en.arb", arb) }.Concat(translation == null ? Array.Empty<Input>() : new[] { new Input("/test/arb/app_sr.arb", translation) }));
object Load(Result result, string locale = "en")
{
    Check(result.Problems.Count == 0, string.Join(";", result.Problems));
    using var stream = new MemoryStream(); var emit = Compilation(result.Source).Emit(stream);
    Check(emit.Success, string.Join(";", emit.Diagnostics));
    var assembly = System.Reflection.Assembly.Load(stream.ToArray());
    return assembly.GetType("Tests.AppLocalizations")!.GetMethod("ForLocale", new[] { typeof(string) })!.Invoke(null, new object[] { locale })!;
}
string Invoke(object instance, string member, params object[] args) => (string)instance.GetType().GetMethod(member)!.Invoke(instance, args)!;
var exact = Load(Compile("{\"m\":\"{n, plural, =1{first} one{last} other{# literal}}\"}"));
Check(Invoke(exact, "M", new L10nNumber(1L)) == "last", "last merged exact/category case wins");
Check(Invoke(exact, "M", new L10nNumber(2L)) == "# literal", "pound is literal");
var implicitArgs = Compile("{\"m\":\"{z} {a}\"}"); Check(implicitArgs.Source.Contains("M(object @a, object @z)", StringComparison.Ordinal), "inferred alphabetical ordering");
var ordered = Compile("{\"m\":\"{z} {a}\",\"@m\":{\"placeholders\":{\"a\":{\"type\":\"int\"},\"z\":{\"type\":\"String\"}}}}"); Check(ordered.Source.Contains("M(long @a, string @z)", StringComparison.Ordinal), "metadata ordering");
Check(Invoke(Load(ordered), "M", 7L, "B") == "B 7", "ordered invocation");
var select = Load(Compile("{\"m\":\"{g, select, male{Male} other{Other}}\"}")); Check(Invoke(select, "M", "Enum.male") == "Male", "Intl select suffix");
var unused = Load(Compile("{\"m\":\"Ready\",\"@m\":{\"placeholders\":{\"unused\":{\"type\":\"int\"}}}}")); Check(Invoke(unused, "M", 2L) == "Ready", "unused declared argument retained");
var escaped = Load(Compile("{\"m\":\"'{literal}' 'it''s'\"}", ",\"use-escaping\":true")); Check((string)escaped.GetType().GetProperty("M")!.GetValue(escaped)! == "{literal} it's", "escaping");
var translated = Load(Compile("{\"m\":\"{name}\",\"@m\":{\"placeholders\":{\"name\":{\"type\":\"String\"}}}}", translation: "{\"m\":\"Hello\"}"), "sr"); Check(Invoke(translated, "M", "ignored") == "Hello", "translation may omit argument");
var eager = Load(Compile("{\"m\":\"{g, select, a{{n, plural, other{X}}} other{Y}}\"}"));
bool threw = false; try { Invoke(eager, "M", "other", new L10nNumber(double.NaN)); } catch (System.Reflection.TargetInvocationException) { threw = true; }
Check(threw, "nested unselected plural evaluates eagerly like Flutter");
Check(Formatting.Raw(new L10nNumber(1L)) == "1" && Formatting.Raw(new L10nNumber(1d)) == "1.0", "Dart num retains int/double");
Check(Formatting.Number(9007199254740993L, "en", "decimalPattern") == "9,007,199,254,740,993", "int64 precision");
GeneratorDriver driver = CSharpGeneratorDriver.Create(new[] { new LocalizationGenerator().AsSourceGenerator() }, new AdditionalText[] { new MemoryFile("/test/l10n.json", "{\"arb-dir\":\"arb\"}"), new MemoryFile("/test/arb/app_en.arb", "{\"broken\":\"{\"}") });
driver = driver.RunGenerators(CSharpCompilation.Create("bad"));
Check(driver.GetRunResult().Diagnostics.Any(d => d.Id == "L10N001" && d.Location.GetLineSpan().Path.EndsWith("app_en.arb", StringComparison.Ordinal)), "source-located generator diagnostic");
// Compare accepted/rejected input against actual Flutter generation plus Dart analysis.
var cases = JArray.Parse(File.ReadAllText(Path.Combine(root, "fixtures/validation/cases.json")));
var oracle = JArray.Parse(File.ReadAllText(Path.Combine(root, "fixtures/results/validation-flutter.json")));
var rows = new JArray(); int differences = 0;
foreach (JObject test in cases)
{
    string name = (string)test["name"]!;
    var config = new JObject { ["arb-dir"] = "arb", ["namespace"] = "Tests" }; config.Merge(test["config"]);
    var inputs = ((JObject)test["files"]!).Properties().Select(p => new Input("/test/arb/" + p.Name, (string)p.Value!));
    var compiled = ArbCompiler.Compile(new Input("/test/l10n.json", config.ToString()), inputs);
    var diagnostics = compiled.Problems.Count == 0 ? Compilation(compiled.Source).GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error).Select(d => d.ToString()).ToArray() : Array.Empty<string>();
    bool accepted = compiled.Problems.Count == 0 && diagnostics.Length == 0;
    bool flutter = (bool)oracle.Single(r => (string)r["name"]! == name)["accepted"]!;
    rows.Add(new JObject { ["name"] = name, ["flutter"] = flutter, ["csharp"] = accepted, ["diagnostics"] = new JArray(compiled.Problems.Select(p => p.ToString()).Concat(diagnostics)) });
    if (accepted != flutter) { Console.Error.WriteLine(name + ": Flutter=" + flutter + ", C#=" + accepted + " " + string.Join(";", compiled.Problems)); differences++; } else passed++;
}
File.WriteAllText(Path.Combine(root, "fixtures/results/validation-comparison.json"), rows.ToString());
Check(differences == 0, "Validation parity: " + differences + " differences");
Console.WriteLine($"Compiler: {passed} assertions passed, including {cases.Count} Flutter acceptance comparisons.");
sealed class MemoryFile : AdditionalText
{
    private readonly string text; public override string Path { get; }
    public MemoryFile(string path, string text) { Path = path; this.text = text; }
    public override SourceText GetText(System.Threading.CancellationToken cancellationToken = default) => SourceText.From(text);
}
