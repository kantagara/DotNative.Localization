// Port of Dart intl 0.20.2 plural_rules.dart. BSD-3-Clause.
using System;
namespace DotNative.Localization;
public static class PluralRules {
private static double Mod(double a,double b) { double r=a%b;return r<0?r+b:r; }
public static string Select(string locale, double value, int? precision = null) {
if (!double.IsFinite(value)) throw new ArgumentException("Non-finite plural count.", nameof(value));
double _n=value;long _i=DartInteger(Math.Round(value,MidpointRounding.AwayFromZero));
string repr=Formatting.RawDouble(value);
int dot=repr.IndexOf('.');
int _v=precision ?? Math.Min(3, dot<0?0:repr.Length-dot-1);
if(precision==null && value<long.MaxValue && value>=long.MinValue && Math.Truncate(value)==value) _v=0;
double factor=Math.Pow(10,_v);long _f=Mod(DartInteger(Math.Floor(value*factor)),(long)factor), _t=_f;
while(_t!=0 && _t%10==0) _t/=10;
foreach(var candidate in IntlLocale.Candidates(locale)) {
switch(candidate) {
case "en_ISO": return _ast_rule(_n,_i,_v,_f,_t);
case "af": return _af_rule(_n,_i,_v,_f,_t);
case "am": return _am_rule(_n,_i,_v,_f,_t);
case "ar": return _ar_rule(_n,_i,_v,_f,_t);
case "ar_DZ": return _ar_rule(_n,_i,_v,_f,_t);
case "ar_EG": return _ar_rule(_n,_i,_v,_f,_t);
case "as": return _am_rule(_n,_i,_v,_f,_t);
case "az": return _af_rule(_n,_i,_v,_f,_t);
case "be": return _be_rule(_n,_i,_v,_f,_t);
case "bg": return _af_rule(_n,_i,_v,_f,_t);
case "bm": return _default_rule(_n,_i,_v,_f,_t);
case "bn": return _am_rule(_n,_i,_v,_f,_t);
case "br": return _br_rule(_n,_i,_v,_f,_t);
case "bs": return _bs_rule(_n,_i,_v,_f,_t);
case "ca": return _ca_rule(_n,_i,_v,_f,_t);
case "chr": return _af_rule(_n,_i,_v,_f,_t);
case "cs": return _cs_rule(_n,_i,_v,_f,_t);
case "cy": return _cy_rule(_n,_i,_v,_f,_t);
case "da": return _da_rule(_n,_i,_v,_f,_t);
case "de": return _ast_rule(_n,_i,_v,_f,_t);
case "de_AT": return _ast_rule(_n,_i,_v,_f,_t);
case "de_CH": return _ast_rule(_n,_i,_v,_f,_t);
case "el": return _af_rule(_n,_i,_v,_f,_t);
case "en": return _ast_rule(_n,_i,_v,_f,_t);
case "en_AU": return _ast_rule(_n,_i,_v,_f,_t);
case "en_CA": return _ast_rule(_n,_i,_v,_f,_t);
case "en_GB": return _ast_rule(_n,_i,_v,_f,_t);
case "en_IE": return _ast_rule(_n,_i,_v,_f,_t);
case "en_IN": return _ast_rule(_n,_i,_v,_f,_t);
case "en_MY": return _ast_rule(_n,_i,_v,_f,_t);
case "en_NZ": return _ast_rule(_n,_i,_v,_f,_t);
case "en_SG": return _ast_rule(_n,_i,_v,_f,_t);
case "en_US": return _ast_rule(_n,_i,_v,_f,_t);
case "en_ZA": return _ast_rule(_n,_i,_v,_f,_t);
case "es": return _es_rule(_n,_i,_v,_f,_t);
case "es_419": return _es_rule(_n,_i,_v,_f,_t);
case "es_ES": return _es_rule(_n,_i,_v,_f,_t);
case "es_MX": return _es_rule(_n,_i,_v,_f,_t);
case "es_US": return _es_rule(_n,_i,_v,_f,_t);
case "et": return _ast_rule(_n,_i,_v,_f,_t);
case "eu": return _af_rule(_n,_i,_v,_f,_t);
case "fa": return _am_rule(_n,_i,_v,_f,_t);
case "fi": return _ast_rule(_n,_i,_v,_f,_t);
case "fil": return _ceb_rule(_n,_i,_v,_f,_t);
case "fr": return _fr_rule(_n,_i,_v,_f,_t);
case "fr_CA": return _fr_rule(_n,_i,_v,_f,_t);
case "fr_CH": return _fr_rule(_n,_i,_v,_f,_t);
case "fur": return _af_rule(_n,_i,_v,_f,_t);
case "ga": return _ga_rule(_n,_i,_v,_f,_t);
case "gl": return _ast_rule(_n,_i,_v,_f,_t);
case "gsw": return _af_rule(_n,_i,_v,_f,_t);
case "gu": return _am_rule(_n,_i,_v,_f,_t);
case "haw": return _af_rule(_n,_i,_v,_f,_t);
case "he": return _he_rule(_n,_i,_v,_f,_t);
case "hi": return _am_rule(_n,_i,_v,_f,_t);
case "hr": return _bs_rule(_n,_i,_v,_f,_t);
case "hu": return _af_rule(_n,_i,_v,_f,_t);
case "hy": return _ff_rule(_n,_i,_v,_f,_t);
case "id": return _default_rule(_n,_i,_v,_f,_t);
case "in": return _default_rule(_n,_i,_v,_f,_t);
case "is": return _is_rule(_n,_i,_v,_f,_t);
case "it": return _ca_rule(_n,_i,_v,_f,_t);
case "it_CH": return _ca_rule(_n,_i,_v,_f,_t);
case "iw": return _he_rule(_n,_i,_v,_f,_t);
case "ja": return _default_rule(_n,_i,_v,_f,_t);
case "ka": return _af_rule(_n,_i,_v,_f,_t);
case "kk": return _af_rule(_n,_i,_v,_f,_t);
case "km": return _default_rule(_n,_i,_v,_f,_t);
case "kn": return _am_rule(_n,_i,_v,_f,_t);
case "ko": return _default_rule(_n,_i,_v,_f,_t);
case "ky": return _af_rule(_n,_i,_v,_f,_t);
case "ln": return _ak_rule(_n,_i,_v,_f,_t);
case "lo": return _default_rule(_n,_i,_v,_f,_t);
case "lt": return _lt_rule(_n,_i,_v,_f,_t);
case "lv": return _lv_rule(_n,_i,_v,_f,_t);
case "mg": return _ak_rule(_n,_i,_v,_f,_t);
case "mk": return _mk_rule(_n,_i,_v,_f,_t);
case "ml": return _af_rule(_n,_i,_v,_f,_t);
case "mn": return _af_rule(_n,_i,_v,_f,_t);
case "mr": return _af_rule(_n,_i,_v,_f,_t);
case "ms": return _default_rule(_n,_i,_v,_f,_t);
case "mt": return _mt_rule(_n,_i,_v,_f,_t);
case "my": return _default_rule(_n,_i,_v,_f,_t);
case "nb": return _af_rule(_n,_i,_v,_f,_t);
case "ne": return _af_rule(_n,_i,_v,_f,_t);
case "nl": return _ast_rule(_n,_i,_v,_f,_t);
case "no": return _af_rule(_n,_i,_v,_f,_t);
case "no_NO": return _af_rule(_n,_i,_v,_f,_t);
case "nyn": return _af_rule(_n,_i,_v,_f,_t);
case "or": return _af_rule(_n,_i,_v,_f,_t);
case "pa": return _ak_rule(_n,_i,_v,_f,_t);
case "pl": return _pl_rule(_n,_i,_v,_f,_t);
case "ps": return _af_rule(_n,_i,_v,_f,_t);
case "pt": return _pt_rule(_n,_i,_v,_f,_t);
case "pt_BR": return _pt_rule(_n,_i,_v,_f,_t);
case "pt_PT": return _ca_rule(_n,_i,_v,_f,_t);
case "ro": return _mo_rule(_n,_i,_v,_f,_t);
case "ru": return _ru_rule(_n,_i,_v,_f,_t);
case "si": return _si_rule(_n,_i,_v,_f,_t);
case "sk": return _cs_rule(_n,_i,_v,_f,_t);
case "sl": return _sl_rule(_n,_i,_v,_f,_t);
case "sq": return _af_rule(_n,_i,_v,_f,_t);
case "sr": return _bs_rule(_n,_i,_v,_f,_t);
case "sr_Latn": return _bs_rule(_n,_i,_v,_f,_t);
case "sv": return _ast_rule(_n,_i,_v,_f,_t);
case "sw": return _ast_rule(_n,_i,_v,_f,_t);
case "ta": return _af_rule(_n,_i,_v,_f,_t);
case "te": return _af_rule(_n,_i,_v,_f,_t);
case "th": return _default_rule(_n,_i,_v,_f,_t);
case "tl": return _ceb_rule(_n,_i,_v,_f,_t);
case "tr": return _af_rule(_n,_i,_v,_f,_t);
case "uk": return _ru_rule(_n,_i,_v,_f,_t);
case "ur": return _ast_rule(_n,_i,_v,_f,_t);
case "uz": return _af_rule(_n,_i,_v,_f,_t);
case "vi": return _default_rule(_n,_i,_v,_f,_t);
case "zh": return _default_rule(_n,_i,_v,_f,_t);
case "zh_CN": return _default_rule(_n,_i,_v,_f,_t);
case "zh_HK": return _default_rule(_n,_i,_v,_f,_t);
case "zh_TW": return _default_rule(_n,_i,_v,_f,_t);
case "zu": return _am_rule(_n,_i,_v,_f,_t);
case "default": return _default_rule(_n,_i,_v,_f,_t);
} } return "other"; }
public static string Choose(string locale,double value,int available) {
if(value==0 && (available&1)!=0) return "zero";
if(value==1 && (available&2)!=0) return "one";
if(value==2 && (available&4)!=0) return "two";
string category=Select(locale,value);
int mask=category switch { "zero"=>1,"one"=>2,"two"=>4,"few"=>8,"many"=>16,_=>32 };
if((available&mask)!=0) return category;
return category=="two" && (available&8)!=0 ? "few" : "other";
}
private static string _default_rule(double _n,long _i,int _v,long _f,long _t) {return "other";}
private static string _ast_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  return "other";}
private static string _af_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  return "other";}
private static string _am_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_i == 0) || (_n == 1)) {
    return "one";
  }
  return "other";}
