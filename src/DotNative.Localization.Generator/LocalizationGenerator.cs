using System;
using System.Linq;
using System.Text;
using DotNative.Localization.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
namespace DotNative.Localization.Generator;

[Generator(LanguageNames.CSharp)]
public sealed class LocalizationGenerator : IIncrementalGenerator
{
    private static readonly DiagnosticDescriptor Invalid = new DiagnosticDescriptor("L10N001", "Invalid localization input", "{0}: [{1}] {2}", "Localization", DiagnosticSeverity.Error, true);
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var files = context.AdditionalTextsProvider.Where(f => f.Path.EndsWith(".arb", StringComparison.OrdinalIgnoreCase) || System.IO.Path.GetFileName(f.Path) == "l10n.json")
            .Select((f, ct) => new Input(f.Path, f.GetText(ct)?.ToString() ?? "")).Collect();
        context.RegisterSourceOutput(files, (output, inputs) =>
        {
            var configs = inputs.Where(i => System.IO.Path.GetFileName(i.Path) == "l10n.json").GroupBy(i => i.Path).Select(g => g.First()).ToArray();
            if (inputs.Length == 0) return;
            if (configs.Length != 1) { output.ReportDiagnostic(Diagnostic.Create(Invalid, Location.None, "l10n.json", "configuration", "Exactly one l10n.json must be supplied as an AdditionalFile.")); return; }
            var result = ArbCompiler.Compile(configs[0], inputs);
            foreach (var error in result.Problems)
            {
                var input = inputs.FirstOrDefault(i => i.Path == error.File);
                var source = SourceText.From(input?.Text ?? "", Encoding.UTF8);
                int offset = input?.Text.IndexOf("\"" + error.Key + "\"", StringComparison.Ordinal) ?? 0;
                if (offset < 0) offset = 0;
                var span = new TextSpan(offset, 0);
                output.ReportDiagnostic(Diagnostic.Create(Invalid, Location.Create(error.File, span, source.Lines.GetLinePositionSpan(span)), error.File, error.Key, error.Message));
            }
            if (result.Problems.Count == 0) output.AddSource("Localizations.g.cs", SourceText.From(result.Source, Encoding.UTF8));
        });
    }
}
