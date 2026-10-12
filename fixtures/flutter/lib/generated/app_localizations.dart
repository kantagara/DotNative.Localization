import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:intl/intl.dart' as intl;

import 'app_localizations_ar.dart';
import 'app_localizations_en.dart';
import 'app_localizations_fr.dart';
import 'app_localizations_he.dart';
import 'app_localizations_hi.dart';
import 'app_localizations_ja.dart';
import 'app_localizations_pl.dart';
import 'app_localizations_ru.dart';
import 'app_localizations_sr.dart';

// ignore_for_file: type=lint

/// Callers can lookup localized strings with an instance of AppLocalizations
/// returned by `AppLocalizations.of(context)`.
///
/// Applications need to include `AppLocalizations.delegate()` in their app's
/// `localizationDelegates` list, and the locales they support in the app's
/// `supportedLocales` list. For example:
///
/// ```dart
/// import 'generated/app_localizations.dart';
///
/// return MaterialApp(
///   localizationsDelegates: AppLocalizations.localizationsDelegates,
///   supportedLocales: AppLocalizations.supportedLocales,
///   home: MyApplicationHome(),
/// );
/// ```
///
/// ## Update pubspec.yaml
///
/// Please make sure to update your pubspec.yaml to include the following
/// packages:
///
/// ```yaml
/// dependencies:
///   # Internationalization support.
///   flutter_localizations:
///     sdk: flutter
///   intl: any # Use the pinned version from flutter_localizations
///
///   # Rest of dependencies
/// ```
///
/// ## iOS Applications
///
/// iOS applications define key application metadata, including supported
/// locales, in an Info.plist file that is built into the application bundle.
/// To configure the locales supported by your app, you’ll need to edit this
/// file.
///
/// First, open your project’s ios/Runner.xcworkspace Xcode workspace file.
/// Then, in the Project Navigator, open the Info.plist file under the Runner
/// project’s Runner folder.
///
/// Next, select the Information Property List item, select Add Item from the
/// Editor menu, then select Localizations from the pop-up menu.
///
/// Select and expand the newly-created Localizations item then, for each
/// locale your application supports, add a new item and select the locale
/// you wish to add from the pop-up menu in the Value field. This list should
/// be consistent with the languages listed in the AppLocalizations.supportedLocales
/// property.
abstract class AppLocalizations {
  AppLocalizations(String locale)
    : localeName = intl.Intl.canonicalizedLocale(locale.toString());

  final String localeName;

  static AppLocalizations? of(BuildContext context) {
    return Localizations.of<AppLocalizations>(context, AppLocalizations);
  }

  static const LocalizationsDelegate<AppLocalizations> delegate =
      _AppLocalizationsDelegate();

  /// A list of this localizations delegate along with the default localizations
  /// delegates.
  ///
  /// Returns a list of localizations delegates containing this delegate along with
  /// GlobalMaterialLocalizations.delegate, GlobalCupertinoLocalizations.delegate,
  /// and GlobalWidgetsLocalizations.delegate.
  ///
  /// Additional delegates can be added by appending to this list in
  /// MaterialApp. This list does not have to be used at all if a custom list
  /// of delegates is preferred or required.
  static const List<LocalizationsDelegate<dynamic>> localizationsDelegates =
      <LocalizationsDelegate<dynamic>>[
        delegate,
        GlobalMaterialLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
      ];

  /// A list of this localizations delegate's supported locales.
  static const List<Locale> supportedLocales = <Locale>[
    Locale('sr'),
    Locale('en'),
    Locale('ar'),
    Locale('fr'),
    Locale('he'),
    Locale('hi'),
    Locale('ja'),
    Locale('pl'),
    Locale('ru'),
    Locale.fromSubtags(languageCode: 'sr', scriptCode: 'Latn'),
    Locale('sr', 'RS'),
    Locale.fromSubtags(
      languageCode: 'sr',
      countryCode: 'RS',
      scriptCode: 'Latn',
    ),
  ];

