using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace DotNative.Localization.Compiler;

public sealed class Input
{
    public string Path { get; }
    public string Text { get; }
    public Input(string path, string text) { Path = path; Text = text; }
}
public sealed class Problem
{
    public string File { get; }
    public string Key { get; }
    public string Message { get; }
    public Problem(string file, string key, string message) { File = file; Key = key; Message = message; }
    public override string ToString() => File + ": [" + Key + "] " + Message;
}
public sealed class Result
{
    public string Source { get; internal set; } = "";
    public string Report { get; internal set; } = "{}";
    public string? ReportPath { get; internal set; }
    public List<Problem> Problems { get; } = new List<Problem>();
}
internal sealed class Failure : Exception
{
    public string Key { get; }
    public Failure(string key, string message) : base(message) { Key = key; }
}
internal sealed class Part
{
    public string Kind = "text", Value = "", Format = "";
    public List<KeyValuePair<string, List<Part>>> Cases = new List<KeyValuePair<string, List<Part>>>();
}
internal sealed class Placeholder
{
    public string Name = "", Type = "", Format = "", Description = "";
    public bool CustomDate, FormatDate;
    public JObject Options = new JObject();
}
internal sealed class Message
{
    public string Key = "", Member = "";
    public List<Part> Parts = new List<Part>();
    public List<Placeholder> Parameters = new List<Placeholder>();
}
internal sealed class Bundle
{
    public string Path = "", Locale = "";
    public JObject Json = new JObject();
    public Dictionary<string, Message> Messages = new Dictionary<string, Message>(StringComparer.Ordinal);
}
