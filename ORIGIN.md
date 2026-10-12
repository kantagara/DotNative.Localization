# Provenance

Independent .NET library and source generator with behavior targeted to Flutter **3.41.0**, framework revision **44a626f4f0**, Dart native VM **3.11.0**, and Dart intl **0.20.2**. Flutter is the source of truth when an earlier requirement disagrees with actual behavior.

Behavioral reference: Flutter’s `packages/flutter_tools/lib/src/localizations/gen_l10n.dart`, `gen_l10n_types.dart`, `message_parser.dart`, and their emitted Dart. The executable oracle invokes the real installed Flutter tool and Dart runtime. No UI toolkit is linked into the .NET app.

The runtime ports the relevant formatting algorithms from Dart intl 0.20.2. `vendor/intl/data.json` is a deterministic export of its complete date/number/compact/currency tables. `vendor/intl/plural_rules.dart` is its original plural-rule source, mechanically translated to thread-safe C# predicates by `scripts/generate_intl.py`. The same script compiles the exported data into static C# initializers. There is no runtime JSON or ARB parser.

Upstream package: https://pub.dev/packages/intl/versions/0.20.2

Upstream repository: https://github.com/dart-lang/i18n

Dart intl code and data carry BSD-3-Clause terms, retained in `vendor/intl/LICENSE`. Generated/ported files identify their provenance. Unicode-derived locale data is additionally accompanied by `vendor/UNICODE-LICENSE.txt`. Authored library/compiler/test code is MIT; package licensing identifies all three licenses.

The prior CLDR 48 approximation and the three-locale compact table have been removed. The compatibility data now comes from the exact intl package used by the Flutter oracle (whose generated table headers identify CLDR 46).

To regenerate the export from the pinned Flutter fixture’s dependencies:

```sh
cd fixtures/flutter
dart run tool/export_intl.dart
cd ../..
python3 scripts/generate_intl.py
```

Run the full oracle suite after a data or algorithm change. It intentionally rejects any expected-difference baseline.
