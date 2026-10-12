using System;
namespace DotNative.Localization;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
public sealed class ArbKeyAttribute : Attribute
{
    public ArbKeyAttribute(string key) => Key = key;
    public string Key { get; }
}
