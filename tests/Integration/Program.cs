using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.Json;
using Fixture;
var output = new SortedDictionary<string, string>();
foreach (var locale in new[] { "en", "sr", "ru", "ar", "he", "pl", "fr", "ja", "hi", "sr-Latn-RS", "sr-Latn-BA", "sr-Cyrl-RS", "sr-Cyrl-BG", "zz" })
{
    AppLocalizations l;
    try { l = AppLocalizations.ForLocale(locale); } catch (ArgumentException) { output.Add(locale + "/lookup-error", "unsupported"); continue; }
    void Add(string key, string value) => output.Add(locale + "/" + key, value);
    Add("hello", l.Hello); Add("empty", l.Empty); Add("time_format", l.TimeFormat);
    Add("greeting", l.Greeting("Ada")); Add("multi", l.Multi(first: 7, second: "B"));
    foreach (var g in new[] { "male", "female", "other", "unknown" }) Add("select/" + g, l.Select(g));
    foreach (var n in new[] { 0d, 1, 2, 3, 4, 5, 11, 21, 22, 101, 1.2, 2.1, -1 }) Add("items/" + n.ToString(CultureInfo.InvariantCulture), l.Items(n));
    foreach (var n in new[] { 0, 1, 2 }) foreach (var g in new[] { "male", "female", "other" }) Add("nested/" + n + "/" + g, l.Nested(n, g));
    Add("date", l.Date(new DateTime(2024, 7, 10))); Add("longDate", l.LongDate(new DateTime(2024, 7, 10))); Add("number", l.Number(12345.67)); Add("money", l.Money(1234.5)); Add("compact", l.Compact(1200));
    Add("event", l.Event); Add("namespace", l.Namespace); Add("checked", l.Checked); Add("parentText", l.ParentText);
    Add("implicit", l.Implicit(a: true, z: 7)); Add("unused", l.Unused(3)); Add("omit", l.Omit("Ada"));
    foreach (var n in new[] { 0d, 1, 2, 21, -1, 1.1 }) Add("merged/" + n.ToString(CultureInfo.InvariantCulture), l.Merged(n));
    Add("duplicateSelect", l.DuplicateSelect("a")); Add("select/suffix", l.Select("Enum.male"));
    Add("rawNum/int", l.RawNum(1)); Add("rawNum/double", l.RawNum(1d)); Add("largeInt", l.LargeInt(9007199254740993));
    var date = new DateTime(2024, 7, 10, 15, 4, 5, 123);
    Add("inlineDate", l.InlineDate(date)); Add("inlineTime", l.InlineTime(date)); Add("customDate", l.CustomDate(date)); Add("quarter", l.Quarter(date)); Add("combinedDate", l.CombinedDate(date));
    Add("currencyDefault", l.CurrencyDefault(1234.5)); Add("shortCurrency", l.ShortCurrency(1234.5)); Add("longCompact", l.LongCompact(1234.5)); Add("formatStringIgnored", l.FormatStringIgnored("Hello"));
    Add("escaped", l.Escaped); Add("pound", l.Pound(2)); Add("fallback", l.Fallback); Add("fallbackPlural", l.FallbackPlural(21));
}
Console.WriteLine(JsonSerializer.Serialize(output, FixtureJson.Default.SortedDictionaryStringString));

[System.Text.Json.Serialization.JsonSerializable(typeof(SortedDictionary<string, string>))]
[System.Text.Json.Serialization.JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class FixtureJson : System.Text.Json.Serialization.JsonSerializerContext { }
