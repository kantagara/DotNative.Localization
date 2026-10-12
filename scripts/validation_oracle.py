#!/usr/bin/env python3
from pathlib import Path
import json,subprocess,shutil
root=Path(__file__).resolve().parents[1]; project=root/'fixtures/flutter'
cases=json.loads((root/'fixtures/validation/cases.json').read_text());cfg=project/'l10n.yaml';original=cfg.read_text();stage=project/'validation-arb';rows=[]
try:
 for case in cases:
  if stage.exists():shutil.rmtree(stage)
  stage.mkdir()
  for name,text in case['files'].items():(stage/name).write_text(text)
  settings={'arb-dir':'validation-arb','template-arb-file':'app_en.arb','output-dir':'lib/generated','output-class':'AppLocalizations',**case['config']}
  cfg.write_text('\n'.join(k+': '+json.dumps(v) for k,v in settings.items())+'\n')
  generated=project/'lib/generated'
  if generated.exists():shutil.rmtree(generated)
  p=subprocess.run(['flutter','gen-l10n'],cwd=project,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  accepted=p.returncode==0;logs=p.stdout
  if accepted:
   analysis=subprocess.run(['dart','analyze','--format=machine','lib/generated'],cwd=project,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
   logs+=analysis.stdout
   accepted=not any(line.startswith('ERROR|') for line in analysis.stdout.splitlines())
  rows.append({'name':case['name'],'accepted':accepted,'log':logs})
  print(case['name'],accepted,flush=True)
finally:cfg.write_text(original)
(root/'fixtures/results/validation-flutter.json').write_text(json.dumps(rows,ensure_ascii=False,indent=2)+'\n')