private static string _ar_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_n == 0) {
    return "zero";
  }
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if ((Mod(_n,100) == 3 || Mod(_n,100) == 4 || Mod(_n,100) == 5 || Mod(_n,100) == 6 || Mod(_n,100) == 7 || Mod(_n,100) == 8 || Mod(_n,100) == 9 || Mod(_n,100) == 10)) {
    return "few";
  }
  if ((Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19 || Mod(_n,100) == 20 || Mod(_n,100) == 21 || Mod(_n,100) == 22 || Mod(_n,100) == 23 || Mod(_n,100) == 24 || Mod(_n,100) == 25 || Mod(_n,100) == 26 || Mod(_n,100) == 27 || Mod(_n,100) == 28 || Mod(_n,100) == 29 || Mod(_n,100) == 30 || Mod(_n,100) == 31 || Mod(_n,100) == 32 || Mod(_n,100) == 33 || Mod(_n,100) == 34 || Mod(_n,100) == 35 || Mod(_n,100) == 36 || Mod(_n,100) == 37 || Mod(_n,100) == 38 || Mod(_n,100) == 39 || Mod(_n,100) == 40 || Mod(_n,100) == 41 || Mod(_n,100) == 42 || Mod(_n,100) == 43 || Mod(_n,100) == 44 || Mod(_n,100) == 45 || Mod(_n,100) == 46 || Mod(_n,100) == 47 || Mod(_n,100) == 48 || Mod(_n,100) == 49 || Mod(_n,100) == 50 || Mod(_n,100) == 51 || Mod(_n,100) == 52 || Mod(_n,100) == 53 || Mod(_n,100) == 54 || Mod(_n,100) == 55 || Mod(_n,100) == 56 || Mod(_n,100) == 57 || Mod(_n,100) == 58 || Mod(_n,100) == 59 || Mod(_n,100) == 60 || Mod(_n,100) == 61 || Mod(_n,100) == 62 || Mod(_n,100) == 63 || Mod(_n,100) == 64 || Mod(_n,100) == 65 || Mod(_n,100) == 66 || Mod(_n,100) == 67 || Mod(_n,100) == 68 || Mod(_n,100) == 69 || Mod(_n,100) == 70 || Mod(_n,100) == 71 || Mod(_n,100) == 72 || Mod(_n,100) == 73 || Mod(_n,100) == 74 || Mod(_n,100) == 75 || Mod(_n,100) == 76 || Mod(_n,100) == 77 || Mod(_n,100) == 78 || Mod(_n,100) == 79 || Mod(_n,100) == 80 || Mod(_n,100) == 81 || Mod(_n,100) == 82 || Mod(_n,100) == 83 || Mod(_n,100) == 84 || Mod(_n,100) == 85 || Mod(_n,100) == 86 || Mod(_n,100) == 87 || Mod(_n,100) == 88 || Mod(_n,100) == 89 || Mod(_n,100) == 90 || Mod(_n,100) == 91 || Mod(_n,100) == 92 || Mod(_n,100) == 93 || Mod(_n,100) == 94 || Mod(_n,100) == 95 || Mod(_n,100) == 96 || Mod(_n,100) == 97 || Mod(_n,100) == 98 || Mod(_n,100) == 99)) {
    return "many";
  }
  return "other";}
