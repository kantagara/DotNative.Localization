using System;
using System.Globalization;
namespace DotNative.Localization;
/// <summary>Dart num semantics: retains whether a value was an int or a double.</summary>
public readonly struct L10nNumber : IFormattable
{
    private readonly long integer;
    private readonly double floating;
    public bool IsInteger { get; }
    public long Integer => IsInteger ? integer : throw new InvalidOperationException("Value is a double.");
    public double Double => IsInteger ? integer : floating;
    public L10nNumber(long value) { integer = value; floating = 0; IsInteger = true; }
    public L10nNumber(double value) { floating = value; integer = 0; IsInteger = false; }
    public static implicit operator L10nNumber(int value) => new((long)value);
    public static implicit operator L10nNumber(long value) => new(value);
    public static implicit operator L10nNumber(double value) => new(value);
    public override string ToString() => IsInteger ? integer.ToString(CultureInfo.InvariantCulture) : Formatting.RawDouble(floating);
    public string ToString(string? format, IFormatProvider? provider) => ToString();
}
