using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
namespace DotNative.Localization.Compiler;

internal sealed class Parser
{
    private readonly string text, key;
    private readonly bool escaping, relaxed;
    private readonly HashSet<string> declared;
    private int pos;
    public readonly List<string> Names = new List<string>();
    public readonly HashSet<string> Plurals = new HashSet<string>();
    public readonly HashSet<string> Dates = new HashSet<string>();
    public readonly HashSet<string> Interpolations = new HashSet<string>();
    public readonly HashSet<string> Selects = new HashSet<string>();
    public Parser(string text, string key, bool escaping, bool relaxed = false, IEnumerable<string>? declared = null) { this.text = text; this.key = key; this.escaping = escaping; this.relaxed = relaxed; this.declared = new HashSet<string>(declared ?? Array.Empty<string>()); }
    private void Fail(string message) => throw new Failure(key, message + " at ICU offset " + pos + ".");
    private void Space() { while (pos < text.Length && char.IsWhiteSpace(text[pos])) pos++; }
    private void Expect(char c) { Space(); if (pos >= text.Length || text[pos] != c) Fail("Expected '" + c + "'"); pos++; }
    private string Token() { Space(); int start = pos; while (pos < text.Length && !char.IsWhiteSpace(text[pos]) && text[pos] != '{' && text[pos] != '}' && text[pos] != ',') pos++; if (start == pos) Fail("Expected token"); return text.Substring(start, pos - start); }
    public List<Part> Parse() { var value = Message(false, null); if (pos != text.Length) Fail("Unexpected closing brace"); return value; }
    private List<Part> Message(bool nested, string? plural)
    {
        var parts = new List<Part>(); var literal = new StringBuilder();
        Action flush = () => { if (literal.Length > 0) { parts.Add(new Part { Value = literal.ToString() }); literal.Clear(); } };
        while (pos < text.Length)
        {
            char c = text[pos];
            if (c == '}') { if (!nested && relaxed) { literal.Append(c); pos++; continue; } if (!nested) Fail("Unexpected closing brace"); break; }
            if (c == '\'' && escaping)
            {
                pos++;
                if (pos < text.Length && text[pos] == '\'') { literal.Append('\''); pos++; continue; }
                bool closed = false;
                while (pos < text.Length) { c = text[pos++]; if (c == '\'') { if (pos < text.Length && text[pos] == '\'') { literal.Append('\''); pos++; } else { closed = true; break; } } else literal.Append(c); }
                if (!closed) Fail("Unterminated apostrophe escape");
                continue;
            }
            if (c != '{') { literal.Append(c); pos++; continue; }
            if (relaxed)
            {
                var match = Regex.Match(text.Substring(pos + 1), @"^\s*([A-Za-z_][A-Za-z0-9_]*)");
                if (!match.Success || !declared.Contains(match.Groups[1].Value)) { literal.Append(c); pos++; continue; }
            }
            flush(); pos++; string name = Token();
            if (!Regex.IsMatch(name, @"^[A-Za-z_][A-Za-z0-9_]*$")) Fail("Invalid placeholder name '" + name + "'");
            if (!Names.Contains(name)) Names.Add(name);
            Space();
            if (pos < text.Length && text[pos] == '}') { pos++; Interpolations.Add(name); parts.Add(new Part { Kind = "arg", Value = name }); continue; }
            Expect(','); string kind = Token();
            if (kind == "date" || kind == "time")
            {
                Expect(','); Space(); if (pos + 1 >= text.Length || text[pos] != ':' || text[pos + 1] != ':') Fail("Date/time argument requires ::skeleton"); pos += 2;
                string format = Token(); if (!ArbCompiler.DateFormats.Contains(format)) Fail("Unsupported inline date/time format " + format); Expect('}'); Dates.Add(name); parts.Add(new Part { Kind = "date", Value = name, Format = format }); continue;
            }
            if (kind != "plural" && kind != "select") Fail("Unsupported ICU argument '" + kind + "' (only plural and select are supported)");
            if (kind == "plural") Plurals.Add(name); else Selects.Add(name);
            Expect(','); var part = new Part { Kind = kind, Value = name }; var seen = new HashSet<string>();
            while (true)
            {
                Space(); if (pos >= text.Length) Fail("Unclosed " + kind);
                if (text[pos] == '}') { pos++; break; }
                string branch = Token();
                if (kind == "plural" && branch == "=") branch += Token();
                if (kind == "plural")
                {
                    if (branch.StartsWith("=", StringComparison.Ordinal))
                    {
                        if (branch != "=0" && branch != "=1" && branch != "=2") Fail("Flutter plural cases support only =0, =1 and =2.");
                        branch = branch == "=0" ? "zero" : branch == "=1" ? "one" : "two";
                    }
                    else if (branch != "zero" && branch != "one" && branch != "two" && branch != "few" && branch != "many" && branch != "other") Fail("Invalid plural category '" + branch + "'");
                }
                seen.Add(branch);
                if (kind == "select" && (!Regex.IsMatch(branch, @"^(?:[0-9]+|[A-Za-z_|][A-Za-z0-9_|]*)$") || branch == "plural" || branch == "select" || branch == "date" || branch == "time")) Fail("Invalid select case '" + branch + "'");
                Expect('{'); var body = Message(true, kind == "plural" ? name : plural); Expect('}');
                part.Cases.Add(new KeyValuePair<string, List<Part>>(branch, body));
            }
            if (!seen.Contains("other")) Fail(kind + " requires an 'other' branch");
            parts.Add(part);
        }
        flush(); return parts;
    }
}
