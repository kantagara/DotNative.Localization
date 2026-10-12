# Executed Flutter / C# parity

Flutter 3.41.0 (`44a626f4f0`), Dart 3.11.0, intl 0.20.2. **872/872 generated-message observations match. Zero allowed differences.**

Unknown-locale failures are compared as `unsupported` because Dart and CLR have different exception types. All successful strings are compared exactly, including Unicode spaces, digit shapes and script. The same ARB files go to both compilers. Rejected Dart keyword fixtures live separately and both builds must reject them.

## Template API

| ARB key | Flutter | C# |
| --- | --- | --- |
| `hello` | `String get hello;` | `string Hello { get; }` |
| `empty` | `String get empty;` | `string Empty { get; }` |
| `time_format` | `String get time_format;` | `string TimeFormat { get; }` |
| `greeting` | `String greeting(String name);` | `string Greeting(string @name);` |
| `multi` | `String multi(int first, String second);` | `string Multi(long @first, string @second);` |
| `select` | `String select(String gender);` | `string Select(string @gender);` |
| `items` | `String items(num count);` | `string Items(global::DotNative.Localization.L10nNumber @count);` |
| `nested` | `String nested(int count, String gender);` | `string Nested(long @count, string @gender);` |
| `date` | `String date(DateTime value);` | `string Date(global::System.DateTime @value);` |
| `longDate` | `String longDate(DateTime value);` | `string LongDate(global::System.DateTime @value);` |
| `number` | `String number(double value);` | `string Number(double @value);` |
| `money` | `String money(num value);` | `string Money(global::DotNative.Localization.L10nNumber @value);` |
| `compact` | `String compact(num value);` | `string Compact(global::DotNative.Localization.L10nNumber @value);` |
| `escaped` | `String get escaped;` | `string Escaped { get; }` |
| `pound` | `String pound(int count);` | `string Pound(long @count);` |
| `fallback` | `String get fallback;` | `string Fallback { get; }` |
| `fallbackPlural` | `String fallbackPlural(num count);` | `string FallbackPlural(global::DotNative.Localization.L10nNumber @count);` |
| `event` | `String get event;` | `string Event { get; }` |
| `namespace` | `String get namespace;` | `string Namespace { get; }` |
| `checked` | `String get checked;` | `string Checked { get; }` |
| `parentText` | `String get parentText;` | `string ParentText { get; }` |
| `implicit` | `String implicit(Object a, Object z);` | `string Implicit(object @a, object @z);` |
| `unused` | `String unused(int unused);` | `string Unused(long @unused);` |
| `omit` | `String omit(String name);` | `string Omit(string @name);` |
| `merged` | `String merged(num n);` | `string Merged(global::DotNative.Localization.L10nNumber @n);` |
| `duplicateSelect` | `String duplicateSelect(String g);` | `string DuplicateSelect(string @g);` |
| `rawNum` | `String rawNum(num n);` | `string RawNum(global::DotNative.Localization.L10nNumber @n);` |
| `largeInt` | `String largeInt(int n);` | `string LargeInt(long @n);` |
| `inlineDate` | `String inlineDate(DateTime date);` | `string InlineDate(global::System.DateTime @date);` |
| `inlineTime` | `String inlineTime(DateTime date);` | `string InlineTime(global::System.DateTime @date);` |
| `customDate` | `String customDate(DateTime date);` | `string CustomDate(global::System.DateTime @date);` |
| `quarter` | `String quarter(DateTime date);` | `string Quarter(global::System.DateTime @date);` |
| `combinedDate` | `String combinedDate(DateTime date);` | `string CombinedDate(global::System.DateTime @date);` |
| `currencyDefault` | `String currencyDefault(num n);` | `string CurrencyDefault(global::DotNative.Localization.L10nNumber @n);` |
| `shortCurrency` | `String shortCurrency(num n);` | `string ShortCurrency(global::DotNative.Localization.L10nNumber @n);` |
| `longCompact` | `String longCompact(num n);` | `string LongCompact(global::DotNative.Localization.L10nNumber @n);` |
| `formatStringIgnored` | `String formatStringIgnored(String x);` | `string FormatStringIgnored(string @x);` |

## Every generated-message observation

