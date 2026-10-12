#!/usr/bin/env python3
"""Compile the pinned Dart intl export to C# constants; no runtime JSON."""
import json,re
from pathlib import Path
root=Path(__file__).resolve().parents[1];data=json.loads((root/'vendor/intl/data.json').read_text())
def q(x):return json.dumps(x,ensure_ascii=True)
def arr(x):return 'new string[] {'+','.join(map(q,x))+'}'
def mapping(x,typ,val):return f'new Dictionary<string,{typ}> {{'+','.join('{'+q(k)+','+val(v)+'}' for k,v in x.items())+'}'
out=['// Derived from Dart intl 0.20.2. BSD-3-Clause; vendor/intl/LICENSE.','#nullable enable','using System.Collections.Generic;','namespace DotNative.Localization;','internal static partial class IntlData {']
for group,typ in [('numbers','NumberData'),('dates','DateData')]:
 out.append(f'internal static {typ}? Load{typ}(string locale) => locale switch {{')
 for locale in data[group]:out.append(q(locale)+' => '+group+'_'+locale+'(),')
 out.append('_ => null };')
 for locale,d in data[group].items():
  if group=='numbers':
   compact=data['compact'][locale]
   maps=[]
   for kind in ['short','long','currency']:
    vals=compact[kind] or compact['short']
    maps.append('new SortedDictionary<int,Dictionary<string,string>> {'+','.join('{'+exp+','+mapping(v,'string',q)+'}' for exp,v in vals.items())+'}')
   body='new NumberData('+arr(list(d.values()))+','+','.join(maps)+')'
  else:
   syms={k:v for k,v in d.items() if isinstance(v,list) and (not v or isinstance(v[0],str))}
   body='new DateData('+mapping(syms,'string[]',arr)+','+mapping(data['datePatterns'][locale],'string',q)+','+q(d.get('ZERODIGIT') or '0')+')'
  out.append(f'private static {typ} {group}_{locale}() => {body};')
for field in ['currencyDigits','currencySymbols']:
 typ='int' if field=='currencyDigits' else 'string'
 out.append(f'internal static readonly Dictionary<string,{typ}> {field} = '+mapping(data[field],typ,str if typ=='int' else q)+';')
out.append('}')
(root/'src/DotNative.Localization/IntlData.g.cs').write_text('\n'.join(out)+'\n')
# Translate the actual intl predicates, preserving signed Dart operands and Euclidean modulo.
source=(root/'vendor/intl/plural_rules.dart').read_text()
functions=re.findall(r'PluralCase (_\w+_rule)\(\) \{(.*?)\n\}',source,re.S)
functions.insert(0,('_default_rule','return OTHER;'))
cs=['// Port of Dart intl 0.20.2 plural_rules.dart. BSD-3-Clause.','using System;','namespace DotNative.Localization;','public static class PluralRules {','private static double Mod(double a,double b) { double r=a%b;return r<0?r+b:r; }', '''public static string Select(string locale, double value, int? precision = null) {
if (!double.IsFinite(value)) throw new ArgumentException("Non-finite plural count.", nameof(value));
double _n=value;long _i=DartInteger(Math.Round(value,MidpointRounding.AwayFromZero));
string repr=Formatting.RawDouble(value);
int dot=repr.IndexOf('.');
int _v=precision ?? Math.Min(3, dot<0?0:repr.Length-dot-1);
if(precision==null && value<long.MaxValue && value>=long.MinValue && Math.Truncate(value)==value) _v=0;
double factor=Math.Pow(10,_v);long _f=Mod(DartInteger(Math.Floor(value*factor)),(long)factor), _t=_f;
while(_t!=0 && _t%10==0) _t/=10;
foreach(var candidate in IntlLocale.Candidates(locale)) {
switch(candidate) {''']
for locale,fn in re.findall(r"  '([^']+)': (_\w+_rule),",source):cs.append('case '+q(locale)+': return '+fn+'(_n,_i,_v,_f,_t);')
cs+=['} } return "other"; }', '''public static string Choose(string locale,double value,int available) {
if(value==0 && (available&1)!=0) return "zero";
if(value==1 && (available&2)!=0) return "one";
if(value==2 && (available&4)!=0) return "two";
string category=Select(locale,value);
int mask=category switch { "zero"=>1,"one"=>2,"two"=>4,"few"=>8,"many"=>16,_=>32 };
if((available&mask)!=0) return category;
return category=="two" && (available&8)!=0 ? "few" : "other";
}''']
for fn,body in functions:
 body=re.sub(r'List.generate\((\d+), \(i\) => i \+ (\d+)\)\.contains\(([^)]+)\)',lambda m:'['+','.join(str(n) for n in range(int(m[2]),int(m[2])+int(m[1])))+'].contains('+m[3]+')',body)
 body=re.sub(r'\[([0-9,\s]+)\]\.contains\(([^)]+)\)',lambda m:'('+' || '.join(m[2]+' == '+n.strip() for n in m[1].split(','))+')',body)
 body=re.sub(r'(_[nivft]) % (\d+)',r'Mod(\1,\2)',body)
 body=re.sub(r'\b(ZERO|ONE|TWO|FEW|MANY|OTHER)\b',lambda m:q(m[0].lower()),body)
 cs.append('private static string '+fn+'(double _n,long _i,int _v,long _f,long _t) {'+body+'}')
cs.append('private static long DartInteger(double v)=>v>=long.MaxValue?long.MaxValue:v<=long.MinValue?long.MinValue:(long)v;')
cs.append('private static long Mod(long a,long b) => (a%b+b)%b;')
cs.append('public static string SelectInteger(string locale,long value) { foreach(var candidate in IntlLocale.Candidates(locale)) { switch(candidate) {')
for locale,fn in re.findall(r"  '([^']+)': (_\w+_rule),",source):cs.append('case '+q(locale)+': return '+fn+'Integer(value,value,0,0,0);')
cs.append('} } return "other"; }')
cs.append('public static string Choose(string locale,long value,int available) => Choose(locale,new L10nNumber(value),available);')
cs.append('''public static string Choose(string locale,L10nNumber value,int available) {
if(!value.IsInteger)return Choose(locale,value.Double,available);
long n=value.Integer;
if(n==0 && (available&1)!=0)return "zero";
if(n==1 && (available&2)!=0)return "one";
if(n==2 && (available&4)!=0)return "two";
string category=SelectInteger(locale,n);
int mask=category switch { "zero"=>1,"one"=>2,"two"=>4,"few"=>8,"many"=>16,_=>32 };
return (available&mask)!=0?category:category=="two" && (available&8)!=0?"few":"other";
}''')
for item in list(cs):
 if item.startswith('private static string _'):
  cs.append(re.sub(r'(_\w+_rule)\(',r'\1Integer(',item).replace('double _','long _'))
cs.append('}')
(root/'src/DotNative.Localization/PluralRules.g.cs').write_text('\n'.join(cs)+'\n')
