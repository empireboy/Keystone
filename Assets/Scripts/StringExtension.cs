public static class StringExtension
{
    public static bool EqualsWithoutSpaces(this string originalValue, string otherValue)
    {
        return string.Equals(originalValue.RemoveSpaces(), otherValue.RemoveSpaces());
    }

    public static string RemoveSpaces(this string value)
    {
        return value.Replace(" ", string.Empty);
    }
}