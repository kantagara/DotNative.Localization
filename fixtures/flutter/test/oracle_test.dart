import 'dart:convert';
import 'dart:io';
import 'dart:ui';
import 'package:intl/date_symbol_data_local.dart';
import 'package:flutter_test/flutter_test.dart';
import '../lib/generated/app_localizations.dart';
void main() {
 test('capture actual gen-l10n output', () async {
 await initializeDateFormatting();
 final output=<String,String>{};
 for(final tag in ['en','sr','ru','ar','he','pl','fr','ja','hi','sr-Latn-RS','sr-Latn-BA','sr-Cyrl-RS','sr-Cyrl-BG','zz']) {
  final parts=tag.split('-');
  final locale=Locale.fromSubtags(languageCode:parts[0],scriptCode:parts.length>1?parts[1]:null,countryCode:parts.length>2?parts[2]:null);
  AppLocalizations l;
  try { l=lookupAppLocalizations(locale); } catch(e) { output['$tag/lookup-error']='unsupported'; continue; }
  void add(String key,String value) { output['$tag/$key']=value; }
  add('hello',l.hello); add('empty',l.empty); add('time_format',l.time_format);
  add('greeting',l.greeting('Ada')); add('multi',l.multi(7,'B'));
  for(final g in ['male','female','other','unknown']) { add('select/$g',l.select(g)); }
  for(final n in <num>[0,1,2,3,4,5,11,21,22,101,1.2,2.1,-1]) { add('items/$n',l.items(n)); }
  for(final n in [0,1,2]) { for(final g in ['male','female','other']) { add('nested/$n/$g',l.nested(n,g)); } }
  add('date',l.date(DateTime(2024,7,10))); add('longDate',l.longDate(DateTime(2024,7,10)));
  add('number',l.number(12345.67)); add('money',l.money(1234.5)); add('compact',l.compact(1200));
  add('event',l.event);add('namespace',l.namespace);add('checked',l.checked);add('parentText',l.parentText);
  add('implicit',l.implicit(true,7));add('unused',l.unused(3));add('omit',l.omit('Ada'));
  for(final n in <num>[0,1,2,21,-1,1.1])add('merged/$n',l.merged(n));
  add('duplicateSelect',l.duplicateSelect('a'));add('select/suffix',l.select('Enum.male'));
  add('rawNum/int',l.rawNum(1));add('rawNum/double',l.rawNum(1.0));add('largeInt',l.largeInt(9007199254740993));
  final date=DateTime(2024,7,10,15,4,5,123);
  add('inlineDate',l.inlineDate(date));add('inlineTime',l.inlineTime(date));add('customDate',l.customDate(date));add('quarter',l.quarter(date));add('combinedDate',l.combinedDate(date));
  add('currencyDefault',l.currencyDefault(1234.5));add('shortCurrency',l.shortCurrency(1234.5));add('longCompact',l.longCompact(1234.5));add('formatStringIgnored',l.formatStringIgnored('Hello'));
  add('escaped',l.escaped); add('pound',l.pound(2)); add('fallback',l.fallback); add('fallbackPlural',l.fallbackPlural(21));
 }
 File('../results/flutter.json').writeAsStringSync(const JsonEncoder.withIndent('  ').convert(output));
 });
}