  /// No description provided for @hello.
  ///
  /// In en, this message translates to:
  /// **'Hello'**
  String get hello;

  /// No description provided for @empty.
  ///
  /// In en, this message translates to:
  /// **''**
  String get empty;

  /// No description provided for @time_format.
  ///
  /// In en, this message translates to:
  /// **'24-hour'**
  String get time_format;

  /// No description provided for @greeting.
  ///
  /// In en, this message translates to:
  /// **'Hello {name}!'**
  String greeting(String name);

  /// No description provided for @multi.
  ///
  /// In en, this message translates to:
  /// **'{second} then {first}'**
  String multi(int first, String second);

  /// No description provided for @select.
  ///
  /// In en, this message translates to:
  /// **'{gender, select, male{He} female{She} other{They}}'**
  String select(String gender);

  /// No description provided for @items.
  ///
  /// In en, this message translates to:
  /// **'{count, plural, =0{empty} one{one} few{few} many{many} other{other}}'**
  String items(num count);

  /// No description provided for @nested.
  ///
  /// In en, this message translates to:
  /// **'{count, plural, =0{Nobody} one{{gender, select, male{His item} female{Her item} other{Their item}}} other{{gender, select, male{His items} female{Her items} other{Their items}}}}'**
  String nested(int count, String gender);

  /// No description provided for @date.
  ///
  /// In en, this message translates to:
  /// **'Date {value}'**
  String date(DateTime value);

  /// No description provided for @longDate.
  ///
  /// In en, this message translates to:
  /// **'Date {value}'**
  String longDate(DateTime value);

  /// No description provided for @number.
  ///
  /// In en, this message translates to:
  /// **'Number {value}'**
  String number(double value);

  /// No description provided for @money.
  ///
  /// In en, this message translates to:
  /// **'Money {value}'**
  String money(num value);

  /// No description provided for @compact.
  ///
  /// In en, this message translates to:
  /// **'Count {value}'**
  String compact(num value);

  /// No description provided for @escaped.
  ///
  /// In en, this message translates to:
  /// **'Use \'{name}\' and \'it\'\'s\' safe'**
  String get escaped;

  /// No description provided for @pound.
  ///
  /// In en, this message translates to:
  /// **'{count, plural, one{# item} other{# items}}'**
  String pound(int count);

  /// No description provided for @fallback.
  ///
  /// In en, this message translates to:
  /// **'Template fallback'**
  String get fallback;

  /// No description provided for @fallbackPlural.
  ///
  /// In en, this message translates to:
  /// **'{count, plural, one{English one} other{English other}}'**
  String fallbackPlural(num count);

  /// No description provided for @event.
  ///
  /// In en, this message translates to:
  /// **'Event'**
  String get event;

  /// No description provided for @namespace.
  ///
  /// In en, this message translates to:
  /// **'Namespace'**
  String get namespace;

  /// No description provided for @checked.
  ///
  /// In en, this message translates to:
  /// **'Checked'**
  String get checked;

  /// No description provided for @parentText.
  ///
  /// In en, this message translates to:
  /// **'English parent'**
  String get parentText;

  /// No description provided for @implicit.
  ///
  /// In en, this message translates to:
  /// **'{z}:{a}'**
  String implicit(Object a, Object z);

  /// No description provided for @unused.
  ///
  /// In en, this message translates to:
  /// **'ready'**
  String unused(int unused);

  /// No description provided for @omit.
  ///
  /// In en, this message translates to:
  /// **'Hello {name}'**
  String omit(String name);

  /// No description provided for @merged.
  ///
  /// In en, this message translates to:
  /// **'{n, plural, =1{first} one{last} =2{two-first} two{two-last} few{few} other{other}}'**
  String merged(num n);

  /// No description provided for @duplicateSelect.
  ///
  /// In en, this message translates to:
  /// **'{g, select, a{first} a{last} other{other}}'**
  String duplicateSelect(String g);

