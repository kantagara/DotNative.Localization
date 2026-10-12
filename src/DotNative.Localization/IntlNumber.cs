// Formatting algorithm ported from Dart intl 0.20.2; vendor/intl/LICENSE.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
namespace DotNative.Localization;

internal static class IntlNumber
{
    private sealed class Pattern
    {
        public string PositivePrefix = "", NegativePrefix = "", PositiveSuffix = "", NegativeSuffix = "";
        public int MinInteger = 1, MaxInteger = 40, MinFraction, MaxFraction = 3, Group = 3, FinalGroup = 3, Multiplier = 1, ExponentDigits;
        public bool DecimalAlways, Exponential, ExponentSign;
    }
    private static Pattern Parse(string pattern, NumberData data, string symbol, bool currency, int? digits, string name)
    {
        var result = new Pattern(); int i = 0;
        string Affix()
        {
            var s = new StringBuilder(); bool quoted = false;
            while (i < pattern.Length)
            {
                char c = pattern[i];
                if (c == '\'') { i++; if (i < pattern.Length && pattern[i] == '\'') { s.Append('\''); i++; } else quoted = !quoted; continue; }
                if (!quoted && "#0,.;".Contains(c)) break;
                if (!quoted)
                {
                    if (c == '¤') s.Append(symbol);
                    else if (c == '%') { s.Append(data.Percent); result.Multiplier = 100; }
                    else if (c == '‰') { s.Append(data.Permille); result.Multiplier = 1000; }
                    else s.Append(c);
                }
                else s.Append(c);
                i++;
            }
            return s.ToString();
        }
        result.PositivePrefix = Affix(); int start = i;
        while (i < pattern.Length && "#0,.E+".Contains(pattern[i])) i++;
        string trunk = pattern.Substring(start, i - start);
        result.PositiveSuffix = Affix();
        if (i < pattern.Length && pattern[i] == ';')
        {
            i++; result.NegativePrefix = Affix();
            foreach (char c in trunk) { if (i < pattern.Length && pattern[i] != c) throw new FormatException("Positive and negative trunks must be the same."); i++; }
            result.NegativeSuffix = Affix();
        }
        else { result.NegativePrefix = data.Minus + result.PositivePrefix; result.NegativeSuffix = result.PositiveSuffix; }
        var exp = trunk.Split('E'); result.Exponential = exp.Length > 1;
        if (result.Exponential) { result.ExponentSign = exp[1].StartsWith('+'); result.ExponentDigits = exp[1].Count(c => c == '0'); }
        var pieces = exp[0].Split('.'); string integer = pieces[0], fraction = pieces.Length > 1 ? pieces[1] : "";
        result.MinInteger = integer.Count(c => c == '0');
        result.MinFraction = fraction.Count(c => c == '0'); result.MaxFraction = fraction.Count(c => c == '0' || c == '#');
        result.DecimalAlways = exp[0].StartsWith('.') || exp[0].EndsWith('.');
        if (result.Exponential) { result.MaxInteger = integer.Count(c => c == '0' || c == '#'); if (result.MaxFraction == 0 && result.MinInteger == 0) result.MinInteger = 1; }
        var groups = integer.Split(','); result.FinalGroup = groups.Length > 1 ? groups[^1].Length : 0; result.Group = groups.Length > 2 ? groups[^2].Length : result.FinalGroup;
        if (currency) digits ??= IntlData.currencyDigits.GetValueOrDefault(name.ToUpperInvariant(), IntlData.currencyDigits["DEFAULT"]);
        if (digits.HasValue) result.MinFraction = result.MaxFraction = digits.Value;
        return result;
    }
    internal static string Format(double value, NumberData data, string format, int? digits, string? name, string? symbol, string? customPattern, long? exactInteger = null)
    {
        bool compact = format.StartsWith("compact", StringComparison.Ordinal), currency = format.Contains("Currency", StringComparison.Ordinal) || format is "currency" or "simpleCurrency";
        name ??= data.DefaultCurrency;
        bool simple = format is "simpleCurrency" or "compactSimpleCurrency";
        symbol ??= simple ? IntlData.currencySymbols.GetValueOrDefault(name, name) : name;
        string pattern = customPattern ?? (currency ? data.CurrencyPattern : format is "percentPattern" or "decimalPercentPattern" ? data.PercentPattern : format == "scientificPattern" ? data.ScientificPattern : data.DecimalPattern);
        var parsed = Parse(pattern, data, symbol, currency, digits, name);
        if (!compact) return Render(value, parsed, data, false, false, exactInteger);
        if (!double.IsFinite(value)) throw new ArgumentException("Non-finite compact input."); // intl style selection attempts round().
        var patterns = format == "compactLong" ? data.Long : currency ? data.Currency : data.Short;
        parsed.Group = parsed.FinalGroup = 0;
        if (Math.Abs(value) < 10) return Render(value, parsed, data, true, currency);
        int length = IntegerDigits(value); double rounded = value, divisor = 1;
        void RoundForStyle()
        {
            int fractionDigits = Math.Max(0, 3 - length + IntegerDigits(divisor) - 1);
            double factor = Math.Pow(10, fractionDigits);
            rounded = DartInteger(Math.Round(rounded * factor / divisor, MidpointRounding.AwayFromZero)) * divisor / factor;
            length = IntegerDigits(rounded);
        }
        RoundForStyle(); Dictionary<string, string>? selected = null; int exponent = 0;
        foreach (var entry in patterns)
        {
            if (entry.Key + 1 > length) break;
            selected = entry.Value; exponent = entry.Key; divisor = Divisor(selected["other"], exponent);
            RoundForStyle();
        }
        if (selected == null) return Render(value, parsed, data, true, currency);
        double scaled = value / divisor, abs = Math.Abs(scaled);
        int precision = Math.Max(0, 3 - IntegerDigits(Math.Floor(abs)));
        double scale = Math.Pow(10, precision), displayed = DartInteger(Math.Round(abs * scale, MidpointRounding.AwayFromZero)) / scale;
        string fractionDigitsString = Math.Floor(displayed * scale).ToString("0", CultureInfo.InvariantCulture);
        while (precision > 0 && fractionDigitsString.EndsWith('0')) { precision--; fractionDigitsString = fractionDigitsString.Substring(0, fractionDigitsString.Length - 1); }
        string category = PluralRules.Select(data.Locale, displayed, precision);
        string chosen = selected.GetValueOrDefault(category, category == "two" ? selected.GetValueOrDefault("few", selected["other"]) : selected["other"]);
        if (scaled >= 0 && precision == 0 && selected.TryGetValue(Math.Floor(displayed).ToString(CultureInfo.InvariantCulture), out var direct)) chosen = direct;
        if (chosen == "0") return Render(value, parsed, data, true, currency);
        var signed = chosen.Split(';'); bool negative = BitConverter.DoubleToInt64Bits(value) < 0;
        string actual = signed.Length > 1 ? (negative ? signed[^1] : signed[0]) : chosen;
        var match = Regex.Match(actual, "([^0]*)(0+)(.*)");
        if (!match.Success) return (negative ? data.Minus : "") + actual;
        divisor = Divisor(actual, exponent);
        parsed.PositivePrefix = match.Groups[1].Value;
        parsed.NegativePrefix = (signed.Length > 1 ? "" : data.Minus) + match.Groups[1].Value;
        parsed.PositiveSuffix = parsed.NegativeSuffix = match.Groups[3].Value;
        parsed.MinFraction = 0;
        string rendered = Render(value / divisor, parsed, data, true, false);
        if (currency) { int sign = rendered.IndexOf('¤'); if (sign >= 0) rendered = rendered.Substring(0, sign) + symbol + rendered.Substring(sign + 1); }
        return rendered;
    }
    private static double Divisor(string pattern, int exponent)
    {
        string positive = pattern.Split(';')[0]; var match = Regex.Match(positive, "([^0]*)(0+)(.*)");
        if (!match.Success) return Math.Pow(10, exponent);
        if (positive.All(c => c == '0')) return 1;
        return Math.Pow(10, exponent - match.Groups[2].Length + 1);
    }
    private static string Render(double value, Pattern pattern, NumberData data, bool significant, bool currencyFallback, long? exactInteger = null)
    {
        if (double.IsNaN(value)) return data.NaN;
        bool negative = BitConverter.DoubleToInt64Bits(value) < 0;
        string prefix = negative ? pattern.NegativePrefix : pattern.PositivePrefix, suffix = negative ? pattern.NegativeSuffix : pattern.PositiveSuffix;
        if (double.IsInfinity(value)) return prefix + data.Infinity;
        double number = Math.Abs(value); string body;
        if (pattern.Exponential && number != 0)
        {
            int exponent = (int)Math.Floor(Math.Log(number) / Math.Log(10)); double mantissa = number / Math.Pow(10, exponent);
            if (pattern.MaxInteger > 1 && pattern.MaxInteger > pattern.MinInteger) { while (exponent % pattern.MaxInteger != 0) { mantissa *= 10; exponent--; } }
            else if (pattern.MinInteger < 1) { exponent++; mantissa /= 10; }
            else { exponent -= pattern.MinInteger - 1; mantissa *= Math.Pow(10, pattern.MinInteger - 1); }
            body = Fixed(mantissa, pattern, data, significant, currencyFallback) + Exponent(exponent, pattern, data);
        }
        else { body = Fixed(number, pattern, data, significant, currencyFallback, exactInteger); if (pattern.Exponential) body += Exponent(0, pattern, data); }
        return prefix + body + suffix;
    }
    private static string Exponent(int exponent, Pattern p, NumberData d) => d.Exponent + (exponent < 0 ? d.Minus : p.ExponentSign ? d.Plus : "") + Formatting.Digits(Math.Abs(exponent).ToString(CultureInfo.InvariantCulture).PadLeft(p.ExponentDigits, '0'), d.Zero);
    private static string Fixed(double number, Pattern p, NumberData data, bool significant, bool currencyFallback, long? exactInteger = null)
    {
        double integer = Math.Floor(number), fraction = number - integer;
        int digits = p.MaxFraction, min = p.MinFraction;
        int multiplierDigits = p.Multiplier == 100 ? 2 : p.Multiplier == 1000 ? 3 : 0;
        void Compute()
        {
            if (!significant) return;
            int length = number == 0 ? 1 : integer != 0 ? IntegerDigits(integer) : (int)Math.Ceiling(Math.Log(fraction) / Math.Log(10));
            int remaining = 3 - multiplierDigits - length;
            digits = Math.Max(0, remaining);
            if (currencyFallback) digits = remaining > 0 ? p.MaxFraction : Math.Min(digits, p.MaxFraction);
        }
        Compute();
        long intPower = Pow10(digits); if (intPower <= 0) throw new ArithmeticException("intl fraction scale overflow.");
        double power = intPower, digitMultiplier = unchecked(intPower * p.Multiplier);
        double remainingDigits = Math.Round(fraction * digitMultiplier, MidpointRounding.AwayFromZero);
        bool rounded = false;
        if (remainingDigits >= digitMultiplier) { integer++; remainingDigits -= digitMultiplier; rounded = true; }
        else if (IntegerDigits(remainingDigits) > IntegerDigits(Math.Floor(fraction * digitMultiplier))) { fraction = remainingDigits / digitMultiplier; rounded = true; }
        if (rounded && significant) Compute(); // intl intentionally retains the original power here.
        var extra = new BigInteger(Math.Floor(remainingDigits / power));
        var fractionPart = new BigInteger(remainingDigits) % new BigInteger(power);
        string padding = "";
        string integral;
        if (exactInteger.HasValue) integral = exactInteger == 0 ? "" : BigInteger.Abs(new BigInteger(exactInteger.Value)).ToString(CultureInfo.InvariantCulture);
        else
        {
            if (integer > long.MaxValue)
            {
                int excess = (int)Math.Ceiling(Math.Log(integer) / Math.Log(10)) - 19;
                double divisor = DartInteger(Math.Pow(10, excess)); if (divisor == 0) divisor = Math.Pow(10, excess);
                integer = integer / divisor; padding = new string('0', Math.Max(0, excess));
            }
            integral = integer == 0 ? "" : DartInteger(integer).ToString(CultureInfo.InvariantCulture);
        }
        string additional = extra == 0 ? "" : extra.ToString(CultureInfo.InvariantCulture);
        if (integral.Length > 0) additional = additional.PadLeft(multiplierDigits, '0');
        integral += additional + padding;
        bool fractionPresent = digits > 0 && (min > 0 || fractionPart > 0);
        if (integral.Length > 0 || p.MinInteger > 0) integral = integral.PadLeft(p.MinInteger, '0'); else if (!fractionPresent) integral = "0";
        var result = new StringBuilder();
        for (int i = 0; i < integral.Length; i++)
        {
            result.Append(Formatting.Digits(integral[i].ToString(), data.Zero)); int remaining = integral.Length - i - 1;
            if (p.Group > 0 && remaining >= p.FinalGroup && (remaining == p.FinalGroup || (remaining - p.FinalGroup) % p.Group == 0)) result.Append(data.Group);
        }
        if (p.DecimalAlways || fractionPresent) result.Append(data.Decimal);
        if (fractionPresent)
        {
            string decimals = (fractionPart + new BigInteger(power)).ToString(CultureInfo.InvariantCulture).Substring(1);
            while (decimals.Length > min && decimals.EndsWith('0')) decimals = decimals.Substring(0, decimals.Length - 1);
            result.Append(Formatting.Digits(decimals, data.Zero));
        }
        return result.ToString();
    }
    private static long DartInteger(double value) => value >= long.MaxValue ? long.MaxValue : value <= long.MinValue ? long.MinValue : (long)value;
    private static long Pow10(int exponent) { if (exponent < 0) throw new ArgumentOutOfRangeException(nameof(exponent)); long n = 1; for (int i = 0; i < exponent; i++) n = unchecked(n * 10); return n; }
    private static int IntegerDigits(double value)
    {
        double n = Math.Abs(value), bound = 10;
        for (int i = 1; i < 19; i++, bound *= 10) if (n < bound) return i;
        return 19;
    }
}