private static string _be_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 1) && !(Mod(_n,100) == 11)) {
    return "one";
  }
  if ((Mod(_n,10) == 2 || Mod(_n,10) == 3 || Mod(_n,10) == 4) &&
      !(Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14)) {
    return "few";
  }
  if ((Mod(_n,10) == 0) ||
      (Mod(_n,10) == 5 ||
          Mod(_n,10) == 6 ||
          Mod(_n,10) == 7 ||
          Mod(_n,10) == 8 ||
          Mod(_n,10) == 9) ||
      (Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14)) {
    return "many";
  }
  return "other";}
private static string _br_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 1) && !(Mod(_n,100) == 11 || Mod(_n,100) == 71 || Mod(_n,100) == 91)) {
    return "one";
  }
  if ((Mod(_n,10) == 2) && !(Mod(_n,100) == 12 || Mod(_n,100) == 72 || Mod(_n,100) == 92)) {
    return "two";
  }
  if ((Mod(_n,10) == 3 || Mod(_n,10) == 4 || Mod(_n,10) == 9) &&
      !((Mod(_n,100) == 10 || Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19) ||
          (Mod(_n,100) == 70 || Mod(_n,100) == 71 || Mod(_n,100) == 72 || Mod(_n,100) == 73 || Mod(_n,100) == 74 || Mod(_n,100) == 75 || Mod(_n,100) == 76 || Mod(_n,100) == 77 || Mod(_n,100) == 78 || Mod(_n,100) == 79) ||
          (Mod(_n,100) == 90 || Mod(_n,100) == 91 || Mod(_n,100) == 92 || Mod(_n,100) == 93 || Mod(_n,100) == 94 || Mod(_n,100) == 95 || Mod(_n,100) == 96 || Mod(_n,100) == 97 || Mod(_n,100) == 98 || Mod(_n,100) == 99))) {
    return "few";
  }
  if (!(_n == 0) && (Mod(_n,1000000) == 0)) {
    return "many";
  }
  return "other";}
private static string _bs_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11) ||
      (Mod(_f,10) == 1) && !(Mod(_f,100) == 11)) {
    return "one";
  }
  if ((_v == 0) &&
          (Mod(_i,10) == 2 || Mod(_i,10) == 3 || Mod(_i,10) == 4) &&
          !(Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14) ||
      (Mod(_f,10) == 2 || Mod(_f,10) == 3 || Mod(_f,10) == 4) &&
          !(Mod(_f,100) == 12 || Mod(_f,100) == 13 || Mod(_f,100) == 14)) {
    return "few";
  }
  return "other";}
private static string _ca_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _cs_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if ((_i == 2 || _i == 3 || _i == 4) && (_v == 0)) {
    return "few";
  }
  if (!(_v == 0)) {
    return "many";
  }
  return "other";}
private static string _cy_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_n == 0) {
    return "zero";
  }
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if (_n == 3) {
    return "few";
  }
  if (_n == 6) {
    return "many";
  }
  return "other";}
private static string _da_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_n == 1) || !(_t == 0) && (_i == 0 || _i == 1)) {
    return "one";
  }
  return "other";}
