# DotNative.Localization

A standalone .NET implementation of Flutter's typed ARB localization pipeline. The behavior target is **Flutter 3.41.0 (`44a626f4f0`), Dart VM 3.11.0 and intl 0.20.2**. Flutter's implementation and executable output are the specification. There is no expected-difference allowlist.

This repository is a sibling of `dotNative`. It has no DotNative or UI toolkit dependency. Runtime: .NET 8+. Generator: netstandard2.0 / Roslyn 4.8+. A packaged build-time report tool requires the .NET 8 runtime. The consuming app needs neither Flutter nor Dart.

## Install and use

```xml
<ItemGroup>
  <PackageReference Include="DotNative.Localization" Version="0.1.0-preview.2" />
  <AdditionalFiles Include="l10n/*.arb" />
</ItemGroup>
```

The package automatically includes the project’s `l10n.json` as an AdditionalFile:

```json
{
  "arb-dir": "l10n",
  "template-arb-file": "app_en.arb",
  "output-class": "AppLocalizations",
  "namespace": "MyApp.Localization",
  "preferred-supported-locales": ["en", "sr"],
  "untranslated-messages-file": "obj/untranslated.json",
  "use-escaping": true
}
```

```json
{
  "@@locale": "en",
  "hello": "Hello",
  "greeting": "Hello {name}!",
  "@greeting": { "placeholders": { "name": { "type": "String" } } },
  "items": "{count, plural, =0{Empty} one{One item} other{{count} items}}",
  "@items": { "placeholders": { "count": { "type": "int" } } }
}
```

```csharp
var text = AppLocalizations.ForLocale("sr-Latn-RS");
// Also accepts CultureInfo.
Console.WriteLine(text.Hello);
Console.WriteLine(text.Greeting(name: "Ada"));
Console.WriteLine(text.Items(count: 2));
```

Messages become get-only properties or typed methods on a generated abstract base with concrete locale classes. No string indexer, `Get(string)`, runtime ARB loading, RESX, `IStringLocalizer`, reflection-based dispatch, or satellite assemblies. All message strings and ICU branches are compiled into the consuming assembly.

## Flutter semantics

- Template metadata insertion order determines parameters. Undeclared placeholders inferred from all translations follow in alphabetical order. Translation-only message keys do not add members.
- Missing base-locale messages use the template. Missing variant messages inherit the language’s translation. Formatting still uses the selected locale.
- Lookup order: full language/script/country → matching language/script → matching language/country → language. An unsupported language throws, just like Flutter’s generated lookup. Flutter widget-level locale negotiation is a separate API and is not implemented here.
- Empty translations are preserved. Translations may omit placeholders. Unused declared placeholders remain parameters.
- `#` is literal text. Use `{count}` for interpolation. Only `=0`, `=1`, `=2` are accepted and merge with zero/one/two; the last equivalent plural branch wins.
- Plural selection uses the **actual intl 0.20.2 predicates and Dart VM numeric semantics**, including negative counts, explicit zero/one/two, and two→few fallback. Nested branches and metadata formatting are evaluated eagerly as in generated Dart.
- Date and number formatting use **vendored intl tables and a C# port of its formatting algorithms**, not approximations based on OS culture data. All intl locales, 11 gen-l10n number formats, date skeletons, composed/custom date patterns and currency tables are included.

The previous preview’s general `=N`, pound substitution, template fallback for unsupported languages, strict placeholder-set equality and first-appearance parameter order have been removed.

## C# API mapping

ARB member names map to PascalCase: `time_format`→`TimeFormat`, `event`→`Event`. A deterministic collision suffix is `__` plus the original UTF-8 key in lowercase hex, applied to every colliding member (including infrastructure names). Key order never changes the mapping; adding a colliding key is an API change. `[ArbKey("time_format")]` preserves the original name on each member. The runtime never reflects over that attribute.

Dart-invalid identifiers are rejected. `class` and `continue` are **not** usable ARB keys: real Flutter emits invalid Dart for them. C# keywords that are valid Dart names, such as `event`, `namespace`, and `checked`, are tested.

| Dart placeholder | C# parameter |
| --- | --- |
| `String` | `string` |
| `int` | `long` (native Dart VM int64) |
| `num` | `L10nNumber`, with implicit conversions from int/long/double |
| `double` | `double` |
| `DateTime` | `System.DateTime` |
| `Object` | `object` |

`L10nNumber` retains integer versus double identity: interpolation of `1` is `"1"`, while `1.0` is `"1.0"`. Integer formatting preserves all 64 bits. C# named arguments use placeholder names.

## Configuration and diagnostics

Paths resolve relative to `l10n.json`. Like Flutter, only ARBs directly in `arb-dir` are inputs; mark them as AdditionalFiles. Locale comes from `@@locale` or the filename. Conflicting locale declarations and variants without a base-language ARB are errors.

Supported options: `arb-dir`, `template-arb-file`, `output-class`, `namespace`, `header`, `preferred-supported-locales`, `untranslated-messages-file`, `use-escaping`, `relax-syntax`, `required-resource-attributes`, `use-named-parameters`, `nullable-getter`, `suppress-warnings`.

`use-named-parameters` does not change C# signatures: C# already supports both call forms. `nullable-getter` only controls Flutter’s `of(BuildContext)` helper, which has no counterpart here; message properties always return nonnullable strings. `suppress-warnings` is accepted; correctness diagnostics remain errors. Unknown configuration fields are rejected. Dart source layout, synthetic-package and deferred-import options are not C# localization behaviors; see [COMPATIBILITY.md](COMPATIBILITY.md).

Errors are `L10N001` with file/key context. Duplicate JSON properties have last-value semantics, matching Dart JSON decoding. Flutter acceptance/rejection and actual generated C# compilation are compared for the validation fixtures, rather than enforcing extra rules from the original request.

The untranslated report is written after successful compilation by a packaged build-time CLI sharing the compiler. No source-generator filesystem side effects or design-time report writes. Its JSON contents are compared with Flutter’s report.

## Verification

```sh
python3 scripts/verify.py
```

The script reruns real Flutter generation and Dart analysis, the generated-message oracle, the complete intl differential suite, compiler tests, package-only consumption, broken-ARB build rejection, and NativeAOT publish/run. It requires the pinned Flutter/Dart SDK, .NET SDK/runtime, Python 3, and a NativeAOT toolchain. Any differing successful string or accept/reject outcome fails verification.

[Side-by-side results](fixtures/results/comparison.md) · [Compatibility contract](COMPATIBILITY.md) · [Provenance and licenses](ORIGIN.md)

`python3 scripts/generate_intl.py` regenerates C# tables and predicates offline from vendored inputs. `fixtures/flutter/tool/export_intl.dart` refreshes the table export from the pinned intl package when intentionally updating the compatibility target.
