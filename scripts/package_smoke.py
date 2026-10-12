#!/usr/bin/env python3
from pathlib import Path
import json,subprocess,shutil,os
root=Path(__file__).resolve().parents[1]; artifacts=root/'artifacts'; consumer=artifacts/'package-consumer'
consumer.mkdir(parents=True,exist_ok=True)
def run(args,expected=0):
 p=subprocess.run(args,cwd=root,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
 if p.returncode!=expected: raise RuntimeError(' '.join(map(str,args))+'\n'+p.stdout)
 print(p.stdout)
 return p.stdout
run(['dotnet','pack','src/DotNative.Localization','-c','Release','-m:1','-o','artifacts','-v:q'])
(consumer/'NuGet.Config').write_text(f'''<configuration><packageSources><clear/><add key="local" value="{artifacts}"/><add key="nuget" value="https://api.nuget.org/v3/index.json"/></packageSources><config><add key="globalPackagesFolder" value="packages"/></config></configuration>''')
# Keep package cache under artifacts rather than under project (where .cs files could be globbed).
(consumer/'Consumer.csproj').write_text('''<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><Nullable>enable</Nullable><IsAotCompatible>true</IsAotCompatible></PropertyGroup><ItemGroup><PackageReference Include="DotNative.Localization" Version="0.1.0-preview.2"/><AdditionalFiles Include="arb/*.arb"/></ItemGroup></Project>''')
cache=consumer/'packages/dotnative.localization'
if cache.exists():shutil.rmtree(cache)
shutil.copytree(root/'fixtures/arb',consumer/'arb',dirs_exist_ok=True)
config={'arb-dir':'arb','namespace':'Smoke','output-class':'Texts','untranslated-messages-file':'obj/untranslated.json','use-escaping':True,'preferred-supported-locales':['sr','en']}
(consumer/'l10n.json').write_text(json.dumps(config))
(consumer/'Program.cs').write_text('''using System;
using System.Globalization;
using Smoke;
if(Texts.ForLocale("sr-Latn-RS").Hello!="Zdravo") throw new Exception("exact locale");
if(Texts.ForLocale("sr-Latn-BA").Hello!="Latin parent") throw new Exception("language fallback");
bool rejected=false;try{Texts.ForLocale("xx-YY");}catch(ArgumentException){rejected=true;}if(!rejected)throw new Exception("unsupported locale");
if(Texts.ForLocale(CultureInfo.GetCultureInfo("ru")).Items(22)!="few") throw new Exception("CLDR few");
if(Texts.ForLocale("ru").Items(11)!="many") throw new Exception("CLDR many");
if(Texts.ForLocale("ru").Empty!="") throw new Exception("empty");
if(Texts.ForLocale("ru").Fallback!="Template fallback") throw new Exception("missing key");
if(Texts.ForLocale("en").Multi(first:7,second:"B")!="B then 7") throw new Exception("named parameters");
if(Texts.ForLocale("en").Pound(2)!="# items") throw new Exception("pound");
if(Texts.SupportedLocales[0]!="sr") throw new Exception("preferred locales");
Console.WriteLine("Package consumer and NativeAOT smoke passed.");
''')
run(['dotnet','build',str(consumer/'Consumer.csproj'),'-m:1','-v:q'])
shutil.copy(consumer/'obj/untranslated.json',root/'fixtures/results/csharp-untranslated.json')
report=json.loads((consumer/'obj/untranslated.json').read_text());assert 'fallback' in report['ru'];assert 'empty' not in report.get('ru',[])
run(['dotnet',str(consumer/'bin/Debug/net8.0/Consumer.dll')])
# Invalid ARB must fail the actual consuming build with the generator's diagnostic.
path=consumer/'arb/app_en.arb'; original=path.read_text();path.write_text((root/'fixtures/broken/app_en.arb').read_text())
try:
 p=subprocess.run(['dotnet','build',str(consumer/'Consumer.csproj'),'-m:1','-v:q'],cwd=root,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
 (root/'fixtures/results/csharp-broken.txt').write_text(p.stdout)
 assert p.returncode!=0 and 'L10N001' in p.stdout and '[broken]' in p.stdout,p.stdout
finally:path.write_text(original)
run(['dotnet','publish',str(consumer/'Consumer.csproj'),'-c','Release','-p:PublishAot=true','-m:1','-o',str(artifacts/'aot'),'-v:q'])
run([str(artifacts/'aot/Consumer')])
assert not list((artifacts/'aot').glob('*.arb'))
assert not list((artifacts/'aot').glob('*Newtonsoft*'))
assert not list((artifacts/'aot').glob('*Compiler*'))