private static string _es_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _ceb_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (_i == 1 || _i == 2 || _i == 3) ||
      (_v == 0) && !(Mod(_i,10) == 4 || Mod(_i,10) == 6 || Mod(_i,10) == 9) ||
      !(_v == 0) && !(Mod(_f,10) == 4 || Mod(_f,10) == 6 || Mod(_f,10) == 9)) {
    return "one";
  }
  return "other";}
private static string _fr_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_i == 0 || _i == 1) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _ga_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if (_n == 3 || _n == 4 || _n == 5 || _n == 6) {
    return "few";
  }
  if (_n == 7 || _n == 8 || _n == 9 || _n == 10) {
    return "many";
  }
  return "other";}
private static string _he_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0) || (_i == 0) && !(_v == 0)) {
    return "one";
  }
  if ((_i == 2) && (_v == 0)) {
    return "two";
  }
  return "other";}
private static string _ff_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_i == 0 || _i == 1) {
    return "one";
  }
  return "other";}
private static string _is_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_t == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11) ||
      (Mod(_t,10) == 1) && !(Mod(_t,100) == 11)) {
    return "one";
  }
  return "other";}
private static string _ak_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_n == 0 || _n == 1) {
    return "one";
  }
  return "other";}
private static string _lt_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 1) &&
      !(Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) {
    return "one";
  }
  if (((Mod(_n,10) == 2 || Mod(_n,10) == 3 || Mod(_n,10) == 4 || Mod(_n,10) == 5 || Mod(_n,10) == 6 || Mod(_n,10) == 7 || Mod(_n,10) == 8 || Mod(_n,10) == 9)) &&
      !(Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) {
    return "few";
  }
  if (!(_f == 0)) {
    return "many";
  }
  return "other";}
private static string _lv_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 0) ||
      ((Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) ||
      (_v == 2) && ((Mod(_f,100) == 11 || Mod(_f,100) == 12 || Mod(_f,100) == 13 || Mod(_f,100) == 14 || Mod(_f,100) == 15 || Mod(_f,100) == 16 || Mod(_f,100) == 17 || Mod(_f,100) == 18 || Mod(_f,100) == 19))) {
    return "zero";
  }
  if ((Mod(_n,10) == 1) && !(Mod(_n,100) == 11) ||
      (_v == 2) && (Mod(_f,10) == 1) && !(Mod(_f,100) == 11) ||
      !(_v == 2) && (Mod(_f,10) == 1)) {
    return "one";
  }
  return "other";}
private static string _mk_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11) ||
      (Mod(_f,10) == 1) && !(Mod(_f,100) == 11)) {
    return "one";
  }
  return "other";}
private static string _mt_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if ((_n == 0) || ((Mod(_n,100) == 3 || Mod(_n,100) == 4 || Mod(_n,100) == 5 || Mod(_n,100) == 6 || Mod(_n,100) == 7 || Mod(_n,100) == 8 || Mod(_n,100) == 9 || Mod(_n,100) == 10))) {
    return "few";
  }
  if ((Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) {
    return "many";
  }
  return "other";}
private static string _pl_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if ((_v == 0) &&
      (Mod(_i,10) == 2 || Mod(_i,10) == 3 || Mod(_i,10) == 4) &&
      !(Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14)) {
    return "few";
  }
  if ((_v == 0) && !(_i == 1) && (Mod(_i,10) == 0 || Mod(_i,10) == 1) ||
      (_v == 0) &&
          (Mod(_i,10) == 5 ||
              Mod(_i,10) == 6 ||
              Mod(_i,10) == 7 ||
              Mod(_i,10) == 8 ||
              Mod(_i,10) == 9) ||
      (_v == 0) && (Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14)) {
    return "many";
  }
  return "other";}
private static string _pt_rule(double _n,long _i,int _v,long _f,long _t) {
  if (_i == 0 || _i == 1) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _mo_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if (!(_v == 0) ||
      (_n == 0) ||
      !(_n == 1) && ((Mod(_n,100) == 1 || Mod(_n,100) == 2 || Mod(_n,100) == 3 || Mod(_n,100) == 4 || Mod(_n,100) == 5 || Mod(_n,100) == 6 || Mod(_n,100) == 7 || Mod(_n,100) == 8 || Mod(_n,100) == 9 || Mod(_n,100) == 10 || Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19))) {
    return "few";
  }
  return "other";}
private static string _ru_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11)) {
    return "one";
  }
  if ((_v == 0) &&
      (Mod(_i,10) == 2 || Mod(_i,10) == 3 || Mod(_i,10) == 4) &&
      !(Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14)) {
    return "few";
  }
  if ((_v == 0) && (Mod(_i,10) == 0) ||
      (_v == 0) &&
          (Mod(_i,10) == 5 ||
              Mod(_i,10) == 6 ||
              Mod(_i,10) == 7 ||
              Mod(_i,10) == 8 ||
              Mod(_i,10) == 9) ||
      (_v == 0) &&
          (Mod(_i,100) == 11 ||
              Mod(_i,100) == 12 ||
              Mod(_i,100) == 13 ||
              Mod(_i,100) == 14)) {
    return "many";
  }
  return "other";}
private static string _si_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_n == 0 || _n == 1) || (_i == 0) && (_f == 1)) {
    return "one";
  }
  return "other";}
private static string _sl_rule(double _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,100) == 1)) {
    return "one";
  }
  if ((_v == 0) && (Mod(_i,100) == 2)) {
    return "two";
  }
  if ((_v == 0) && (Mod(_i,100) == 3 || Mod(_i,100) == 4) || !(_v == 0)) {
    return "few";
  }
  return "other";}
