#!/usr/bin/env python3
"""No allowlist: every generated-localization output must equal Flutter."""
from pathlib import Path
import json,re
root=Path(__file__).resolve().parents[1];result=root/'fixtures/results'
a=json.loads((result/'flutter.json').read_text());b=json.loads((result/'csharp.json').read_text())
diff={k:{'flutter':a.get(k),'csharp':b.get(k)} for k in sorted(a.keys()|b.keys()) if a.get(k)!=b.get(k)}
assert not diff,json.dumps(diff,ensure_ascii=False,indent=2)
assert len(a)>800,len(a)
cs_report=json.loads((result/'csharp-untranslated.json').read_text());flutter_report=json.loads((root/'fixtures/flutter/untranslated.json').read_text())
assert cs_report==flutter_report,'Untranslated report differs'
source=list((root/'tests/Integration/obj/generated').rglob('Localizations.g.cs'));assert len(source)==1
cs=source[0].read_text();(result/'Localizations.g.cs.txt').write_text(cs)
dart=(root/'fixtures/flutter/lib/generated/app_localizations.dart').read_text()
signatures=dict(re.findall(r'\[global::DotNative.Localization.ArbKey\("(.*?)"\)\]\s*public abstract ([^\n]+)',cs))
api=json.loads((root/'fixtures/arb/app_en.arb').read_text())
rows=['# Executed Flutter / C# parity','',f'Flutter 3.41.0 (`44a626f4f0`), Dart 3.11.0, intl 0.20.2. **{len(a)}/{len(a)} generated-message observations match. Zero allowed differences.**','', 'Unknown-locale failures are compared as `unsupported` because Dart and CLR have different exception types. All successful strings are compared exactly, including Unicode spaces, digit shapes and script. The same ARB files go to both compilers. Rejected Dart keyword fixtures live separately and both builds must reject them.','', '## Template API','', '| ARB key | Flutter | C# |','| --- | --- | --- |']
for key in api:
 if key.startswith('@'):continue
 ds=re.findall(r'  String (?:get )?'+re.escape(key)+r'(?:\([^\n]*\))?;',dart)
 assert ds,key
 rows.append(f'| `{key}` | `{ds[0].strip()}` | `{signatures[key]}` |')
rows+=['','## Every generated-message observation','', '| Locale / member / arguments | Flutter | C# |','| --- | --- | --- |']
def cell(v):return '`'+json.dumps(v,ensure_ascii=False).replace('\u00a0','\\u00a0').replace('\u202f','\\u202f').replace('|','\\|').replace('`','\\u0060')+'`'
for key in sorted(a):rows.append(f'| `{key}` | {cell(a[key])} | {cell(b[key])} |')
validation=json.loads((result/'validation-comparison.json').read_text())
rows+=['','## ARB acceptance / rejection','', '| Case | Flutter + Dart analysis | C# compiler |','| --- | --- | --- |']
for row in validation:
 assert row['flutter']==row['csharp'],row
 rows.append('| '+row['name']+' | '+('accept' if row['flutter'] else 'reject')+' | '+('accept' if row['csharp'] else 'reject')+' |')
intl=json.loads((result/'intl-oracle.json').read_text());failures=json.loads((root/'artifacts/intl-failures.json').read_text());assert not failures
rows+=['','## intl runtime differential suite','',f'**{len(intl):,}/{len(intl):,} cases match.** The complete machine-readable side is `intl-oracle.json`; `tests/IntlTests` executes and compares each C# result. Includes every locale in the pinned date/number/plural tables, all 11 number formats, all gen-l10n date skeletons, custom/combined date patterns, currency options, negative/large/tiny numbers, native int64 values, NaN/infinity and error outcomes.','', 'Run `python3 scripts/verify.py` to recreate the oracle and rerun all comparisons. No mismatch baseline is accepted.','']
(result/'comparison.md').write_text('\n'.join(rows))
print(f'Parity: {len(a)} generated messages, {len(validation)} validation cases, {len(intl)} intl cases; zero differences.')