  /// No description provided for @rawNum.
  ///
  /// In en, this message translates to:
  /// **'{n}'**
  String rawNum(num n);

  /// No description provided for @largeInt.
  ///
  /// In en, this message translates to:
  /// **'{n}'**
  String largeInt(int n);

  /// No description provided for @inlineDate.
  ///
  /// In en, this message translates to:
  /// **'{date, date, ::yMMMd}'**
  String inlineDate(DateTime date);

  /// No description provided for @inlineTime.
  ///
  /// In en, this message translates to:
  /// **'{date, time, ::Hms}'**
  String inlineTime(DateTime date);

  /// No description provided for @customDate.
  ///
  /// In en, this message translates to:
  /// **'{date}'**
  String customDate(DateTime date);

  /// No description provided for @quarter.
  ///
  /// In en, this message translates to:
  /// **'{date}'**
  String quarter(DateTime date);

  /// No description provided for @combinedDate.
  ///
  /// In en, this message translates to:
  /// **'{date}'**
  String combinedDate(DateTime date);

  /// No description provided for @currencyDefault.
  ///
  /// In en, this message translates to:
  /// **'{n}'**
  String currencyDefault(num n);

  /// No description provided for @shortCurrency.
  ///
  /// In en, this message translates to:
  /// **'{n}'**
  String shortCurrency(num n);

  /// No description provided for @longCompact.
  ///
  /// In en, this message translates to:
  /// **'{n}'**
  String longCompact(num n);

  /// No description provided for @formatStringIgnored.
  ///
  /// In en, this message translates to:
  /// **'{x}'**
  String formatStringIgnored(String x);
}

class _AppLocalizationsDelegate
    extends LocalizationsDelegate<AppLocalizations> {
  const _AppLocalizationsDelegate();

  @override
  Future<AppLocalizations> load(Locale locale) {
    return SynchronousFuture<AppLocalizations>(lookupAppLocalizations(locale));
  }

  @override
  bool isSupported(Locale locale) => <String>[
    'ar',
    'en',
    'fr',
    'he',
    'hi',
    'ja',
    'pl',
    'ru',
    'sr',
  ].contains(locale.languageCode);

  @override
  bool shouldReload(_AppLocalizationsDelegate old) => false;
}

AppLocalizations lookupAppLocalizations(Locale locale) {
  // Lookup logic when language+script+country codes are specified.
  switch (locale.toString()) {
    case 'sr_Latn_RS':
      return AppLocalizationsSrLatnRs();
  }

  // Lookup logic when language+script codes are specified.
  switch (locale.languageCode) {
    case 'sr':
      {
        switch (locale.scriptCode) {
          case 'Latn':
            return AppLocalizationsSrLatn();
        }
        break;
      }
  }

  // Lookup logic when language+country codes are specified.
  switch (locale.languageCode) {
    case 'sr':
      {
        switch (locale.countryCode) {
          case 'RS':
            return AppLocalizationsSrRs();
        }
        break;
      }
  }

  // Lookup logic when only language code is specified.
  switch (locale.languageCode) {
    case 'ar':
      return AppLocalizationsAr();
    case 'en':
      return AppLocalizationsEn();
    case 'fr':
      return AppLocalizationsFr();
    case 'he':
      return AppLocalizationsHe();
    case 'hi':
      return AppLocalizationsHi();
    case 'ja':
      return AppLocalizationsJa();
    case 'pl':
      return AppLocalizationsPl();
    case 'ru':
      return AppLocalizationsRu();
    case 'sr':
      return AppLocalizationsSr();
  }

  throw FlutterError(
    'AppLocalizations.delegate failed to load unsupported locale "$locale". This is likely '
    'an issue with the localizations generation tool. Please file an issue '
    'on GitHub with a reproducible sample app and the gen-l10n configuration '
    'that was used.',
  );
}