private static long DartInteger(double v)=>v>=long.MaxValue?long.MaxValue:v<=long.MinValue?long.MinValue:(long)v;
private static long Mod(long a,long b) => (a%b+b)%b;
public static string SelectInteger(string locale,long value) { foreach(var candidate in IntlLocale.Candidates(locale)) { switch(candidate) {
case "en_ISO": return _ast_ruleInteger(value,value,0,0,0);
case "af": return _af_ruleInteger(value,value,0,0,0);
case "am": return _am_ruleInteger(value,value,0,0,0);
case "ar": return _ar_ruleInteger(value,value,0,0,0);
case "ar_DZ": return _ar_ruleInteger(value,value,0,0,0);
case "ar_EG": return _ar_ruleInteger(value,value,0,0,0);
case "as": return _am_ruleInteger(value,value,0,0,0);
case "az": return _af_ruleInteger(value,value,0,0,0);
case "be": return _be_ruleInteger(value,value,0,0,0);
case "bg": return _af_ruleInteger(value,value,0,0,0);
case "bm": return _default_ruleInteger(value,value,0,0,0);
case "bn": return _am_ruleInteger(value,value,0,0,0);
case "br": return _br_ruleInteger(value,value,0,0,0);
case "bs": return _bs_ruleInteger(value,value,0,0,0);
case "ca": return _ca_ruleInteger(value,value,0,0,0);
case "chr": return _af_ruleInteger(value,value,0,0,0);
case "cs": return _cs_ruleInteger(value,value,0,0,0);
case "cy": return _cy_ruleInteger(value,value,0,0,0);
case "da": return _da_ruleInteger(value,value,0,0,0);
case "de": return _ast_ruleInteger(value,value,0,0,0);
case "de_AT": return _ast_ruleInteger(value,value,0,0,0);
case "de_CH": return _ast_ruleInteger(value,value,0,0,0);
case "el": return _af_ruleInteger(value,value,0,0,0);
case "en": return _ast_ruleInteger(value,value,0,0,0);
case "en_AU": return _ast_ruleInteger(value,value,0,0,0);
case "en_CA": return _ast_ruleInteger(value,value,0,0,0);
case "en_GB": return _ast_ruleInteger(value,value,0,0,0);
case "en_IE": return _ast_ruleInteger(value,value,0,0,0);
case "en_IN": return _ast_ruleInteger(value,value,0,0,0);
case "en_MY": return _ast_ruleInteger(value,value,0,0,0);
case "en_NZ": return _ast_ruleInteger(value,value,0,0,0);
case "en_SG": return _ast_ruleInteger(value,value,0,0,0);
case "en_US": return _ast_ruleInteger(value,value,0,0,0);
case "en_ZA": return _ast_ruleInteger(value,value,0,0,0);
case "es": return _es_ruleInteger(value,value,0,0,0);
case "es_419": return _es_ruleInteger(value,value,0,0,0);
case "es_ES": return _es_ruleInteger(value,value,0,0,0);
case "es_MX": return _es_ruleInteger(value,value,0,0,0);
case "es_US": return _es_ruleInteger(value,value,0,0,0);
case "et": return _ast_ruleInteger(value,value,0,0,0);
case "eu": return _af_ruleInteger(value,value,0,0,0);
case "fa": return _am_ruleInteger(value,value,0,0,0);
case "fi": return _ast_ruleInteger(value,value,0,0,0);
case "fil": return _ceb_ruleInteger(value,value,0,0,0);
case "fr": return _fr_ruleInteger(value,value,0,0,0);
case "fr_CA": return _fr_ruleInteger(value,value,0,0,0);
case "fr_CH": return _fr_ruleInteger(value,value,0,0,0);
case "fur": return _af_ruleInteger(value,value,0,0,0);
case "ga": return _ga_ruleInteger(value,value,0,0,0);
case "gl": return _ast_ruleInteger(value,value,0,0,0);
case "gsw": return _af_ruleInteger(value,value,0,0,0);
case "gu": return _am_ruleInteger(value,value,0,0,0);
case "haw": return _af_ruleInteger(value,value,0,0,0);
case "he": return _he_ruleInteger(value,value,0,0,0);
case "hi": return _am_ruleInteger(value,value,0,0,0);
case "hr": return _bs_ruleInteger(value,value,0,0,0);
case "hu": return _af_ruleInteger(value,value,0,0,0);
case "hy": return _ff_ruleInteger(value,value,0,0,0);
case "id": return _default_ruleInteger(value,value,0,0,0);
case "in": return _default_ruleInteger(value,value,0,0,0);
case "is": return _is_ruleInteger(value,value,0,0,0);
case "it": return _ca_ruleInteger(value,value,0,0,0);
case "it_CH": return _ca_ruleInteger(value,value,0,0,0);
case "iw": return _he_ruleInteger(value,value,0,0,0);
case "ja": return _default_ruleInteger(value,value,0,0,0);
case "ka": return _af_ruleInteger(value,value,0,0,0);
case "kk": return _af_ruleInteger(value,value,0,0,0);
case "km": return _default_ruleInteger(value,value,0,0,0);
case "kn": return _am_ruleInteger(value,value,0,0,0);
case "ko": return _default_ruleInteger(value,value,0,0,0);
case "ky": return _af_ruleInteger(value,value,0,0,0);
case "ln": return _ak_ruleInteger(value,value,0,0,0);
case "lo": return _default_ruleInteger(value,value,0,0,0);
case "lt": return _lt_ruleInteger(value,value,0,0,0);
case "lv": return _lv_ruleInteger(value,value,0,0,0);
case "mg": return _ak_ruleInteger(value,value,0,0,0);
case "mk": return _mk_ruleInteger(value,value,0,0,0);
case "ml": return _af_ruleInteger(value,value,0,0,0);
case "mn": return _af_ruleInteger(value,value,0,0,0);
case "mr": return _af_ruleInteger(value,value,0,0,0);
case "ms": return _default_ruleInteger(value,value,0,0,0);
case "mt": return _mt_ruleInteger(value,value,0,0,0);
case "my": return _default_ruleInteger(value,value,0,0,0);
case "nb": return _af_ruleInteger(value,value,0,0,0);
case "ne": return _af_ruleInteger(value,value,0,0,0);
case "nl": return _ast_ruleInteger(value,value,0,0,0);
case "no": return _af_ruleInteger(value,value,0,0,0);
case "no_NO": return _af_ruleInteger(value,value,0,0,0);
case "nyn": return _af_ruleInteger(value,value,0,0,0);
case "or": return _af_ruleInteger(value,value,0,0,0);
case "pa": return _ak_ruleInteger(value,value,0,0,0);
case "pl": return _pl_ruleInteger(value,value,0,0,0);
case "ps": return _af_ruleInteger(value,value,0,0,0);
case "pt": return _pt_ruleInteger(value,value,0,0,0);
case "pt_BR": return _pt_ruleInteger(value,value,0,0,0);
case "pt_PT": return _ca_ruleInteger(value,value,0,0,0);
case "ro": return _mo_ruleInteger(value,value,0,0,0);
case "ru": return _ru_ruleInteger(value,value,0,0,0);
case "si": return _si_ruleInteger(value,value,0,0,0);
case "sk": return _cs_ruleInteger(value,value,0,0,0);
case "sl": return _sl_ruleInteger(value,value,0,0,0);
case "sq": return _af_ruleInteger(value,value,0,0,0);
case "sr": return _bs_ruleInteger(value,value,0,0,0);
case "sr_Latn": return _bs_ruleInteger(value,value,0,0,0);
case "sv": return _ast_ruleInteger(value,value,0,0,0);
case "sw": return _ast_ruleInteger(value,value,0,0,0);
case "ta": return _af_ruleInteger(value,value,0,0,0);
case "te": return _af_ruleInteger(value,value,0,0,0);
case "th": return _default_ruleInteger(value,value,0,0,0);
case "tl": return _ceb_ruleInteger(value,value,0,0,0);
case "tr": return _af_ruleInteger(value,value,0,0,0);
case "uk": return _ru_ruleInteger(value,value,0,0,0);
case "ur": return _ast_ruleInteger(value,value,0,0,0);
case "uz": return _af_ruleInteger(value,value,0,0,0);
case "vi": return _default_ruleInteger(value,value,0,0,0);
case "zh": return _default_ruleInteger(value,value,0,0,0);
case "zh_CN": return _default_ruleInteger(value,value,0,0,0);
case "zh_HK": return _default_ruleInteger(value,value,0,0,0);
case "zh_TW": return _default_ruleInteger(value,value,0,0,0);
case "zu": return _am_ruleInteger(value,value,0,0,0);
case "default": return _default_ruleInteger(value,value,0,0,0);
} } return "other"; }
public static string Choose(string locale,long value,int available) => Choose(locale,new L10nNumber(value),available);
public static string Choose(string locale,L10nNumber value,int available) {
if(!value.IsInteger)return Choose(locale,value.Double,available);
long n=value.Integer;
if(n==0 && (available&1)!=0)return "zero";
if(n==1 && (available&2)!=0)return "one";
if(n==2 && (available&4)!=0)return "two";
string category=SelectInteger(locale,n);
int mask=category switch { "zero"=>1,"one"=>2,"two"=>4,"few"=>8,"many"=>16,_=>32 };
return (available&mask)!=0?category:category=="two" && (available&8)!=0?"few":"other";
}
private static string _default_ruleInteger(long _n,long _i,int _v,long _f,long _t) {return "other";}
private static string _ast_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  return "other";}
private static string _af_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  return "other";}
private static string _am_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_i == 0) || (_n == 1)) {
    return "one";
  }
  return "other";}
