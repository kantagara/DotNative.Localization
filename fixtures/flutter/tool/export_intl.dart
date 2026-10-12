import 'dart:convert';
import 'dart:io';
import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/date_time_patterns.dart';
import 'package:intl/number_symbols_data.dart';
import 'package:intl/src/intl/constants.dart';
void main() {
 final data={
 'dates':dateTimeSymbolMap().map((k,v)=>MapEntry(k,v.serializeToMap())),
 'datePatterns':dateTimePatternMap(),
 'numbers':numberFormatSymbols.map((k,v)=>MapEntry(k,{
 'NAME':v.NAME,'DECIMAL_SEP':v.DECIMAL_SEP,'GROUP_SEP':v.GROUP_SEP,'PERCENT':v.PERCENT,'ZERO_DIGIT':v.ZERO_DIGIT,'PLUS_SIGN':v.PLUS_SIGN,'MINUS_SIGN':v.MINUS_SIGN,'EXP_SYMBOL':v.EXP_SYMBOL,'PERMILL':v.PERMILL,'INFINITY':v.INFINITY,'NAN':v.NAN,'DECIMAL_PATTERN':v.DECIMAL_PATTERN,'SCIENTIFIC_PATTERN':v.SCIENTIFIC_PATTERN,'PERCENT_PATTERN':v.PERCENT_PATTERN,'CURRENCY_PATTERN':v.CURRENCY_PATTERN,'DEF_CURRENCY_CODE':v.DEF_CURRENCY_CODE})),
 'compact':compactNumberSymbols.map((k,v)=>MapEntry(k,{'short':v.COMPACT_DECIMAL_SHORT_PATTERN.map((k,v)=>MapEntry('$k',v)),'long':v.COMPACT_DECIMAL_LONG_PATTERN?.map((k,v)=>MapEntry('$k',v)),'currency':v.COMPACT_DECIMAL_SHORT_CURRENCY_PATTERN.map((k,v)=>MapEntry('$k',v))})),
 'currencyDigits':currencyFractionDigits,'currencySymbols':simpleCurrencySymbols};
 File('../../vendor/intl/data.json').writeAsStringSync(const JsonEncoder.withIndent('  ').convert(data));
}
