using System;
using System.Globalization;
using System.Text;
namespace DotNative.Localization;

public static class Formatting
{
    public static string Raw(object? value) => value switch
    {
        null => "null",
        bool b => b ? "true" : "false",
        double d => RawDouble(d),
        float f => RawDouble(f),
        DateTime d => d.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + (d.Kind == DateTimeKind.Utc ? "Z" : ""),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture) ?? "null",
        _ => value.ToString() ?? "null"
    };
    public static string RawDouble(double value)
    {
        if (double.IsNaN(value)) return "NaN";
        if (double.IsPositiveInfinity(value)) return "Infinity";
        if (double.IsNegativeInfinity(value)) return "-Infinity";
        string result = value.ToString("R", CultureInfo.InvariantCulture);
        double abs = Math.Abs(value);
        if (abs >= 1e-6 && abs < 1e21 && result.Contains('E'))
        {
            var p = result.Split('E'); int exp = int.Parse(p[1], CultureInfo.InvariantCulture);
            bool negative = p[0][0] == '-'; string digits = p[0].TrimStart('-').Replace(".", "");
            int position = 1 + exp;
            result = (negative ? "-" : "") + (position <= 0 ? "0." + new string('0', -position) + digits : position >= digits.Length ? digits + new string('0', position - digits.Length) : digits.Insert(position, "."));
        }
        if (result.Contains('E'))
        {
            var p = result.Split('E'); int exp = int.Parse(p[1], CultureInfo.InvariantCulture);
            return p[0] + "e" + (exp >= 0 ? "+" : "") + exp.ToString(CultureInfo.InvariantCulture);
        }
        return result.Contains('.') ? result : result + ".0";
    }
    public static string SelectKey(string value) => value.Substring(value.LastIndexOf('.') + 1);
    public static string Number(L10nNumber value, string locale, string format, int? decimalDigits = null, string? name = null, string? symbol = null, string? customPattern = null)
        => IntlNumber.Format(value.Double, IntlData.Number(locale), format, decimalDigits, name, symbol, customPattern, value.IsInteger ? value.Integer : null);
    public static string Number(long value, string locale, string format, int? decimalDigits = null, string? name = null, string? symbol = null, string? customPattern = null)
        => IntlNumber.Format(value, IntlData.Number(locale), format, decimalDigits, name, symbol, customPattern, value);
    public static string Number(double value, string locale, string format, int? decimalDigits = null, string? name = null, string? symbol = null, string? customPattern = null)
        => IntlNumber.Format(value, IntlData.Number(locale), format, decimalDigits, name, symbol, customPattern);
    public static string Date(DateTime value, string locale, string format, bool custom = false)
    {
        var data = IntlData.Date(locale);
        var result = new StringBuilder();
        var formats = custom ? new[] { format } : format.Split('+');
        foreach (string part in formats)
        {
            if (result.Length > 0) result.Append(' ');
            string pattern = data.Patterns.TryGetValue(part, out var found) ? found : part;
            result.Append(DatePattern(value, pattern, data));
        }
        return result.ToString();
    }
    private static string DatePattern(DateTime value, string pattern, DateData data)
    {
        var result = new StringBuilder();
        string Pad(int n, int width) => Digits(n.ToString(CultureInfo.InvariantCulture).PadLeft(width, '0'), data.Zero);
        string Sym(string key, int index) => data.Symbols[key][index];
        for (int i = 0; i < pattern.Length;)
        {
            char c = pattern[i++];
            if (c == '\'')
            {
                var quoted = System.Text.RegularExpressions.Regex.Match(pattern.Substring(i - 1), "^'(?:[^']|'')*'");
                if (!quoted.Success) break;
                var token = quoted.Value;
                result.Append(token == "''" ? "'" : token.Substring(1, token.Length - 2).Replace("''", "'"));
                i += token.Length - 1;
                continue;
            }
            if ("aLcDEGdHhKkMmQsySvzZ".IndexOf(c) < 0) { result.Append(c); continue; }
            int width = 1; while (i < pattern.Length && pattern[i] == c) { width++; i++; }
            string field = c switch
            {
                'a' => Sym("AMPMS", value.Hour >= 12 ? 1 : 0),
                'L' or 'M' => width >= 3 ? Sym((c == 'L' ? "STANDALONE" : "") + (width == 3 ? "SHORTMONTHS" : width == 4 ? "MONTHS" : "NARROWMONTHS"), value.Month - 1) : Pad(value.Month, width),
                'c' => width >= 3 ? Sym("STANDALONE" + (width == 3 ? "SHORTWEEKDAYS" : width == 4 ? "WEEKDAYS" : "NARROWWEEKDAYS"), (int)value.DayOfWeek) : Pad(value.Day, 1),
                'E' => width >= 6 ? throw new NotSupportedException("Short weekdays are not supported by intl.") : Sym(width <= 3 ? "SHORTWEEKDAYS" : width == 4 ? "WEEKDAYS" : "NARROWWEEKDAYS", (int)value.DayOfWeek),
                'G' => Sym(width >= 4 ? "ERANAMES" : "ERAS", 1),
                'd' => Pad(value.Day, width),
                'D' => Pad(value.DayOfYear, width),
                'H' => Pad(value.Hour, width),
                'h' => Pad(value.Hour % 12 == 0 ? 12 : value.Hour % 12, width),
                'K' => Pad(value.Hour % 12, width),
                'k' => Pad(value.Hour == 0 ? 24 : value.Hour, width),
                'm' => Pad(value.Minute, width),
                's' => Pad(value.Second, width),
                'y' => Pad(width == 2 ? value.Year % 100 : value.Year, width),
                'S' => Pad(value.Millisecond, 3) + new string(data.Zero[0], Math.Max(0, width - 3)),
                'Q' => width == 3 ? Sym("SHORTQUARTERS", (value.Month - 1) / 3) : width == 4 ? Sym("QUARTERS", (value.Month - 1) / 3) : Pad((value.Month - 1) / 3 + 1, width),
                _ => ""
            }; result.Append(field);
        }
        return result.ToString();
    }
    internal static string Digits(string text, string zero)
    {
        if (zero == "0") return text; var b = new StringBuilder(text.Length);
        foreach (char c in text) b.Append(c >= '0' && c <= '9' ? (char)(c - '0' + zero[0]) : c);
        return b.ToString();
    }
}
