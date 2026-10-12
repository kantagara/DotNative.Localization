// ignore: unused_import
import 'package:intl/intl.dart' as intl;
import 'app_localizations.dart';

// ignore_for_file: type=lint

/// The translations for English (`en`).
class AppLocalizationsEn extends AppLocalizations {
  AppLocalizationsEn([String locale = 'en']) : super(locale);

  @override
  String get hello => 'Hello';

  @override
  String get empty => '';

  @override
  String get time_format => '24-hour';

  @override
  String greeting(String name) {
    return 'Hello $name!';
  }

  @override
  String multi(int first, String second) {
    return '$second then $first';
  }

  @override
  String select(String gender) {
    String _temp0 = intl.Intl.selectLogic(gender, {
      'male': 'He',
      'female': 'She',
      'other': 'They',
    });
    return '$_temp0';
  }

  @override
  String items(num count) {
    String _temp0 = intl.Intl.pluralLogic(
      count,
      locale: localeName,
      other: 'other',
      many: 'many',
      few: 'few',
      one: 'one',
      zero: 'empty',
    );
    return '$_temp0';
  }

  @override
  String nested(int count, String gender) {
    String _temp0 = intl.Intl.selectLogic(gender, {
      'male': 'His items',
      'female': 'Her items',
      'other': 'Their items',
    });
    String _temp1 = intl.Intl.selectLogic(gender, {
      'male': 'His item',
      'female': 'Her item',
      'other': 'Their item',
    });
    String _temp2 = intl.Intl.pluralLogic(
      count,
      locale: localeName,
      other: '$_temp0',
      one: '$_temp1',
      zero: 'Nobody',
    );
    return '$_temp2';
  }

  @override
  String date(DateTime value) {
    final intl.DateFormat valueDateFormat = intl.DateFormat.yMd(localeName);
    final String valueString = valueDateFormat.format(value);

    return 'Date $valueString';
  }

  @override
  String longDate(DateTime value) {
    final intl.DateFormat valueDateFormat = intl.DateFormat.yMMMd(localeName);
    final String valueString = valueDateFormat.format(value);

    return 'Date $valueString';
  }

  @override
  String number(double value) {
    final intl.NumberFormat valueNumberFormat =
        intl.NumberFormat.decimalPattern(localeName);
    final String valueString = valueNumberFormat.format(value);

    return 'Number $valueString';
  }

  @override
  String money(num value) {
    final intl.NumberFormat valueNumberFormat = intl.NumberFormat.currency(
      locale: localeName,
      name: 'USD',
      symbol: '\$',
      decimalDigits: 2,
    );
    final String valueString = valueNumberFormat.format(value);

    return 'Money $valueString';
  }

  @override
  String compact(num value) {
    final intl.NumberFormat valueNumberFormat = intl.NumberFormat.compact(
      locale: localeName,
    );
    final String valueString = valueNumberFormat.format(value);

    return 'Count $valueString';
  }

  @override
  String get escaped => 'Use {name} and it\'s safe';

  @override
  String pound(int count) {
    String _temp0 = intl.Intl.pluralLogic(
      count,
      locale: localeName,
      other: '# items',
      one: '# item',
    );
    return '$_temp0';
  }

  @override
  String get fallback => 'Template fallback';

  @override
  String fallbackPlural(num count) {
    String _temp0 = intl.Intl.pluralLogic(
      count,
      locale: localeName,
      other: 'English other',
      one: 'English one',
    );
    return '$_temp0';
  }

  @override
  String get event => 'Event';

  @override
  String get namespace => 'Namespace';

  @override
  String get checked => 'Checked';

  @override
  String get parentText => 'English parent';

  @override
  String implicit(Object a, Object z) {
    return '$z:$a';
  }

  @override
  String unused(int unused) {
    return 'ready';
  }

  @override
  String omit(String name) {
    return 'Hello $name';
  }

  @override
  String merged(num n) {
    String _temp0 = intl.Intl.pluralLogic(
      n,
      locale: localeName,
      other: 'other',
      few: 'few',
      two: 'two-last',
      one: 'last',
    );
    return '$_temp0';
  }

  @override
  String duplicateSelect(String g) {
    String _temp0 = intl.Intl.selectLogic(g, {
      'a': 'first',
      'a': 'last',
      'other': 'other',
    });
    return '$_temp0';
  }

  @override
  String rawNum(num n) {
    return '$n';
  }

  @override
  String largeInt(int n) {
    final intl.NumberFormat nNumberFormat = intl.NumberFormat.decimalPattern(
      localeName,
    );
    final String nString = nNumberFormat.format(n);

    return '$nString';
  }

  @override
  String inlineDate(DateTime date) {
    String _temp0 = intl.DateFormat.yMMMd(localeName).format(date);
    return '$_temp0';
  }

  @override
  String inlineTime(DateTime date) {
    String _temp0 = intl.DateFormat.Hms(localeName).format(date);
    return '$_temp0';
  }

  @override
  String customDate(DateTime date) {
    final intl.DateFormat dateDateFormat = intl.DateFormat(
      'yyyy \'year\' MM-dd HH:mm:ss.SSSS',
      localeName,
    );
    final String dateString = dateDateFormat.format(date);

    return '$dateString';
  }

  @override
  String quarter(DateTime date) {
    final intl.DateFormat dateDateFormat = intl.DateFormat.yQQQQ(localeName);
    final String dateString = dateDateFormat.format(date);

    return '$dateString';
  }

  @override
  String combinedDate(DateTime date) {
    final intl.DateFormat dateDateFormat = intl.DateFormat.yMd(
      localeName,
    ).add_Hms();
    final String dateString = dateDateFormat.format(date);

    return '$dateString';
  }

  @override
  String currencyDefault(num n) {
    final intl.NumberFormat nNumberFormat = intl.NumberFormat.currency(
      locale: localeName,
    );
    final String nString = nNumberFormat.format(n);

    return '$nString';
  }

  @override
  String shortCurrency(num n) {
    final intl.NumberFormat nNumberFormat = intl.NumberFormat.compactCurrency(
      locale: localeName,
    );
    final String nString = nNumberFormat.format(n);

    return '$nString';
  }

  @override
  String longCompact(num n) {
    final intl.NumberFormat nNumberFormat = intl.NumberFormat.compactLong(
      locale: localeName,
    );
    final String nString = nNumberFormat.format(n);

    return '$nString';
  }

  @override
  String formatStringIgnored(String x) {
    return '$x';
  }
}