private static string _ar_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_n == 0) {
    return "zero";
  }
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if ((Mod(_n,100) == 3 || Mod(_n,100) == 4 || Mod(_n,100) == 5 || Mod(_n,100) == 6 || Mod(_n,100) == 7 || Mod(_n,100) == 8 || Mod(_n,100) == 9 || Mod(_n,100) == 10)) {
    return "few";
  }
  if ((Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19 || Mod(_n,100) == 20 || Mod(_n,100) == 21 || Mod(_n,100) == 22 || Mod(_n,100) == 23 || Mod(_n,100) == 24 || Mod(_n,100) == 25 || Mod(_n,100) == 26 || Mod(_n,100) == 27 || Mod(_n,100) == 28 || Mod(_n,100) == 29 || Mod(_n,100) == 30 || Mod(_n,100) == 31 || Mod(_n,100) == 32 || Mod(_n,100) == 33 || Mod(_n,100) == 34 || Mod(_n,100) == 35 || Mod(_n,100) == 36 || Mod(_n,100) == 37 || Mod(_n,100) == 38 || Mod(_n,100) == 39 || Mod(_n,100) == 40 || Mod(_n,100) == 41 || Mod(_n,100) == 42 || Mod(_n,100) == 43 || Mod(_n,100) == 44 || Mod(_n,100) == 45 || Mod(_n,100) == 46 || Mod(_n,100) == 47 || Mod(_n,100) == 48 || Mod(_n,100) == 49 || Mod(_n,100) == 50 || Mod(_n,100) == 51 || Mod(_n,100) == 52 || Mod(_n,100) == 53 || Mod(_n,100) == 54 || Mod(_n,100) == 55 || Mod(_n,100) == 56 || Mod(_n,100) == 57 || Mod(_n,100) == 58 || Mod(_n,100) == 59 || Mod(_n,100) == 60 || Mod(_n,100) == 61 || Mod(_n,100) == 62 || Mod(_n,100) == 63 || Mod(_n,100) == 64 || Mod(_n,100) == 65 || Mod(_n,100) == 66 || Mod(_n,100) == 67 || Mod(_n,100) == 68 || Mod(_n,100) == 69 || Mod(_n,100) == 70 || Mod(_n,100) == 71 || Mod(_n,100) == 72 || Mod(_n,100) == 73 || Mod(_n,100) == 74 || Mod(_n,100) == 75 || Mod(_n,100) == 76 || Mod(_n,100) == 77 || Mod(_n,100) == 78 || Mod(_n,100) == 79 || Mod(_n,100) == 80 || Mod(_n,100) == 81 || Mod(_n,100) == 82 || Mod(_n,100) == 83 || Mod(_n,100) == 84 || Mod(_n,100) == 85 || Mod(_n,100) == 86 || Mod(_n,100) == 87 || Mod(_n,100) == 88 || Mod(_n,100) == 89 || Mod(_n,100) == 90 || Mod(_n,100) == 91 || Mod(_n,100) == 92 || Mod(_n,100) == 93 || Mod(_n,100) == 94 || Mod(_n,100) == 95 || Mod(_n,100) == 96 || Mod(_n,100) == 97 || Mod(_n,100) == 98 || Mod(_n,100) == 99)) {
    return "many";
  }
  return "other";}
