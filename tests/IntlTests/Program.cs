using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using DotNative.Localization;
string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../"));
using var data = JsonDocument.Parse(File.ReadAllText(Path.Combine(root, "fixtures/results/intl-oracle.json")));
int total = 0, failed = 0; var failures = new List<object>();
foreach (var row in data.RootElement.EnumerateArray())
{
    string S(string name) => row.GetProperty(name).GetString()!;
    string? Optional(string name) => row.TryGetProperty(name, out var v) && v.ValueKind != JsonValueKind.Null ? v.GetString() : null;
    string? actual = null, error = null; total++;
    try
    {
        string kind = S("kind"), locale = S("locale");
        actual = kind switch
        {
            "date" => Formatting.Date(DateTime.Parse(S("value"), CultureInfo.InvariantCulture), locale, S("format"), row.TryGetProperty("custom", out var custom) && custom.GetBoolean()),
            "integer" => Formatting.Number(long.Parse(S("value"), CultureInfo.InvariantCulture), locale, S("format")),
            "pluralInteger" => PluralRules.Choose(locale, long.Parse(S("value"), CultureInfo.InvariantCulture), 63),
            "plural" => PluralRules.Choose(locale, double.Parse(S("value"), CultureInfo.InvariantCulture), 63),
            _ => Formatting.Number(double.Parse(S("value"), CultureInfo.InvariantCulture), locale, S("format"), row.TryGetProperty("digits", out var digits) && digits.ValueKind != JsonValueKind.Null ? digits.GetInt32() : null, Optional("name"), Optional("symbol"), Optional("customPattern"))
        };
    }
    catch (Exception ex) { error = ex.GetType().Name; }
    bool expectedError = row.TryGetProperty("error", out _);
    if (expectedError ? error == null : error != null || actual != S("expected"))
    {
        failed++; failures.Add(new { input = row.Clone(), actual, error });
    }
}
File.WriteAllText(Path.Combine(root, "artifacts/intl-failures.json"), JsonSerializer.Serialize(failures, new JsonSerializerOptions { WriteIndented = true }));
Console.WriteLine($"Intl differential: {total - failed}/{total} pass; {failed} differences.");
return failed == 0 ? 0 : 1;
