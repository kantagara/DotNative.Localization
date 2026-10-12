using System;
using System.IO;
using System.Linq;
using DotNative.Localization.Compiler;
if (args.Length != 3 || args[0] != "--report") { Console.Error.WriteLine("Usage: DotNative.Localization.Tool --report l10n.json additional-file-list.txt"); return 2; }
var config = new Input(Path.GetFullPath(args[1]), File.ReadAllText(args[1]));
var inputs = File.ReadAllLines(args[2]).Where(p => p.EndsWith(".arb", StringComparison.OrdinalIgnoreCase)).Select(p => new Input(Path.GetFullPath(p), File.ReadAllText(p))).ToArray();
var result = ArbCompiler.Compile(config, inputs);
foreach (var error in result.Problems) Console.Error.WriteLine(error.File + ": error L10N001: [" + error.Key + "] " + error.Message);
if (result.Problems.Count > 0) return 1;
if (result.ReportPath != null)
{
    if (inputs.Any(i => string.Equals(i.Path, result.ReportPath, StringComparison.OrdinalIgnoreCase)) || string.Equals(config.Path, result.ReportPath, StringComparison.OrdinalIgnoreCase)) { Console.Error.WriteLine("Report path must not overwrite an input file."); return 1; }
    Directory.CreateDirectory(Path.GetDirectoryName(result.ReportPath)!);
    if (!File.Exists(result.ReportPath) || File.ReadAllText(result.ReportPath) != result.Report) File.WriteAllText(result.ReportPath, result.Report);
}
return 0;
