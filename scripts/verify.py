#!/usr/bin/env python3
from pathlib import Path
import subprocess,sys
root=Path(__file__).resolve().parents[1]
def run(*args):
 print('+ '+' '.join(args),flush=True)
 subprocess.run(args,cwd=root,check=True)
run('python3','scripts/generate_intl.py')
run('flutter','pub','get','--directory=fixtures/flutter')
run('python3','scripts/validation_oracle.py')
run('dotnet','build','tests/CompilerTests','-m:1','-v:q')
run('dotnet','tests/CompilerTests/bin/Debug/net8.0/CompilerTests.dll')
run('dotnet','build','tests/Integration','-m:1','-v:q')
with (root/'fixtures/results/csharp.json').open('w') as f:
 subprocess.run(['dotnet','tests/Integration/bin/Debug/net8.0/Integration.dll'],cwd=root,stdout=f,check=True)
run('flutter','pub','get','--directory=fixtures/flutter')
run('python3','scripts/flutter_oracle.py')
subprocess.run(['dart','run','tool/format_oracle.dart'],cwd=root/'fixtures/flutter',check=True)
run('dotnet','build','tests/IntlTests','-m:1','-v:q')
run('dotnet','tests/IntlTests/bin/Debug/net8.0/IntlTests.dll')
run('python3','scripts/package_smoke.py')
run('python3','scripts/compare.py')
print('All verification passed.',flush=True)
