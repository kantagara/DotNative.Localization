import 'dart:convert';
import 'dart:io';
import 'package:intl/intl.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/number_symbols_data.dart';
import 'package:intl/src/plural_rules.dart' as rules;
void main() async {
 await initializeDateFormatting();
 final rows=<Map<String,Object?>>[];
 void capture(Map<String,Object?> row,String Function() eval) {try{row['expected']=eval();}catch(e){row['error']=e.runtimeType.toString();} rows.add(row);}
 final values=<double>[-1000001,-999999,-1234.567,-21,-2.1,-1,-0.0,0,0.001,0.0996,0.5,0.9999,1,1.2,1.005,1.9999,2,2.1,5,9.999,10,11,21,99.999,100,999,999.9,1000,1001,1200,1234.567,9999,99999,999999,1000000,1200000,1000000000,1e12,1e15,1e-7,1e20,1e30,1e100,-1e20,1e-20,5e-324,double.nan,double.infinity,double.negativeInfinity];
 for(final locale in numberFormatSymbols.keys) {
  for(final format in ['decimalPattern','decimalPatternDigits','percentPattern','decimalPercentPattern','scientificPattern','currency','simpleCurrency','compact','compactLong','compactCurrency','compactSimpleCurrency']) {
   for(final value in values) capture({'kind':'number','locale':locale,'format':format,'value':value.toString()},()=>number(format,locale).format(value));
  }
  for(final value in <int>[9007199254740993,9223372036854775807,-9223372036854775807]) {
   for(final format in ['decimalPattern','currency','compact'])capture({'kind':'integer','locale':locale,'format':format,'value':'$value'},()=>number(format,locale).format(value));
   capture({'kind':'pluralInteger','locale':locale,'value':'$value'},()=>Intl.pluralLogic(value,zero:'zero',one:'one',two:'two',few:'few',many:'many',other:'other',locale:locale));
  }
  for(final format in ['currency','simpleCurrency','compactCurrency','compactSimpleCurrency']) {
   for(final name in ['USD','JPY','KWD','CHF','BHD','CLF','XXX']) {
    for(final digits in <int?>[null,0,3]) capture({'kind':'number','locale':locale,'format':format,'value':'1234.567','name':name,'digits':digits},()=>number(format,locale,name:name,digits:digits).format(1234.567));
   }
  }
 }
 final formats='d E EEEE LLL LLLL M Md MEd MMM MMMd MMMEd MMMM MMMMd MMMMEEEEd QQQ QQQQ y yM yMd yMEd yMMM yMMMd yMMMEd yMMMM yMMMMd yMMMMEEEEd yQQQ yQQQQ H Hm Hms j jm jms jmv jmz jv jz m ms s'.split(' ');
 for(final locale in dateTimeSymbolMap().keys) {
  for(final format in [...formats,"yyyy 'unclosed","yyyy '' MM","'a''b' y","''' y",'yMd+Hms',"yyyy-MM-dd 'at' HH:mm:ss.SSSSS",'G GGGG EEEEE cccc LLLLL D Q']) {
   for(final date in [DateTime(2024,7,10,15,4,5,123),DateTime(2000,1,2,0,0,0)]) {
    final custom=format.contains("'")||format=='G GGGG EEEEE cccc LLLLL D Q';
    capture({'kind':'date','locale':locale,'format':format,'value':date.toIso8601String(),'custom':custom},(){final parts=custom?[format]:format.split('+');final f=DateFormat(parts.first,locale);for(final p in parts.skip(1)){f.addPattern(p);}return f.format(date);});
   }
  }
 }
 for(final locale in rules.pluralRules.keys) {
  for(final n in [...values,-101,-11,-5,-0.1,1.0001,2.001,101,102,1000001]) capture({'kind':'plural','locale':locale,'value':'$n'},()=>Intl.pluralLogic(n,zero:'zero',one:'one',two:'two',few:'few',many:'many',other:'other',locale:locale));
 }
 File('../results/intl-oracle.json').writeAsStringSync(jsonEncode(rows));
 print('${rows.length} intl cases exported.');
}
NumberFormat number(String f,String l,{String? name,int? digits})=>switch(f){
'decimalPattern'=>NumberFormat.decimalPattern(l),
'decimalPatternDigits'=>NumberFormat.decimalPatternDigits(locale:l,decimalDigits:digits),
'percentPattern'=>NumberFormat.percentPattern(l),
'decimalPercentPattern'=>NumberFormat.decimalPercentPattern(locale:l,decimalDigits:digits),
'scientificPattern'=>NumberFormat.scientificPattern(l),
'currency'=>NumberFormat.currency(locale:l,name:name,decimalDigits:digits),
'simpleCurrency'=>NumberFormat.simpleCurrency(locale:l,name:name,decimalDigits:digits),
'compact'=>NumberFormat.compact(locale:l),
'compactLong'=>NumberFormat.compactLong(locale:l),
'compactCurrency'=>NumberFormat.compactCurrency(locale:l,name:name,decimalDigits:digits),
'compactSimpleCurrency'=>NumberFormat.compactSimpleCurrency(locale:l,name:name,decimalDigits:digits),
_=>throw ArgumentError(f)};