private static string _be_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 1) && !(Mod(_n,100) == 11)) {
    return "one";
  }
  if ((Mod(_n,10) == 2 || Mod(_n,10) == 3 || Mod(_n,10) == 4) &&
      !(Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14)) {
    return "few";
  }
  if ((Mod(_n,10) == 0) ||
      (Mod(_n,10) == 5 ||
          Mod(_n,10) == 6 ||
          Mod(_n,10) == 7 ||
          Mod(_n,10) == 8 ||
          Mod(_n,10) == 9) ||
      (Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14)) {
    return "many";
  }
  return "other";}
private static string _br_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 1) && !(Mod(_n,100) == 11 || Mod(_n,100) == 71 || Mod(_n,100) == 91)) {
    return "one";
  }
  if ((Mod(_n,10) == 2) && !(Mod(_n,100) == 12 || Mod(_n,100) == 72 || Mod(_n,100) == 92)) {
    return "two";
  }
  if ((Mod(_n,10) == 3 || Mod(_n,10) == 4 || Mod(_n,10) == 9) &&
      !((Mod(_n,100) == 10 || Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19) ||
          (Mod(_n,100) == 70 || Mod(_n,100) == 71 || Mod(_n,100) == 72 || Mod(_n,100) == 73 || Mod(_n,100) == 74 || Mod(_n,100) == 75 || Mod(_n,100) == 76 || Mod(_n,100) == 77 || Mod(_n,100) == 78 || Mod(_n,100) == 79) ||
          (Mod(_n,100) == 90 || Mod(_n,100) == 91 || Mod(_n,100) == 92 || Mod(_n,100) == 93 || Mod(_n,100) == 94 || Mod(_n,100) == 95 || Mod(_n,100) == 96 || Mod(_n,100) == 97 || Mod(_n,100) == 98 || Mod(_n,100) == 99))) {
    return "few";
  }
  if (!(_n == 0) && (Mod(_n,1000000) == 0)) {
    return "many";
  }
  return "other";}
private static string _bs_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11) ||
      (Mod(_f,10) == 1) && !(Mod(_f,100) == 11)) {
    return "one";
  }
  if ((_v == 0) &&
          (Mod(_i,10) == 2 || Mod(_i,10) == 3 || Mod(_i,10) == 4) &&
          !(Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14) ||
      (Mod(_f,10) == 2 || Mod(_f,10) == 3 || Mod(_f,10) == 4) &&
          !(Mod(_f,100) == 12 || Mod(_f,100) == 13 || Mod(_f,100) == 14)) {
    return "few";
  }
  return "other";}
private static string _ca_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _cs_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if ((_i == 2 || _i == 3 || _i == 4) && (_v == 0)) {
    return "few";
  }
  if (!(_v == 0)) {
    return "many";
  }
  return "other";}
private static string _cy_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_n == 0) {
    return "zero";
  }
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if (_n == 3) {
    return "few";
  }
  if (_n == 6) {
    return "many";
  }
  return "other";}
private static string _da_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_n == 1) || !(_t == 0) && (_i == 0 || _i == 1)) {
    return "one";
  }
  return "other";}
private static string _es_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _ceb_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (_i == 1 || _i == 2 || _i == 3) ||
      (_v == 0) && !(Mod(_i,10) == 4 || Mod(_i,10) == 6 || Mod(_i,10) == 9) ||
      !(_v == 0) && !(Mod(_f,10) == 4 || Mod(_f,10) == 6 || Mod(_f,10) == 9)) {
    return "one";
  }
  return "other";}
private static string _fr_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_i == 0 || _i == 1) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _ga_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if (_n == 3 || _n == 4 || _n == 5 || _n == 6) {
    return "few";
  }
  if (_n == 7 || _n == 8 || _n == 9 || _n == 10) {
    return "many";
  }
  return "other";}
private static string _he_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0) || (_i == 0) && !(_v == 0)) {
    return "one";
  }
  if ((_i == 2) && (_v == 0)) {
    return "two";
  }
  return "other";}
