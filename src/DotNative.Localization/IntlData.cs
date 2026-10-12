using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
namespace DotNative.Localization;

internal sealed record DateData(Dictionary<string, string[]> Symbols, Dictionary<string, string> Patterns, string Zero);
internal sealed record NumberData(string[] Symbols, SortedDictionary<int, Dictionary<string, string>> Short, SortedDictionary<int, Dictionary<string, string>> Long, SortedDictionary<int, Dictionary<string, string>> Currency)
{
    public string Locale => Symbols[0]; public string Decimal => Symbols[1]; public string Group => Symbols[2];
    public string Percent => Symbols[3]; public string Zero => Symbols[4]; public string Plus => Symbols[5]; public string Minus => Symbols[6];
    public string Exponent => Symbols[7]; public string Permille => Symbols[8]; public string Infinity => Symbols[9]; public string NaN => Symbols[10];
    public string DecimalPattern => Symbols[11]; public string ScientificPattern => Symbols[12]; public string PercentPattern => Symbols[13];
    public string CurrencyPattern => Symbols[14]; public string DefaultCurrency => Symbols[15];
}
internal static partial class IntlData
{
    private static readonly ConcurrentDictionary<string, NumberData> Numbers = new();
    private static readonly ConcurrentDictionary<string, DateData> Dates = new();
    internal static NumberData Number(string locale) => Numbers.GetOrAdd(locale, tag => Resolve(tag, LoadNumberData));
    internal static DateData Date(string locale) => Dates.GetOrAdd(locale, tag => Resolve(tag, LoadDateData));
    private static T Resolve<T>(string locale, Func<string, T?> load) where T : class
    {
        foreach (var candidate in IntlLocale.Candidates(locale)) if (load(candidate) is T data) return data;
        throw new ArgumentException("Invalid intl locale: " + locale);
    }
}
internal static class IntlLocale
{
    private static string Deprecated(string s) => s switch { "iw" => "he", "he" => "iw", "fil" => "tl", "tl" => "fil", "id" => "in", "in" => "id", _ => s };
    internal static IEnumerable<string> Candidates(string locale)
    {
        yield return locale;
        string canonical = locale == "C" ? "en_ISO" : locale.Replace('-', '_');
        int split = canonical.IndexOf('_');
        if (split >= 0 && canonical.Length - split - 1 <= 3) canonical = canonical.Substring(0, split) + canonical.Substring(split).ToUpperInvariant();
        yield return canonical;
        string shortName = locale.Split('-', '_')[0];
        yield return shortName; yield return Deprecated(locale); yield return Deprecated(shortName); yield return Deprecated(canonical); yield return "fallback";
    }
}