| Locale / member / arguments | Flutter | C# |
| --- | --- | --- |
| `ar/checked` | `"Checked"` | `"Checked"` |
| `ar/combinedDate` | `"10‏/7‏/2024 15:04:05"` | `"10‏/7‏/2024 15:04:05"` |
| `ar/compact` | `"Count 1.2\u00a0ألف"` | `"Count 1.2\u00a0ألف"` |
| `ar/currencyDefault` | `"‏1,234.50\u00a0EGP"` | `"‏1,234.50\u00a0EGP"` |
| `ar/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `ar/date` | `"Date 10‏/7‏/2024"` | `"Date 10‏/7‏/2024"` |
| `ar/duplicateSelect` | `"last"` | `"last"` |
| `ar/empty` | `""` | `""` |
| `ar/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `ar/event` | `"Event"` | `"Event"` |
| `ar/fallback` | `"Template fallback"` | `"Template fallback"` |
| `ar/fallbackPlural` | `"English other"` | `"English other"` |
| `ar/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `ar/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `ar/hello` | `"مرحبا"` | `"مرحبا"` |
| `ar/implicit` | `"7:true"` | `"7:true"` |
| `ar/inlineDate` | `"10 يوليو 2024"` | `"10 يوليو 2024"` |
| `ar/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `ar/items/-1` | `"many"` | `"many"` |
| `ar/items/0` | `"empty"` | `"empty"` |
| `ar/items/1` | `"one"` | `"one"` |
| `ar/items/1.2` | `"other"` | `"other"` |
| `ar/items/101` | `"other"` | `"other"` |
| `ar/items/11` | `"many"` | `"many"` |
| `ar/items/2` | `"few"` | `"few"` |
| `ar/items/2.1` | `"other"` | `"other"` |
| `ar/items/21` | `"many"` | `"many"` |
| `ar/items/22` | `"many"` | `"many"` |
| `ar/items/3` | `"few"` | `"few"` |
| `ar/items/4` | `"few"` | `"few"` |
| `ar/items/5` | `"few"` | `"few"` |
| `ar/largeInt` | `"9,007,199,254,740,993"` | `"9,007,199,254,740,993"` |
| `ar/longCompact` | `"1.23 ألف"` | `"1.23 ألف"` |
| `ar/longDate` | `"Date 10 يوليو 2024"` | `"Date 10 يوليو 2024"` |
| `ar/merged/-1` | `"other"` | `"other"` |
| `ar/merged/0` | `"other"` | `"other"` |
| `ar/merged/1` | `"last"` | `"last"` |
| `ar/merged/1.1` | `"other"` | `"other"` |
| `ar/merged/2` | `"two-last"` | `"two-last"` |
| `ar/merged/21` | `"other"` | `"other"` |
| `ar/money` | `"Money ‏1,234.50\u00a0$"` | `"Money ‏1,234.50\u00a0$"` |
| `ar/multi` | `"B then 7"` | `"B then 7"` |
| `ar/namespace` | `"Namespace"` | `"Namespace"` |
| `ar/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `ar/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `ar/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `ar/nested/1/female` | `"Her item"` | `"Her item"` |
| `ar/nested/1/male` | `"His item"` | `"His item"` |
| `ar/nested/1/other` | `"Their item"` | `"Their item"` |
| `ar/nested/2/female` | `"Her items"` | `"Her items"` |
| `ar/nested/2/male` | `"His items"` | `"His items"` |
| `ar/nested/2/other` | `"Their items"` | `"Their items"` |
| `ar/number` | `"Number 12,345.67"` | `"Number 12,345.67"` |
| `ar/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `ar/parentText` | `"English parent"` | `"English parent"` |
| `ar/pound` | `"# items"` | `"# items"` |
| `ar/quarter` | `"الربع الثالث 2024"` | `"الربع الثالث 2024"` |
| `ar/rawNum/double` | `"1.0"` | `"1.0"` |
| `ar/rawNum/int` | `"1"` | `"1"` |
| `ar/select/female` | `"She"` | `"She"` |
| `ar/select/male` | `"He"` | `"He"` |
| `ar/select/other` | `"They"` | `"They"` |
| `ar/select/suffix` | `"He"` | `"He"` |
| `ar/select/unknown` | `"They"` | `"They"` |
| `ar/shortCurrency` | `"1.23\u00a0ألف\u00a0EGP"` | `"1.23\u00a0ألف\u00a0EGP"` |
| `ar/time_format` | `"24-hour"` | `"24-hour"` |
| `ar/unused` | `"ready"` | `"ready"` |
| `en/checked` | `"Checked"` | `"Checked"` |
| `en/combinedDate` | `"7/10/2024 15:04:05"` | `"7/10/2024 15:04:05"` |
| `en/compact` | `"Count 1.2K"` | `"Count 1.2K"` |
| `en/currencyDefault` | `"USD1,234.50"` | `"USD1,234.50"` |
| `en/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `en/date` | `"Date 7/10/2024"` | `"Date 7/10/2024"` |
| `en/duplicateSelect` | `"last"` | `"last"` |
| `en/empty` | `""` | `""` |
| `en/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `en/event` | `"Event"` | `"Event"` |
| `en/fallback` | `"Template fallback"` | `"Template fallback"` |
| `en/fallbackPlural` | `"English other"` | `"English other"` |
| `en/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `en/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `en/hello` | `"Hello"` | `"Hello"` |
| `en/implicit` | `"7:true"` | `"7:true"` |
| `en/inlineDate` | `"Jul 10, 2024"` | `"Jul 10, 2024"` |
| `en/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `en/items/-1` | `"other"` | `"other"` |
| `en/items/0` | `"empty"` | `"empty"` |
| `en/items/1` | `"one"` | `"one"` |
| `en/items/1.2` | `"other"` | `"other"` |
| `en/items/101` | `"other"` | `"other"` |
| `en/items/11` | `"other"` | `"other"` |
| `en/items/2` | `"other"` | `"other"` |
| `en/items/2.1` | `"other"` | `"other"` |
| `en/items/21` | `"other"` | `"other"` |
| `en/items/22` | `"other"` | `"other"` |
| `en/items/3` | `"other"` | `"other"` |
| `en/items/4` | `"other"` | `"other"` |
| `en/items/5` | `"other"` | `"other"` |
| `en/largeInt` | `"9,007,199,254,740,993"` | `"9,007,199,254,740,993"` |
| `en/longCompact` | `"1.23 thousand"` | `"1.23 thousand"` |
| `en/longDate` | `"Date Jul 10, 2024"` | `"Date Jul 10, 2024"` |
| `en/merged/-1` | `"other"` | `"other"` |
| `en/merged/0` | `"other"` | `"other"` |
| `en/merged/1` | `"last"` | `"last"` |
| `en/merged/1.1` | `"other"` | `"other"` |
| `en/merged/2` | `"two-last"` | `"two-last"` |
| `en/merged/21` | `"other"` | `"other"` |
| `en/money` | `"Money $1,234.50"` | `"Money $1,234.50"` |
| `en/multi` | `"B then 7"` | `"B then 7"` |
| `en/namespace` | `"Namespace"` | `"Namespace"` |
| `en/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `en/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `en/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `en/nested/1/female` | `"Her item"` | `"Her item"` |
| `en/nested/1/male` | `"His item"` | `"His item"` |
| `en/nested/1/other` | `"Their item"` | `"Their item"` |
| `en/nested/2/female` | `"Her items"` | `"Her items"` |
| `en/nested/2/male` | `"His items"` | `"His items"` |
| `en/nested/2/other` | `"Their items"` | `"Their items"` |
| `en/number` | `"Number 12,345.67"` | `"Number 12,345.67"` |
| `en/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `en/parentText` | `"English parent"` | `"English parent"` |
| `en/pound` | `"# items"` | `"# items"` |
| `en/quarter` | `"3rd quarter 2024"` | `"3rd quarter 2024"` |
| `en/rawNum/double` | `"1.0"` | `"1.0"` |
| `en/rawNum/int` | `"1"` | `"1"` |
| `en/select/female` | `"She"` | `"She"` |
| `en/select/male` | `"He"` | `"He"` |
| `en/select/other` | `"They"` | `"They"` |
| `en/select/suffix` | `"He"` | `"He"` |
| `en/select/unknown` | `"They"` | `"They"` |
| `en/shortCurrency` | `"USD1.23K"` | `"USD1.23K"` |
| `en/time_format` | `"24-hour"` | `"24-hour"` |
| `en/unused` | `"ready"` | `"ready"` |
| `fr/checked` | `"Checked"` | `"Checked"` |
| `fr/combinedDate` | `"10/07/2024 15:04:05"` | `"10/07/2024 15:04:05"` |
| `fr/compact` | `"Count 1,2\u00a0k"` | `"Count 1,2\u00a0k"` |
| `fr/currencyDefault` | `"1\u202f234,50\u00a0EUR"` | `"1\u202f234,50\u00a0EUR"` |
| `fr/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `fr/date` | `"Date 10/07/2024"` | `"Date 10/07/2024"` |
| `fr/duplicateSelect` | `"last"` | `"last"` |
| `fr/empty` | `""` | `""` |
| `fr/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `fr/event` | `"Event"` | `"Event"` |
| `fr/fallback` | `"Template fallback"` | `"Template fallback"` |
| `fr/fallbackPlural` | `"English other"` | `"English other"` |
| `fr/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `fr/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `fr/hello` | `"Bonjour"` | `"Bonjour"` |
| `fr/implicit` | `"7:true"` | `"7:true"` |
| `fr/inlineDate` | `"10 juil. 2024"` | `"10 juil. 2024"` |
| `fr/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `fr/items/-1` | `"other"` | `"other"` |
| `fr/items/0` | `"empty"` | `"empty"` |
| `fr/items/1` | `"one"` | `"one"` |
| `fr/items/1.2` | `"one"` | `"one"` |
| `fr/items/101` | `"other"` | `"other"` |
| `fr/items/11` | `"other"` | `"other"` |
| `fr/items/2` | `"other"` | `"other"` |
| `fr/items/2.1` | `"other"` | `"other"` |
| `fr/items/21` | `"other"` | `"other"` |
| `fr/items/22` | `"other"` | `"other"` |
| `fr/items/3` | `"other"` | `"other"` |
| `fr/items/4` | `"other"` | `"other"` |
| `fr/items/5` | `"other"` | `"other"` |
| `fr/largeInt` | `"9\u202f007\u202f199\u202f254\u202f740\u202f993"` | `"9\u202f007\u202f199\u202f254\u202f740\u202f993"` |
| `fr/longCompact` | `"1,23 millier"` | `"1,23 millier"` |
| `fr/longDate` | `"Date 10 juil. 2024"` | `"Date 10 juil. 2024"` |
| `fr/merged/-1` | `"other"` | `"other"` |
| `fr/merged/0` | `"last"` | `"last"` |
| `fr/merged/1` | `"last"` | `"last"` |
| `fr/merged/1.1` | `"last"` | `"last"` |
| `fr/merged/2` | `"two-last"` | `"two-last"` |
| `fr/merged/21` | `"other"` | `"other"` |
| `fr/money` | `"Money 1\u202f234,50\u00a0$"` | `"Money 1\u202f234,50\u00a0$"` |
| `fr/multi` | `"B then 7"` | `"B then 7"` |
| `fr/namespace` | `"Namespace"` | `"Namespace"` |
| `fr/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `fr/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `fr/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `fr/nested/1/female` | `"Her item"` | `"Her item"` |
| `fr/nested/1/male` | `"His item"` | `"His item"` |
| `fr/nested/1/other` | `"Their item"` | `"Their item"` |
| `fr/nested/2/female` | `"Her items"` | `"Her items"` |
| `fr/nested/2/male` | `"His items"` | `"His items"` |
| `fr/nested/2/other` | `"Their items"` | `"Their items"` |
| `fr/number` | `"Number 12\u202f345,67"` | `"Number 12\u202f345,67"` |
| `fr/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `fr/parentText` | `"English parent"` | `"English parent"` |
| `fr/pound` | `"# items"` | `"# items"` |
| `fr/quarter` | `"3e trimestre 2024"` | `"3e trimestre 2024"` |
| `fr/rawNum/double` | `"1.0"` | `"1.0"` |
| `fr/rawNum/int` | `"1"` | `"1"` |
| `fr/select/female` | `"She"` | `"She"` |
| `fr/select/male` | `"He"` | `"He"` |
| `fr/select/other` | `"They"` | `"They"` |
| `fr/select/suffix` | `"He"` | `"He"` |
| `fr/select/unknown` | `"They"` | `"They"` |
| `fr/shortCurrency` | `"1,23\u00a0k\u00a0EUR"` | `"1,23\u00a0k\u00a0EUR"` |
| `fr/time_format` | `"24-hour"` | `"24-hour"` |
| `fr/unused` | `"ready"` | `"ready"` |
| `he/checked` | `"Checked"` | `"Checked"` |
| `he/combinedDate` | `"10.7.2024 15:04:05"` | `"10.7.2024 15:04:05"` |
| `he/compact` | `"Count 1.2K‏"` | `"Count 1.2K‏"` |
| `he/currencyDefault` | `"‏1,234.50\u00a0‏ILS"` | `"‏1,234.50\u00a0‏ILS"` |
| `he/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `he/date` | `"Date 10.7.2024"` | `"Date 10.7.2024"` |
| `he/duplicateSelect` | `"last"` | `"last"` |
| `he/empty` | `""` | `""` |
| `he/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `he/event` | `"Event"` | `"Event"` |
| `he/fallback` | `"Template fallback"` | `"Template fallback"` |
| `he/fallbackPlural` | `"English other"` | `"English other"` |
| `he/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `he/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `he/hello` | `"שלום"` | `"שלום"` |
| `he/implicit` | `"7:true"` | `"7:true"` |
| `he/inlineDate` | `"10 ביולי 2024"` | `"10 ביולי 2024"` |
| `he/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `he/items/-1` | `"other"` | `"other"` |
| `he/items/0` | `"empty"` | `"empty"` |
| `he/items/1` | `"one"` | `"one"` |
| `he/items/1.2` | `"other"` | `"other"` |
| `he/items/101` | `"other"` | `"other"` |
| `he/items/11` | `"other"` | `"other"` |
| `he/items/2` | `"few"` | `"few"` |
| `he/items/2.1` | `"other"` | `"other"` |
| `he/items/21` | `"other"` | `"other"` |
| `he/items/22` | `"other"` | `"other"` |
| `he/items/3` | `"other"` | `"other"` |
| `he/items/4` | `"other"` | `"other"` |
| `he/items/5` | `"other"` | `"other"` |
| `he/largeInt` | `"9,007,199,254,740,993"` | `"9,007,199,254,740,993"` |
| `he/longCompact` | `"‏1.23 אלף"` | `"‏1.23 אלף"` |
| `he/longDate` | `"Date 10 ביולי 2024"` | `"Date 10 ביולי 2024"` |
| `he/merged/-1` | `"other"` | `"other"` |
| `he/merged/0` | `"other"` | `"other"` |
| `he/merged/1` | `"last"` | `"last"` |
| `he/merged/1.1` | `"other"` | `"other"` |
| `he/merged/2` | `"two-last"` | `"two-last"` |
| `he/merged/21` | `"other"` | `"other"` |
| `he/money` | `"Money ‏1,234.50\u00a0‏$"` | `"Money ‏1,234.50\u00a0‏$"` |
| `he/multi` | `"B then 7"` | `"B then 7"` |
| `he/namespace` | `"Namespace"` | `"Namespace"` |
| `he/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `he/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `he/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `he/nested/1/female` | `"Her item"` | `"Her item"` |
| `he/nested/1/male` | `"His item"` | `"His item"` |
| `he/nested/1/other` | `"Their item"` | `"Their item"` |
| `he/nested/2/female` | `"Her items"` | `"Her items"` |
| `he/nested/2/male` | `"His items"` | `"His items"` |
| `he/nested/2/other` | `"Their items"` | `"Their items"` |
| `he/number` | `"Number 12,345.67"` | `"Number 12,345.67"` |
| `he/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `he/parentText` | `"English parent"` | `"English parent"` |
| `he/pound` | `"# items"` | `"# items"` |
| `he/quarter` | `"רבעון 3 2024"` | `"רבעון 3 2024"` |
| `he/rawNum/double` | `"1.0"` | `"1.0"` |
| `he/rawNum/int` | `"1"` | `"1"` |
| `he/select/female` | `"She"` | `"She"` |
| `he/select/male` | `"He"` | `"He"` |
| `he/select/other` | `"They"` | `"They"` |
| `he/select/suffix` | `"He"` | `"He"` |
| `he/select/unknown` | `"They"` | `"They"` |
| `he/shortCurrency` | `"ILS1.23K‏"` | `"ILS1.23K‏"` |
| `he/time_format` | `"24-hour"` | `"24-hour"` |
| `he/unused` | `"ready"` | `"ready"` |
| `hi/checked` | `"Checked"` | `"Checked"` |
| `hi/combinedDate` | `"10/7/2024 15:04:05"` | `"10/7/2024 15:04:05"` |
| `hi/compact` | `"Count 1.2\u00a0हज़ार"` | `"Count 1.2\u00a0हज़ार"` |
| `hi/currencyDefault` | `"INR1,234.50"` | `"INR1,234.50"` |
| `hi/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `hi/date` | `"Date 10/7/2024"` | `"Date 10/7/2024"` |
| `hi/duplicateSelect` | `"last"` | `"last"` |
| `hi/empty` | `""` | `""` |
| `hi/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `hi/event` | `"Event"` | `"Event"` |
| `hi/fallback` | `"Template fallback"` | `"Template fallback"` |
| `hi/fallbackPlural` | `"English other"` | `"English other"` |
| `hi/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `hi/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `hi/hello` | `"नमस्ते"` | `"नमस्ते"` |
| `hi/implicit` | `"7:true"` | `"7:true"` |
| `hi/inlineDate` | `"10 जुल॰ 2024"` | `"10 जुल॰ 2024"` |
| `hi/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `hi/items/-1` | `"other"` | `"other"` |
| `hi/items/0` | `"empty"` | `"empty"` |
| `hi/items/1` | `"one"` | `"one"` |
| `hi/items/1.2` | `"other"` | `"other"` |
| `hi/items/101` | `"other"` | `"other"` |
| `hi/items/11` | `"other"` | `"other"` |
| `hi/items/2` | `"other"` | `"other"` |
| `hi/items/2.1` | `"other"` | `"other"` |
| `hi/items/21` | `"other"` | `"other"` |
| `hi/items/22` | `"other"` | `"other"` |
| `hi/items/3` | `"other"` | `"other"` |
| `hi/items/4` | `"other"` | `"other"` |
| `hi/items/5` | `"other"` | `"other"` |
| `hi/largeInt` | `"9,00,71,99,25,47,40,993"` | `"9,00,71,99,25,47,40,993"` |
| `hi/longCompact` | `"1.23 हज़ार"` | `"1.23 हज़ार"` |
| `hi/longDate` | `"Date 10 जुल॰ 2024"` | `"Date 10 जुल॰ 2024"` |
| `hi/merged/-1` | `"other"` | `"other"` |
| `hi/merged/0` | `"last"` | `"last"` |
| `hi/merged/1` | `"last"` | `"last"` |
| `hi/merged/1.1` | `"other"` | `"other"` |
| `hi/merged/2` | `"two-last"` | `"two-last"` |
| `hi/merged/21` | `"other"` | `"other"` |
| `hi/money` | `"Money $1,234.50"` | `"Money $1,234.50"` |
| `hi/multi` | `"B then 7"` | `"B then 7"` |
| `hi/namespace` | `"Namespace"` | `"Namespace"` |
| `hi/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `hi/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `hi/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `hi/nested/1/female` | `"Her item"` | `"Her item"` |
| `hi/nested/1/male` | `"His item"` | `"His item"` |
| `hi/nested/1/other` | `"Their item"` | `"Their item"` |
| `hi/nested/2/female` | `"Her items"` | `"Her items"` |
| `hi/nested/2/male` | `"His items"` | `"His items"` |
| `hi/nested/2/other` | `"Their items"` | `"Their items"` |
| `hi/number` | `"Number 12,345.67"` | `"Number 12,345.67"` |
| `hi/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `hi/parentText` | `"English parent"` | `"English parent"` |
| `hi/pound` | `"# items"` | `"# items"` |
| `hi/quarter` | `"तीसरी तिमाही 2024"` | `"तीसरी तिमाही 2024"` |
| `hi/rawNum/double` | `"1.0"` | `"1.0"` |
| `hi/rawNum/int` | `"1"` | `"1"` |
| `hi/select/female` | `"She"` | `"She"` |
| `hi/select/male` | `"He"` | `"He"` |
| `hi/select/other` | `"They"` | `"They"` |
| `hi/select/suffix` | `"He"` | `"He"` |
| `hi/select/unknown` | `"They"` | `"They"` |
| `hi/shortCurrency` | `"INR1.23\u00a0हज़ार"` | `"INR1.23\u00a0हज़ार"` |
| `hi/time_format` | `"24-hour"` | `"24-hour"` |
| `hi/unused` | `"ready"` | `"ready"` |
| `ja/checked` | `"Checked"` | `"Checked"` |
| `ja/combinedDate` | `"2024/7/10 15:04:05"` | `"2024/7/10 15:04:05"` |
| `ja/compact` | `"Count 1200"` | `"Count 1200"` |
| `ja/currencyDefault` | `"JPY1,235"` | `"JPY1,235"` |
| `ja/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `ja/date` | `"Date 2024/7/10"` | `"Date 2024/7/10"` |
| `ja/duplicateSelect` | `"last"` | `"last"` |
| `ja/empty` | `""` | `""` |
| `ja/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `ja/event` | `"Event"` | `"Event"` |
| `ja/fallback` | `"Template fallback"` | `"Template fallback"` |
| `ja/fallbackPlural` | `"English other"` | `"English other"` |
| `ja/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `ja/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `ja/hello` | `"こんにちは"` | `"こんにちは"` |
| `ja/implicit` | `"7:true"` | `"7:true"` |
| `ja/inlineDate` | `"2024年7月10日"` | `"2024年7月10日"` |
| `ja/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `ja/items/-1` | `"other"` | `"other"` |
| `ja/items/0` | `"empty"` | `"empty"` |
| `ja/items/1` | `"one"` | `"one"` |
| `ja/items/1.2` | `"other"` | `"other"` |
| `ja/items/101` | `"other"` | `"other"` |
| `ja/items/11` | `"other"` | `"other"` |
| `ja/items/2` | `"other"` | `"other"` |
| `ja/items/2.1` | `"other"` | `"other"` |
| `ja/items/21` | `"other"` | `"other"` |
| `ja/items/22` | `"other"` | `"other"` |
| `ja/items/3` | `"other"` | `"other"` |
| `ja/items/4` | `"other"` | `"other"` |
| `ja/items/5` | `"other"` | `"other"` |
| `ja/largeInt` | `"9,007,199,254,740,993"` | `"9,007,199,254,740,993"` |
| `ja/longCompact` | `"1235"` | `"1235"` |
| `ja/longDate` | `"Date 2024年7月10日"` | `"Date 2024年7月10日"` |
| `ja/merged/-1` | `"other"` | `"other"` |
| `ja/merged/0` | `"other"` | `"other"` |
| `ja/merged/1` | `"last"` | `"last"` |
| `ja/merged/1.1` | `"other"` | `"other"` |
| `ja/merged/2` | `"two-last"` | `"two-last"` |
| `ja/merged/21` | `"other"` | `"other"` |
| `ja/money` | `"Money $1,234.50"` | `"Money $1,234.50"` |
| `ja/multi` | `"B then 7"` | `"B then 7"` |
| `ja/namespace` | `"Namespace"` | `"Namespace"` |
| `ja/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `ja/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `ja/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `ja/nested/1/female` | `"Her item"` | `"Her item"` |
| `ja/nested/1/male` | `"His item"` | `"His item"` |
| `ja/nested/1/other` | `"Their item"` | `"Their item"` |
| `ja/nested/2/female` | `"Her items"` | `"Her items"` |
| `ja/nested/2/male` | `"His items"` | `"His items"` |
| `ja/nested/2/other` | `"Their items"` | `"Their items"` |
| `ja/number` | `"Number 12,345.67"` | `"Number 12,345.67"` |
| `ja/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `ja/parentText` | `"English parent"` | `"English parent"` |
| `ja/pound` | `"# items"` | `"# items"` |
| `ja/quarter` | `"2024年第3四半期"` | `"2024年第3四半期"` |
| `ja/rawNum/double` | `"1.0"` | `"1.0"` |
| `ja/rawNum/int` | `"1"` | `"1"` |
| `ja/select/female` | `"She"` | `"She"` |
| `ja/select/male` | `"He"` | `"He"` |
| `ja/select/other` | `"They"` | `"They"` |
| `ja/select/suffix` | `"He"` | `"He"` |
| `ja/select/unknown` | `"They"` | `"They"` |
| `ja/shortCurrency` | `"JPY1235"` | `"JPY1235"` |
| `ja/time_format` | `"24-hour"` | `"24-hour"` |
| `ja/unused` | `"ready"` | `"ready"` |
| `pl/checked` | `"Checked"` | `"Checked"` |
| `pl/combinedDate` | `"10.07.2024 15:04:05"` | `"10.07.2024 15:04:05"` |
| `pl/compact` | `"Count 1,2\u00a0tys."` | `"Count 1,2\u00a0tys."` |
| `pl/currencyDefault` | `"1\u00a0234,50\u00a0PLN"` | `"1\u00a0234,50\u00a0PLN"` |
| `pl/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `pl/date` | `"Date 10.07.2024"` | `"Date 10.07.2024"` |
| `pl/duplicateSelect` | `"last"` | `"last"` |
| `pl/empty` | `""` | `""` |
| `pl/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `pl/event` | `"Event"` | `"Event"` |
| `pl/fallback` | `"Template fallback"` | `"Template fallback"` |
| `pl/fallbackPlural` | `"English other"` | `"English other"` |
| `pl/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `pl/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `pl/hello` | `"Cześć"` | `"Cześć"` |
| `pl/implicit` | `"7:true"` | `"7:true"` |
| `pl/inlineDate` | `"10 lip 2024"` | `"10 lip 2024"` |
| `pl/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `pl/items/-1` | `"many"` | `"many"` |
| `pl/items/0` | `"empty"` | `"empty"` |
| `pl/items/1` | `"one"` | `"one"` |
| `pl/items/1.2` | `"other"` | `"other"` |
| `pl/items/101` | `"many"` | `"many"` |
| `pl/items/11` | `"many"` | `"many"` |
| `pl/items/2` | `"few"` | `"few"` |
| `pl/items/2.1` | `"other"` | `"other"` |
| `pl/items/21` | `"many"` | `"many"` |
| `pl/items/22` | `"few"` | `"few"` |
| `pl/items/3` | `"few"` | `"few"` |
| `pl/items/4` | `"few"` | `"few"` |
| `pl/items/5` | `"many"` | `"many"` |
| `pl/largeInt` | `"9\u00a0007\u00a0199\u00a0254\u00a0740\u00a0993"` | `"9\u00a0007\u00a0199\u00a0254\u00a0740\u00a0993"` |
| `pl/longCompact` | `"1,23 tysiąca"` | `"1,23 tysiąca"` |
| `pl/longDate` | `"Date 10 lip 2024"` | `"Date 10 lip 2024"` |
| `pl/merged/-1` | `"other"` | `"other"` |
| `pl/merged/0` | `"other"` | `"other"` |
| `pl/merged/1` | `"last"` | `"last"` |
| `pl/merged/1.1` | `"other"` | `"other"` |
| `pl/merged/2` | `"two-last"` | `"two-last"` |
| `pl/merged/21` | `"other"` | `"other"` |
| `pl/money` | `"Money 1\u00a0234,50\u00a0$"` | `"Money 1\u00a0234,50\u00a0$"` |
| `pl/multi` | `"B then 7"` | `"B then 7"` |
| `pl/namespace` | `"Namespace"` | `"Namespace"` |
| `pl/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `pl/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `pl/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `pl/nested/1/female` | `"Her item"` | `"Her item"` |
| `pl/nested/1/male` | `"His item"` | `"His item"` |
| `pl/nested/1/other` | `"Their item"` | `"Their item"` |
| `pl/nested/2/female` | `"Her items"` | `"Her items"` |
| `pl/nested/2/male` | `"His items"` | `"His items"` |
| `pl/nested/2/other` | `"Their items"` | `"Their items"` |
| `pl/number` | `"Number 12\u00a0345,67"` | `"Number 12\u00a0345,67"` |
| `pl/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `pl/parentText` | `"English parent"` | `"English parent"` |
| `pl/pound` | `"# items"` | `"# items"` |
| `pl/quarter` | `"III kwartał 2024"` | `"III kwartał 2024"` |
| `pl/rawNum/double` | `"1.0"` | `"1.0"` |
| `pl/rawNum/int` | `"1"` | `"1"` |
| `pl/select/female` | `"She"` | `"She"` |
| `pl/select/male` | `"He"` | `"He"` |
| `pl/select/other` | `"They"` | `"They"` |
| `pl/select/suffix` | `"He"` | `"He"` |
| `pl/select/unknown` | `"They"` | `"They"` |
| `pl/shortCurrency` | `"1,23\u00a0tys.\u00a0PLN"` | `"1,23\u00a0tys.\u00a0PLN"` |
| `pl/time_format` | `"24-hour"` | `"24-hour"` |
| `pl/unused` | `"ready"` | `"ready"` |
| `ru/checked` | `"Checked"` | `"Checked"` |
| `ru/combinedDate` | `"10.07.2024 15:04:05"` | `"10.07.2024 15:04:05"` |
| `ru/compact` | `"Count 1,2\u00a0тыс."` | `"Count 1,2\u00a0тыс."` |
| `ru/currencyDefault` | `"1\u00a0234,50\u00a0RUB"` | `"1\u00a0234,50\u00a0RUB"` |
| `ru/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `ru/date` | `"Date 10.07.2024"` | `"Date 10.07.2024"` |
| `ru/duplicateSelect` | `"last"` | `"last"` |
| `ru/empty` | `""` | `""` |
| `ru/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `ru/event` | `"Event"` | `"Event"` |
| `ru/fallback` | `"Template fallback"` | `"Template fallback"` |
| `ru/fallbackPlural` | `"English one"` | `"English one"` |
| `ru/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `ru/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `ru/hello` | `"Привет"` | `"Привет"` |
| `ru/implicit` | `"7:true"` | `"7:true"` |
| `ru/inlineDate` | `"10 июл. 2024\u202fг."` | `"10 июл. 2024\u202fг."` |
| `ru/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `ru/items/-1` | `"many"` | `"many"` |
| `ru/items/0` | `"empty"` | `"empty"` |
| `ru/items/1` | `"one"` | `"one"` |
| `ru/items/1.2` | `"other"` | `"other"` |
| `ru/items/101` | `"one"` | `"one"` |
| `ru/items/11` | `"many"` | `"many"` |
| `ru/items/2` | `"few"` | `"few"` |
| `ru/items/2.1` | `"other"` | `"other"` |
| `ru/items/21` | `"one"` | `"one"` |
| `ru/items/22` | `"few"` | `"few"` |
| `ru/items/3` | `"few"` | `"few"` |
| `ru/items/4` | `"few"` | `"few"` |
| `ru/items/5` | `"many"` | `"many"` |
| `ru/largeInt` | `"9\u00a0007\u00a0199\u00a0254\u00a0740\u00a0993"` | `"9\u00a0007\u00a0199\u00a0254\u00a0740\u00a0993"` |
| `ru/longCompact` | `"1,23 тысячи"` | `"1,23 тысячи"` |
| `ru/longDate` | `"Date 10 июл. 2024\u202fг."` | `"Date 10 июл. 2024\u202fг."` |
| `ru/merged/-1` | `"other"` | `"other"` |
| `ru/merged/0` | `"other"` | `"other"` |
| `ru/merged/1` | `"last"` | `"last"` |
| `ru/merged/1.1` | `"other"` | `"other"` |
| `ru/merged/2` | `"two-last"` | `"two-last"` |
| `ru/merged/21` | `"last"` | `"last"` |
| `ru/money` | `"Money 1\u00a0234,50\u00a0$"` | `"Money 1\u00a0234,50\u00a0$"` |
| `ru/multi` | `"B then 7"` | `"B then 7"` |
| `ru/namespace` | `"Namespace"` | `"Namespace"` |
| `ru/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `ru/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `ru/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `ru/nested/1/female` | `"Her item"` | `"Her item"` |
| `ru/nested/1/male` | `"His item"` | `"His item"` |
| `ru/nested/1/other` | `"Their item"` | `"Their item"` |
| `ru/nested/2/female` | `"Her items"` | `"Her items"` |
| `ru/nested/2/male` | `"His items"` | `"His items"` |
| `ru/nested/2/other` | `"Their items"` | `"Their items"` |
| `ru/number` | `"Number 12\u00a0345,67"` | `"Number 12\u00a0345,67"` |
| `ru/omit` | `"Hello Ada"` | `"Hello Ada"` |
| `ru/parentText` | `"English parent"` | `"English parent"` |
| `ru/pound` | `"# items"` | `"# items"` |
| `ru/quarter` | `"3-й квартал 2024\u202fг."` | `"3-й квартал 2024\u202fг."` |
| `ru/rawNum/double` | `"1.0"` | `"1.0"` |
| `ru/rawNum/int` | `"1"` | `"1"` |
| `ru/select/female` | `"She"` | `"She"` |
| `ru/select/male` | `"He"` | `"He"` |
| `ru/select/other` | `"They"` | `"They"` |
| `ru/select/suffix` | `"He"` | `"He"` |
| `ru/select/unknown` | `"They"` | `"They"` |
| `ru/shortCurrency` | `"1,23\u00a0тыс.\u00a0RUB"` | `"1,23\u00a0тыс.\u00a0RUB"` |
| `ru/time_format` | `"24-hour"` | `"24-hour"` |
| `ru/unused` | `"ready"` | `"ready"` |
| `sr-Cyrl-BG/checked` | `"Checked"` | `"Checked"` |
| `sr-Cyrl-BG/combinedDate` | `"10. 7. 2024. 15:04:05"` | `"10. 7. 2024. 15:04:05"` |
| `sr-Cyrl-BG/compact` | `"Count 1,2\u00a0хиљ."` | `"Count 1,2\u00a0хиљ."` |
| `sr-Cyrl-BG/currencyDefault` | `"1.235\u00a0RSD"` | `"1.235\u00a0RSD"` |
| `sr-Cyrl-BG/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `sr-Cyrl-BG/date` | `"Date 10. 7. 2024."` | `"Date 10. 7. 2024."` |
| `sr-Cyrl-BG/duplicateSelect` | `"last"` | `"last"` |
| `sr-Cyrl-BG/empty` | `""` | `""` |
| `sr-Cyrl-BG/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `sr-Cyrl-BG/event` | `"Event"` | `"Event"` |
| `sr-Cyrl-BG/fallback` | `"Template fallback"` | `"Template fallback"` |
| `sr-Cyrl-BG/fallbackPlural` | `"English one"` | `"English one"` |
| `sr-Cyrl-BG/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `sr-Cyrl-BG/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `sr-Cyrl-BG/hello` | `"Здраво"` | `"Здраво"` |
| `sr-Cyrl-BG/implicit` | `"7:true"` | `"7:true"` |
| `sr-Cyrl-BG/inlineDate` | `"10. јул 2024."` | `"10. јул 2024."` |
| `sr-Cyrl-BG/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `sr-Cyrl-BG/items/-1` | `"other"` | `"other"` |
| `sr-Cyrl-BG/items/0` | `"empty"` | `"empty"` |
| `sr-Cyrl-BG/items/1` | `"one"` | `"one"` |
| `sr-Cyrl-BG/items/1.2` | `"few"` | `"few"` |
| `sr-Cyrl-BG/items/101` | `"one"` | `"one"` |
| `sr-Cyrl-BG/items/11` | `"other"` | `"other"` |
| `sr-Cyrl-BG/items/2` | `"few"` | `"few"` |
| `sr-Cyrl-BG/items/2.1` | `"one"` | `"one"` |
| `sr-Cyrl-BG/items/21` | `"one"` | `"one"` |
| `sr-Cyrl-BG/items/22` | `"few"` | `"few"` |
| `sr-Cyrl-BG/items/3` | `"few"` | `"few"` |
| `sr-Cyrl-BG/items/4` | `"few"` | `"few"` |
| `sr-Cyrl-BG/items/5` | `"other"` | `"other"` |
| `sr-Cyrl-BG/largeInt` | `"9.007.199.254.740.993"` | `"9.007.199.254.740.993"` |
| `sr-Cyrl-BG/longCompact` | `"1,23 хиљаде"` | `"1,23 хиљаде"` |
| `sr-Cyrl-BG/longDate` | `"Date 10. јул 2024."` | `"Date 10. јул 2024."` |
| `sr-Cyrl-BG/merged/-1` | `"other"` | `"other"` |
| `sr-Cyrl-BG/merged/0` | `"other"` | `"other"` |
| `sr-Cyrl-BG/merged/1` | `"last"` | `"last"` |
| `sr-Cyrl-BG/merged/1.1` | `"last"` | `"last"` |
| `sr-Cyrl-BG/merged/2` | `"two-last"` | `"two-last"` |
| `sr-Cyrl-BG/merged/21` | `"last"` | `"last"` |
| `sr-Cyrl-BG/money` | `"Money 1.234,50\u00a0$"` | `"Money 1.234,50\u00a0$"` |
| `sr-Cyrl-BG/multi` | `"B then 7"` | `"B then 7"` |
| `sr-Cyrl-BG/namespace` | `"Namespace"` | `"Namespace"` |
| `sr-Cyrl-BG/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `sr-Cyrl-BG/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `sr-Cyrl-BG/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `sr-Cyrl-BG/nested/1/female` | `"Her item"` | `"Her item"` |
| `sr-Cyrl-BG/nested/1/male` | `"His item"` | `"His item"` |
| `sr-Cyrl-BG/nested/1/other` | `"Their item"` | `"Their item"` |
| `sr-Cyrl-BG/nested/2/female` | `"Her items"` | `"Her items"` |
| `sr-Cyrl-BG/nested/2/male` | `"His items"` | `"His items"` |
| `sr-Cyrl-BG/nested/2/other` | `"Their items"` | `"Their items"` |
| `sr-Cyrl-BG/number` | `"Number 12.345,67"` | `"Number 12.345,67"` |
| `sr-Cyrl-BG/omit` | `"Bez imena"` | `"Bez imena"` |
| `sr-Cyrl-BG/parentText` | `"Sr parent"` | `"Sr parent"` |
| `sr-Cyrl-BG/pound` | `"# items"` | `"# items"` |
| `sr-Cyrl-BG/quarter` | `"трећи квартал 2024."` | `"трећи квартал 2024."` |
| `sr-Cyrl-BG/rawNum/double` | `"1.0"` | `"1.0"` |
| `sr-Cyrl-BG/rawNum/int` | `"1"` | `"1"` |
| `sr-Cyrl-BG/select/female` | `"She"` | `"She"` |
| `sr-Cyrl-BG/select/male` | `"He"` | `"He"` |
| `sr-Cyrl-BG/select/other` | `"They"` | `"They"` |
| `sr-Cyrl-BG/select/suffix` | `"He"` | `"He"` |
| `sr-Cyrl-BG/select/unknown` | `"They"` | `"They"` |
| `sr-Cyrl-BG/shortCurrency` | `"1,23\u00a0хиљ.\u00a0RSD"` | `"1,23\u00a0хиљ.\u00a0RSD"` |
| `sr-Cyrl-BG/time_format` | `"24-hour"` | `"24-hour"` |
| `sr-Cyrl-BG/unused` | `"ready"` | `"ready"` |
| `sr-Cyrl-RS/checked` | `"Checked"` | `"Checked"` |
| `sr-Cyrl-RS/combinedDate` | `"10. 7. 2024. 15:04:05"` | `"10. 7. 2024. 15:04:05"` |
| `sr-Cyrl-RS/compact` | `"Count 1,2\u00a0хиљ."` | `"Count 1,2\u00a0хиљ."` |
| `sr-Cyrl-RS/currencyDefault` | `"1.235\u00a0RSD"` | `"1.235\u00a0RSD"` |
| `sr-Cyrl-RS/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `sr-Cyrl-RS/date` | `"Date 10. 7. 2024."` | `"Date 10. 7. 2024."` |
| `sr-Cyrl-RS/duplicateSelect` | `"last"` | `"last"` |
| `sr-Cyrl-RS/empty` | `""` | `""` |
| `sr-Cyrl-RS/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `sr-Cyrl-RS/event` | `"Event"` | `"Event"` |
| `sr-Cyrl-RS/fallback` | `"Template fallback"` | `"Template fallback"` |
| `sr-Cyrl-RS/fallbackPlural` | `"English one"` | `"English one"` |
| `sr-Cyrl-RS/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `sr-Cyrl-RS/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `sr-Cyrl-RS/hello` | `"Country parent"` | `"Country parent"` |
| `sr-Cyrl-RS/implicit` | `"7:true"` | `"7:true"` |
| `sr-Cyrl-RS/inlineDate` | `"10. јул 2024."` | `"10. јул 2024."` |
| `sr-Cyrl-RS/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `sr-Cyrl-RS/items/-1` | `"other"` | `"other"` |
| `sr-Cyrl-RS/items/0` | `"empty"` | `"empty"` |
| `sr-Cyrl-RS/items/1` | `"one"` | `"one"` |
| `sr-Cyrl-RS/items/1.2` | `"few"` | `"few"` |
| `sr-Cyrl-RS/items/101` | `"one"` | `"one"` |
| `sr-Cyrl-RS/items/11` | `"other"` | `"other"` |
| `sr-Cyrl-RS/items/2` | `"few"` | `"few"` |
| `sr-Cyrl-RS/items/2.1` | `"one"` | `"one"` |
| `sr-Cyrl-RS/items/21` | `"one"` | `"one"` |
| `sr-Cyrl-RS/items/22` | `"few"` | `"few"` |
| `sr-Cyrl-RS/items/3` | `"few"` | `"few"` |
| `sr-Cyrl-RS/items/4` | `"few"` | `"few"` |
| `sr-Cyrl-RS/items/5` | `"other"` | `"other"` |
| `sr-Cyrl-RS/largeInt` | `"9.007.199.254.740.993"` | `"9.007.199.254.740.993"` |
| `sr-Cyrl-RS/longCompact` | `"1,23 хиљаде"` | `"1,23 хиљаде"` |
| `sr-Cyrl-RS/longDate` | `"Date 10. јул 2024."` | `"Date 10. јул 2024."` |
| `sr-Cyrl-RS/merged/-1` | `"other"` | `"other"` |
| `sr-Cyrl-RS/merged/0` | `"other"` | `"other"` |
| `sr-Cyrl-RS/merged/1` | `"last"` | `"last"` |
| `sr-Cyrl-RS/merged/1.1` | `"last"` | `"last"` |
| `sr-Cyrl-RS/merged/2` | `"two-last"` | `"two-last"` |
| `sr-Cyrl-RS/merged/21` | `"last"` | `"last"` |
| `sr-Cyrl-RS/money` | `"Money 1.234,50\u00a0$"` | `"Money 1.234,50\u00a0$"` |
| `sr-Cyrl-RS/multi` | `"B then 7"` | `"B then 7"` |
| `sr-Cyrl-RS/namespace` | `"Namespace"` | `"Namespace"` |
| `sr-Cyrl-RS/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `sr-Cyrl-RS/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `sr-Cyrl-RS/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `sr-Cyrl-RS/nested/1/female` | `"Her item"` | `"Her item"` |
| `sr-Cyrl-RS/nested/1/male` | `"His item"` | `"His item"` |
| `sr-Cyrl-RS/nested/1/other` | `"Their item"` | `"Their item"` |
| `sr-Cyrl-RS/nested/2/female` | `"Her items"` | `"Her items"` |
| `sr-Cyrl-RS/nested/2/male` | `"His items"` | `"His items"` |
| `sr-Cyrl-RS/nested/2/other` | `"Their items"` | `"Their items"` |
| `sr-Cyrl-RS/number` | `"Number 12.345,67"` | `"Number 12.345,67"` |
| `sr-Cyrl-RS/omit` | `"Bez imena"` | `"Bez imena"` |
| `sr-Cyrl-RS/parentText` | `"Sr parent"` | `"Sr parent"` |
| `sr-Cyrl-RS/pound` | `"# items"` | `"# items"` |
| `sr-Cyrl-RS/quarter` | `"трећи квартал 2024."` | `"трећи квартал 2024."` |
| `sr-Cyrl-RS/rawNum/double` | `"1.0"` | `"1.0"` |
| `sr-Cyrl-RS/rawNum/int` | `"1"` | `"1"` |
| `sr-Cyrl-RS/select/female` | `"She"` | `"She"` |
| `sr-Cyrl-RS/select/male` | `"He"` | `"He"` |
| `sr-Cyrl-RS/select/other` | `"They"` | `"They"` |
| `sr-Cyrl-RS/select/suffix` | `"He"` | `"He"` |
| `sr-Cyrl-RS/select/unknown` | `"They"` | `"They"` |
| `sr-Cyrl-RS/shortCurrency` | `"1,23\u00a0хиљ.\u00a0RSD"` | `"1,23\u00a0хиљ.\u00a0RSD"` |
| `sr-Cyrl-RS/time_format` | `"24-hour"` | `"24-hour"` |
| `sr-Cyrl-RS/unused` | `"ready"` | `"ready"` |
| `sr-Latn-BA/checked` | `"Checked"` | `"Checked"` |
| `sr-Latn-BA/combinedDate` | `"10. 7. 2024. 15:04:05"` | `"10. 7. 2024. 15:04:05"` |
| `sr-Latn-BA/compact` | `"Count 1,2\u00a0hilj."` | `"Count 1,2\u00a0hilj."` |
| `sr-Latn-BA/currencyDefault` | `"1.235\u00a0RSD"` | `"1.235\u00a0RSD"` |
| `sr-Latn-BA/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `sr-Latn-BA/date` | `"Date 10. 7. 2024."` | `"Date 10. 7. 2024."` |
| `sr-Latn-BA/duplicateSelect` | `"last"` | `"last"` |
| `sr-Latn-BA/empty` | `""` | `""` |
| `sr-Latn-BA/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `sr-Latn-BA/event` | `"Event"` | `"Event"` |
| `sr-Latn-BA/fallback` | `"Template fallback"` | `"Template fallback"` |
| `sr-Latn-BA/fallbackPlural` | `"English one"` | `"English one"` |
| `sr-Latn-BA/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `sr-Latn-BA/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `sr-Latn-BA/hello` | `"Latin parent"` | `"Latin parent"` |
| `sr-Latn-BA/implicit` | `"7:true"` | `"7:true"` |
| `sr-Latn-BA/inlineDate` | `"10. jul 2024."` | `"10. jul 2024."` |
| `sr-Latn-BA/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `sr-Latn-BA/items/-1` | `"other"` | `"other"` |
| `sr-Latn-BA/items/0` | `"empty"` | `"empty"` |
| `sr-Latn-BA/items/1` | `"one"` | `"one"` |
| `sr-Latn-BA/items/1.2` | `"few"` | `"few"` |
| `sr-Latn-BA/items/101` | `"one"` | `"one"` |
| `sr-Latn-BA/items/11` | `"other"` | `"other"` |
| `sr-Latn-BA/items/2` | `"few"` | `"few"` |
| `sr-Latn-BA/items/2.1` | `"one"` | `"one"` |
| `sr-Latn-BA/items/21` | `"one"` | `"one"` |
| `sr-Latn-BA/items/22` | `"few"` | `"few"` |
| `sr-Latn-BA/items/3` | `"few"` | `"few"` |
| `sr-Latn-BA/items/4` | `"few"` | `"few"` |
| `sr-Latn-BA/items/5` | `"other"` | `"other"` |
| `sr-Latn-BA/largeInt` | `"9.007.199.254.740.993"` | `"9.007.199.254.740.993"` |
| `sr-Latn-BA/longCompact` | `"1,23 hiljade"` | `"1,23 hiljade"` |
| `sr-Latn-BA/longDate` | `"Date 10. jul 2024."` | `"Date 10. jul 2024."` |
| `sr-Latn-BA/merged/-1` | `"other"` | `"other"` |
| `sr-Latn-BA/merged/0` | `"other"` | `"other"` |
| `sr-Latn-BA/merged/1` | `"last"` | `"last"` |
| `sr-Latn-BA/merged/1.1` | `"last"` | `"last"` |
| `sr-Latn-BA/merged/2` | `"two-last"` | `"two-last"` |
| `sr-Latn-BA/merged/21` | `"last"` | `"last"` |
| `sr-Latn-BA/money` | `"Money 1.234,50\u00a0$"` | `"Money 1.234,50\u00a0$"` |
| `sr-Latn-BA/multi` | `"B then 7"` | `"B then 7"` |
| `sr-Latn-BA/namespace` | `"Namespace"` | `"Namespace"` |
| `sr-Latn-BA/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `sr-Latn-BA/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `sr-Latn-BA/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `sr-Latn-BA/nested/1/female` | `"Her item"` | `"Her item"` |
| `sr-Latn-BA/nested/1/male` | `"His item"` | `"His item"` |
| `sr-Latn-BA/nested/1/other` | `"Their item"` | `"Their item"` |
| `sr-Latn-BA/nested/2/female` | `"Her items"` | `"Her items"` |
| `sr-Latn-BA/nested/2/male` | `"His items"` | `"His items"` |
| `sr-Latn-BA/nested/2/other` | `"Their items"` | `"Their items"` |
| `sr-Latn-BA/number` | `"Number 12.345,67"` | `"Number 12.345,67"` |
| `sr-Latn-BA/omit` | `"Bez imena"` | `"Bez imena"` |
| `sr-Latn-BA/parentText` | `"Sr parent"` | `"Sr parent"` |
| `sr-Latn-BA/pound` | `"# items"` | `"# items"` |
| `sr-Latn-BA/quarter` | `"treći kvartal 2024."` | `"treći kvartal 2024."` |
| `sr-Latn-BA/rawNum/double` | `"1.0"` | `"1.0"` |
| `sr-Latn-BA/rawNum/int` | `"1"` | `"1"` |
| `sr-Latn-BA/select/female` | `"She"` | `"She"` |
| `sr-Latn-BA/select/male` | `"He"` | `"He"` |
| `sr-Latn-BA/select/other` | `"They"` | `"They"` |
| `sr-Latn-BA/select/suffix` | `"He"` | `"He"` |
| `sr-Latn-BA/select/unknown` | `"They"` | `"They"` |
| `sr-Latn-BA/shortCurrency` | `"1,23\u00a0hilj.\u00a0RSD"` | `"1,23\u00a0hilj.\u00a0RSD"` |
| `sr-Latn-BA/time_format` | `"24-hour"` | `"24-hour"` |
| `sr-Latn-BA/unused` | `"ready"` | `"ready"` |
| `sr-Latn-RS/checked` | `"Checked"` | `"Checked"` |
| `sr-Latn-RS/combinedDate` | `"10. 7. 2024. 15:04:05"` | `"10. 7. 2024. 15:04:05"` |
| `sr-Latn-RS/compact` | `"Count 1,2\u00a0хиљ."` | `"Count 1,2\u00a0хиљ."` |
| `sr-Latn-RS/currencyDefault` | `"1.235\u00a0RSD"` | `"1.235\u00a0RSD"` |
| `sr-Latn-RS/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `sr-Latn-RS/date` | `"Date 10. 7. 2024."` | `"Date 10. 7. 2024."` |
| `sr-Latn-RS/duplicateSelect` | `"last"` | `"last"` |
| `sr-Latn-RS/empty` | `""` | `""` |
| `sr-Latn-RS/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `sr-Latn-RS/event` | `"Event"` | `"Event"` |
| `sr-Latn-RS/fallback` | `"Template fallback"` | `"Template fallback"` |
| `sr-Latn-RS/fallbackPlural` | `"English one"` | `"English one"` |
| `sr-Latn-RS/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `sr-Latn-RS/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `sr-Latn-RS/hello` | `"Zdravo"` | `"Zdravo"` |
| `sr-Latn-RS/implicit` | `"7:true"` | `"7:true"` |
| `sr-Latn-RS/inlineDate` | `"10. јул 2024."` | `"10. јул 2024."` |
| `sr-Latn-RS/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `sr-Latn-RS/items/-1` | `"other"` | `"other"` |
| `sr-Latn-RS/items/0` | `"empty"` | `"empty"` |
| `sr-Latn-RS/items/1` | `"one"` | `"one"` |
| `sr-Latn-RS/items/1.2` | `"few"` | `"few"` |
| `sr-Latn-RS/items/101` | `"one"` | `"one"` |
| `sr-Latn-RS/items/11` | `"other"` | `"other"` |
| `sr-Latn-RS/items/2` | `"few"` | `"few"` |
| `sr-Latn-RS/items/2.1` | `"one"` | `"one"` |
| `sr-Latn-RS/items/21` | `"one"` | `"one"` |
| `sr-Latn-RS/items/22` | `"few"` | `"few"` |
| `sr-Latn-RS/items/3` | `"few"` | `"few"` |
| `sr-Latn-RS/items/4` | `"few"` | `"few"` |
| `sr-Latn-RS/items/5` | `"other"` | `"other"` |
| `sr-Latn-RS/largeInt` | `"9.007.199.254.740.993"` | `"9.007.199.254.740.993"` |
| `sr-Latn-RS/longCompact` | `"1,23 хиљаде"` | `"1,23 хиљаде"` |
| `sr-Latn-RS/longDate` | `"Date 10. јул 2024."` | `"Date 10. јул 2024."` |
| `sr-Latn-RS/merged/-1` | `"other"` | `"other"` |
| `sr-Latn-RS/merged/0` | `"other"` | `"other"` |
| `sr-Latn-RS/merged/1` | `"last"` | `"last"` |
| `sr-Latn-RS/merged/1.1` | `"last"` | `"last"` |
| `sr-Latn-RS/merged/2` | `"two-last"` | `"two-last"` |
| `sr-Latn-RS/merged/21` | `"last"` | `"last"` |
| `sr-Latn-RS/money` | `"Money 1.234,50\u00a0$"` | `"Money 1.234,50\u00a0$"` |
| `sr-Latn-RS/multi` | `"B then 7"` | `"B then 7"` |
| `sr-Latn-RS/namespace` | `"Namespace"` | `"Namespace"` |
| `sr-Latn-RS/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `sr-Latn-RS/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `sr-Latn-RS/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `sr-Latn-RS/nested/1/female` | `"Her item"` | `"Her item"` |
| `sr-Latn-RS/nested/1/male` | `"His item"` | `"His item"` |
| `sr-Latn-RS/nested/1/other` | `"Their item"` | `"Their item"` |
| `sr-Latn-RS/nested/2/female` | `"Her items"` | `"Her items"` |
| `sr-Latn-RS/nested/2/male` | `"His items"` | `"His items"` |
| `sr-Latn-RS/nested/2/other` | `"Their items"` | `"Their items"` |
| `sr-Latn-RS/number` | `"Number 12.345,67"` | `"Number 12.345,67"` |
| `sr-Latn-RS/omit` | `"Bez imena"` | `"Bez imena"` |
| `sr-Latn-RS/parentText` | `"Sr parent"` | `"Sr parent"` |
| `sr-Latn-RS/pound` | `"# items"` | `"# items"` |
| `sr-Latn-RS/quarter` | `"трећи квартал 2024."` | `"трећи квартал 2024."` |
| `sr-Latn-RS/rawNum/double` | `"1.0"` | `"1.0"` |
| `sr-Latn-RS/rawNum/int` | `"1"` | `"1"` |
| `sr-Latn-RS/select/female` | `"She"` | `"She"` |
| `sr-Latn-RS/select/male` | `"He"` | `"He"` |
| `sr-Latn-RS/select/other` | `"They"` | `"They"` |
| `sr-Latn-RS/select/suffix` | `"He"` | `"He"` |
| `sr-Latn-RS/select/unknown` | `"They"` | `"They"` |
| `sr-Latn-RS/shortCurrency` | `"1,23\u00a0хиљ.\u00a0RSD"` | `"1,23\u00a0хиљ.\u00a0RSD"` |
| `sr-Latn-RS/time_format` | `"24-hour"` | `"24-hour"` |
| `sr-Latn-RS/unused` | `"ready"` | `"ready"` |
| `sr/checked` | `"Checked"` | `"Checked"` |
| `sr/combinedDate` | `"10. 7. 2024. 15:04:05"` | `"10. 7. 2024. 15:04:05"` |
| `sr/compact` | `"Count 1,2\u00a0хиљ."` | `"Count 1,2\u00a0хиљ."` |
| `sr/currencyDefault` | `"1.235\u00a0RSD"` | `"1.235\u00a0RSD"` |
| `sr/customDate` | `"2024 year 07-10 15:04:05.1230"` | `"2024 year 07-10 15:04:05.1230"` |
| `sr/date` | `"Date 10. 7. 2024."` | `"Date 10. 7. 2024."` |
| `sr/duplicateSelect` | `"last"` | `"last"` |
| `sr/empty` | `""` | `""` |
| `sr/escaped` | `"Use {name} and it's safe"` | `"Use {name} and it's safe"` |
| `sr/event` | `"Event"` | `"Event"` |
| `sr/fallback` | `"Template fallback"` | `"Template fallback"` |
| `sr/fallbackPlural` | `"English one"` | `"English one"` |
| `sr/formatStringIgnored` | `"Hello"` | `"Hello"` |
| `sr/greeting` | `"Hello Ada!"` | `"Hello Ada!"` |
| `sr/hello` | `"Здраво"` | `"Здраво"` |
| `sr/implicit` | `"7:true"` | `"7:true"` |
| `sr/inlineDate` | `"10. јул 2024."` | `"10. јул 2024."` |
| `sr/inlineTime` | `"15:04:05"` | `"15:04:05"` |
| `sr/items/-1` | `"other"` | `"other"` |
| `sr/items/0` | `"empty"` | `"empty"` |
| `sr/items/1` | `"one"` | `"one"` |
| `sr/items/1.2` | `"few"` | `"few"` |
| `sr/items/101` | `"one"` | `"one"` |
| `sr/items/11` | `"other"` | `"other"` |
| `sr/items/2` | `"few"` | `"few"` |
| `sr/items/2.1` | `"one"` | `"one"` |
| `sr/items/21` | `"one"` | `"one"` |
| `sr/items/22` | `"few"` | `"few"` |
| `sr/items/3` | `"few"` | `"few"` |
| `sr/items/4` | `"few"` | `"few"` |
| `sr/items/5` | `"other"` | `"other"` |
| `sr/largeInt` | `"9.007.199.254.740.993"` | `"9.007.199.254.740.993"` |
| `sr/longCompact` | `"1,23 хиљаде"` | `"1,23 хиљаде"` |
| `sr/longDate` | `"Date 10. јул 2024."` | `"Date 10. јул 2024."` |
| `sr/merged/-1` | `"other"` | `"other"` |
| `sr/merged/0` | `"other"` | `"other"` |
| `sr/merged/1` | `"last"` | `"last"` |
| `sr/merged/1.1` | `"last"` | `"last"` |
| `sr/merged/2` | `"two-last"` | `"two-last"` |
| `sr/merged/21` | `"last"` | `"last"` |
| `sr/money` | `"Money 1.234,50\u00a0$"` | `"Money 1.234,50\u00a0$"` |
| `sr/multi` | `"B then 7"` | `"B then 7"` |
| `sr/namespace` | `"Namespace"` | `"Namespace"` |
| `sr/nested/0/female` | `"Nobody"` | `"Nobody"` |
| `sr/nested/0/male` | `"Nobody"` | `"Nobody"` |
| `sr/nested/0/other` | `"Nobody"` | `"Nobody"` |
| `sr/nested/1/female` | `"Her item"` | `"Her item"` |
| `sr/nested/1/male` | `"His item"` | `"His item"` |
| `sr/nested/1/other` | `"Their item"` | `"Their item"` |
| `sr/nested/2/female` | `"Her items"` | `"Her items"` |
| `sr/nested/2/male` | `"His items"` | `"His items"` |
| `sr/nested/2/other` | `"Their items"` | `"Their items"` |
| `sr/number` | `"Number 12.345,67"` | `"Number 12.345,67"` |
| `sr/omit` | `"Bez imena"` | `"Bez imena"` |
| `sr/parentText` | `"Sr parent"` | `"Sr parent"` |
| `sr/pound` | `"# items"` | `"# items"` |
| `sr/quarter` | `"трећи квартал 2024."` | `"трећи квартал 2024."` |
| `sr/rawNum/double` | `"1.0"` | `"1.0"` |
| `sr/rawNum/int` | `"1"` | `"1"` |
| `sr/select/female` | `"She"` | `"She"` |
| `sr/select/male` | `"He"` | `"He"` |
| `sr/select/other` | `"They"` | `"They"` |
| `sr/select/suffix` | `"He"` | `"He"` |
| `sr/select/unknown` | `"They"` | `"They"` |
| `sr/shortCurrency` | `"1,23\u00a0хиљ.\u00a0RSD"` | `"1,23\u00a0хиљ.\u00a0RSD"` |
| `sr/time_format` | `"24-hour"` | `"24-hour"` |
| `sr/unused` | `"ready"` | `"ready"` |
| `zz/lookup-error` | `"unsupported"` | `"unsupported"` |

## ARB acceptance / rejection

| Case | Flutter + Dart analysis | C# compiler |
| --- | --- | --- |
| plain | accept | accept |
| empty | accept | accept |
| infer-alphabetical | accept | accept |
| metadata-order | accept | accept |
| infer-plural | accept | accept |
| infer-select | accept | accept |
| unused-declared | accept | accept |
| omitted-in-translation | accept | accept |
| new-in-translation | accept | accept |
| locale-meta-type-mismatch | reject | reject |
| translation-only-key | accept | accept |
| orphan-metadata | accept | accept |
| null-translation | accept | accept |
| null-template | reject | reject |
| duplicate-json | accept | accept |
| duplicate-plural | accept | accept |
| merged-exact | accept | accept |
| exact42 | reject | reject |
| missing-other | reject | reject |
| plural-double | reject | reject |
| select-int | reject | reject |
| unknown-type | reject | reject |
| invalid-key | reject | reject |
| dart-keywords | reject | reject |
| csharp-keywords | accept | accept |
| date-inline | accept | accept |
| time-inline | accept | accept |
| date-missing-format | reject | reject |
| date-custom | accept | accept |
| format-on-string | accept | accept |
| ignored-positional-options | accept | accept |
| compact-digits-invalid | reject | reject |
| currency-custom | accept | accept |
| escaping | accept | accept |
| escaping-broken | reject | reject |
| relaxed | accept | accept |
| required-metadata | reject | reject |
| locale-conflict | reject | reject |
| missing-base-locale | reject | reject |
| broken | reject | reject |
| spaced-exact-plural | accept | accept |
| select-pipe | accept | accept |
| select-numeric-prefix | reject | reject |
| select-numeric | accept | accept |
| select-reserved-plural | reject | reject |
| select-reserved-select | reject | reject |
| select-reserved-date | reject | reject |
| select-reserved-time | reject | reject |

## intl runtime differential suite

**94,103/94,103 cases match.** The complete machine-readable side is `intl-oracle.json`; `tests/IntlTests` executes and compares each C# result. Includes every locale in the pinned date/number/plural tables, all 11 number formats, all gen-l10n date skeletons, custom/combined date patterns, currency options, negative/large/tiny numbers, native int64 values, NaN/infinity and error outcomes.

Run `python3 scripts/verify.py` to recreate the oracle and rerun all comparisons. No mismatch baseline is accepted.
