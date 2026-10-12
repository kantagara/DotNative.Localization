# Compatibility contract

The target is **Flutter 3.41.0 / Dart native VM 3.11.0 / intl 0.20.2**, not a generic interpretation of ICU or the latest CLDR. The installed versions and actual tool output are captured under `fixtures/results`. Changing this target requires rerunning the differential suite. Dart’s JavaScript numeric model is a different target.

## No approved mismatches

`compare.py` requires identical generated-message output and identical missing-message reports. `IntlTests` requires every supported input to produce the same string or both implementations to throw. `CompilerTests` compares ARB acceptance/rejection against real `flutter gen-l10n` followed by Dart static analysis. There is no expected-differences file.

Runtime data is exported from intl, including all date symbols/patterns, all number and compact tables, simple currency symbols and currency fraction digits. Plural predicates are translated from the pinned Dart source, preserving signed operands, Dart modulo, precision and native integer behavior. Formatting also reproduces native VM saturation/overflow behavior exposed by the extreme-value fixtures; it does not substitute mathematically preferable output.

## Supported message behavior

Plain text and empty strings; interpolation; plurals and selects; nested messages; apostrophe escaping; relaxed syntax; inline `{date, date, ::yMd}` and `{date, time, ::Hm}`; inferred and declared placeholders; locale formatting overrides; missing translations and language subclass inheritance; full/script/country/language lookup; untranslated reports; source-located errors.

All 11 gen-l10n number factories are implemented: `compact`, `compactCurrency`, `compactSimpleCurrency`, `compactLong`, `currency`, `decimalPattern`, `decimalPatternDigits`, `decimalPercentPattern`, `percentPattern`, `scientificPattern`, `simpleCurrency`.

All date factory names accepted by the pinned gen-l10n are implemented through the same patterns: `d E EEEE LLL LLLL M Md MEd MMM MMMd MMMEd MMMM MMMMd MMMMEEEEd QQQ QQQQ y yM yMd yMEd yMMM yMMMd yMMMEd yMMMM yMMMMd yMMMMEEEEd yQQQ yQQQQ H Hm Hms j jm jms jmv jmz jv jz m ms s`. This includes intl’s own unfinished timezone-skeleton behavior; the implementation reproduces what intl returns rather than inventing timezone output. `+` composition and `isCustomDateFormat` use the same token rules, quarter names, standalone names, digit shapes and literal quoting.

Number `optionalParameters` follow each actual factory signature. `name`, `symbol`, `decimalDigits`, and `customPattern` are forwarded where applicable. Positional factories ignore optional parameters, as Flutter does. An invalid named argument is a compile-time error. The bool `explicitSign` field is not usable through this pinned gen-l10n because its emitter quotes nonnumeric optional values; the C# compiler rejects it as the corresponding Dart compilation does.

## Necessary host-language boundaries

- C# member spelling and types follow README’s deterministic mapping. Dart-invalid keys fail instead of becoming a new C#-only extension.
- `num` is represented by `L10nNumber`, retaining native int64/double identity. `int` is int64. Custom `Object` values use the caller’s C# `ToString`; the library cannot execute a user’s Dart class implementation.
- `DateTime` parameters use .NET `DateTime` (years 1–9999). Dart-only dates outside that type’s range cannot be supplied. Local/UTC kind is not converted before formatting, matching intl’s field-based formatting.
- CLR and Dart exception classes differ. Differential tests compare success versus failure, and compare every successful string byte-for-byte. Diagnostic wording follows C# tooling, while file/key and failure causes are preserved.
- UI APIs (`BuildContext`, localization delegates, widget locale negotiation) remain outside this standalone library. `ForLocale` corresponds to the generated lookup function, **not** widget-level fallback negotiation.
- Source files, namespace, analyzer packaging and generated-class names are C# artifacts. Dart-only output paths/imports, synthetic packages and deferred Dart loading are not implemented; those options are rejected rather than silently changing localization behavior.

These boundaries are explicit API/platform constraints, not allowances for different localized strings.

## Evidence and coverage

See [the generated comparison report](fixtures/results/comparison.md) for the exact executed totals, API signatures and every ARB observation. The separate `intl-oracle.json` records the full runtime matrix and is replayed by C#. The matrix covers every locale in the pinned tables, all listed formats, custom and combined dates, currency variants, signed/fractional counts, int64 values beyond double’s exact range, compact thresholds, rounding, tiny/huge finite doubles and nonfinite values.

Tests establish behavior for their inputs; they do not mathematically prove equivalence for all possible ARB files and all floating-point values. A newly found difference is a bug and must become a failing differential test, not a documented acceptable difference.
