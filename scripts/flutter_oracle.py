#!/usr/bin/env python3
"""Run real Flutter; capture expected rejections separately from runnable fixtures."""
from pathlib import Path
import json,subprocess,shutil
root=Path(__file__).resolve().parents[1]
project=root/'fixtures/flutter'; results=root/'fixtures/results'
results.mkdir(exist_ok=True)
config=project/'l10n.yaml'; original=config.read_text()
def run(args,log,success=True):
 p=subprocess.run(args,cwd=project,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
 (results/log).write_text(p.stdout)
 if (p.returncode==0)!=success: raise RuntimeError(f'{args}: unexpected exit {p.returncode}:\n{p.stdout}')
 return p.stdout
try:
 run(['flutter','--version'],'flutter-version.txt')
 config.write_text(original.replace('arb-dir: arb','arb-dir: ../rejected/keywords').replace('  - sr\n',''))
 # Full fixture set proves that real Dart keywords cannot compile.
 text=run(['flutter','gen-l10n'],'flutter-keywords.txt',False)
 assert "can't be used as an identifier" in text
 config.write_text(original.replace('arb-dir: arb','arb-dir: ../broken'))
 text=run(['flutter','gen-l10n'],'flutter-broken.txt',False)
 assert 'broken' in text
 # General =N is requested by this library, but is rejected by current gen-l10n.
 temp=project/'exact-arb';temp.mkdir(exist_ok=True)
 (temp/'app_en.arb').write_text(json.dumps({'x':'{n, plural, =42{answer} other{other}}','@x':{'placeholders':{'n':{'type':'int'}}}}))
 config.write_text(original.replace('arb-dir: arb','arb-dir: exact-arb').replace('  - sr\n',''))
 text=run(['flutter','gen-l10n'],'flutter-exact42.txt',False)
 assert '42' in text
 shutil.rmtree(temp)
 config.write_text(original)
 stage=project/'arb';stage.mkdir(exist_ok=True)
 for path in (root/'fixtures/arb').glob('*.arb'):
  data=json.loads(path.read_text());data.pop('class',None);data.pop('continue',None)
  (stage/path.name).write_text(json.dumps(data,ensure_ascii=False,indent=2)+'\n')
 run(['flutter','gen-l10n'],'flutter-generation.txt')
 run(['flutter','test','--reporter','expanded'],'flutter-tests.txt')
finally:
 config.write_text(original)
print('Flutter fixtures executed; errors and output captured.')
