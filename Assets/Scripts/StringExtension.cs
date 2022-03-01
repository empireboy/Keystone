public static class StringExtension
{
    public static bool EqualsWithoutSpaces(this string originalValue, string otherValue)
    {
        return string.Equals(originalValue.Replace(" ", string.Empty), otherValue.Replace(" ", string.Empty));
    }
}