private static string _ff_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_i == 0 || _i == 1) {
    return "one";
  }
  return "other";}
private static string _is_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_t == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11) ||
      (Mod(_t,10) == 1) && !(Mod(_t,100) == 11)) {
    return "one";
  }
  return "other";}
private static string _ak_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_n == 0 || _n == 1) {
    return "one";
  }
  return "other";}
private static string _lt_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 1) &&
      !(Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) {
    return "one";
  }
  if (((Mod(_n,10) == 2 || Mod(_n,10) == 3 || Mod(_n,10) == 4 || Mod(_n,10) == 5 || Mod(_n,10) == 6 || Mod(_n,10) == 7 || Mod(_n,10) == 8 || Mod(_n,10) == 9)) &&
      !(Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) {
    return "few";
  }
  if (!(_f == 0)) {
    return "many";
  }
  return "other";}
private static string _lv_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((Mod(_n,10) == 0) ||
      ((Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) ||
      (_v == 2) && ((Mod(_f,100) == 11 || Mod(_f,100) == 12 || Mod(_f,100) == 13 || Mod(_f,100) == 14 || Mod(_f,100) == 15 || Mod(_f,100) == 16 || Mod(_f,100) == 17 || Mod(_f,100) == 18 || Mod(_f,100) == 19))) {
    return "zero";
  }
  if ((Mod(_n,10) == 1) && !(Mod(_n,100) == 11) ||
      (_v == 2) && (Mod(_f,10) == 1) && !(Mod(_f,100) == 11) ||
      !(_v == 2) && (Mod(_f,10) == 1)) {
    return "one";
  }
  return "other";}
private static string _mk_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11) ||
      (Mod(_f,10) == 1) && !(Mod(_f,100) == 11)) {
    return "one";
  }
  return "other";}
private static string _mt_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_n == 1) {
    return "one";
  }
  if (_n == 2) {
    return "two";
  }
  if ((_n == 0) || ((Mod(_n,100) == 3 || Mod(_n,100) == 4 || Mod(_n,100) == 5 || Mod(_n,100) == 6 || Mod(_n,100) == 7 || Mod(_n,100) == 8 || Mod(_n,100) == 9 || Mod(_n,100) == 10))) {
    return "few";
  }
  if ((Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19)) {
    return "many";
  }
  return "other";}
private static string _pl_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if ((_v == 0) &&
      (Mod(_i,10) == 2 || Mod(_i,10) == 3 || Mod(_i,10) == 4) &&
      !(Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14)) {
    return "few";
  }
  if ((_v == 0) && !(_i == 1) && (Mod(_i,10) == 0 || Mod(_i,10) == 1) ||
      (_v == 0) &&
          (Mod(_i,10) == 5 ||
              Mod(_i,10) == 6 ||
              Mod(_i,10) == 7 ||
              Mod(_i,10) == 8 ||
              Mod(_i,10) == 9) ||
      (_v == 0) && (Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14)) {
    return "many";
  }
  return "other";}
private static string _pt_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if (_i == 0 || _i == 1) {
    return "one";
  }
  if (!(_i == 0) && (Mod(_i,1000000) == 0) && (_v == 0)) {
    return "many";
  }
  return "other";}
private static string _mo_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_i == 1) && (_v == 0)) {
    return "one";
  }
  if (!(_v == 0) ||
      (_n == 0) ||
      !(_n == 1) && ((Mod(_n,100) == 1 || Mod(_n,100) == 2 || Mod(_n,100) == 3 || Mod(_n,100) == 4 || Mod(_n,100) == 5 || Mod(_n,100) == 6 || Mod(_n,100) == 7 || Mod(_n,100) == 8 || Mod(_n,100) == 9 || Mod(_n,100) == 10 || Mod(_n,100) == 11 || Mod(_n,100) == 12 || Mod(_n,100) == 13 || Mod(_n,100) == 14 || Mod(_n,100) == 15 || Mod(_n,100) == 16 || Mod(_n,100) == 17 || Mod(_n,100) == 18 || Mod(_n,100) == 19))) {
    return "few";
  }
  return "other";}
private static string _ru_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,10) == 1) && !(Mod(_i,100) == 11)) {
    return "one";
  }
  if ((_v == 0) &&
      (Mod(_i,10) == 2 || Mod(_i,10) == 3 || Mod(_i,10) == 4) &&
      !(Mod(_i,100) == 12 || Mod(_i,100) == 13 || Mod(_i,100) == 14)) {
    return "few";
  }
  if ((_v == 0) && (Mod(_i,10) == 0) ||
      (_v == 0) &&
          (Mod(_i,10) == 5 ||
              Mod(_i,10) == 6 ||
              Mod(_i,10) == 7 ||
              Mod(_i,10) == 8 ||
              Mod(_i,10) == 9) ||
      (_v == 0) &&
          (Mod(_i,100) == 11 ||
              Mod(_i,100) == 12 ||
              Mod(_i,100) == 13 ||
              Mod(_i,100) == 14)) {
    return "many";
  }
  return "other";}
private static string _si_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_n == 0 || _n == 1) || (_i == 0) && (_f == 1)) {
    return "one";
  }
  return "other";}
private static string _sl_ruleInteger(long _n,long _i,int _v,long _f,long _t) {
  if ((_v == 0) && (Mod(_i,100) == 1)) {
    return "one";
  }
  if ((_v == 0) && (Mod(_i,100) == 2)) {
    return "two";
  }
  if ((_v == 0) && (Mod(_i,100) == 3 || Mod(_i,100) == 4) || !(_v == 0)) {
    return "few";
  }
  return "other";}
}
