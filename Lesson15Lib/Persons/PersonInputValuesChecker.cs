using System.Text.RegularExpressions;
using Lesson15Lib.Exceptions;

namespace Lesson15Lib.Persons;

internal class PersonInputValuesChecker
{
    internal static string CheckName(string value, string message)
    {
        if (string.IsNullOrEmpty(value))
            throw new PersonException(message);
        var pattern = @"^[a-zA-Z''-'\s]{1,20}$";
        if (Regex.IsMatch(value, pattern))
            return FixNameFormat(value);
        throw new PersonException(message);
    }

    internal static string CheckPhoneNumber(string value, string message)
    {
        if (string.IsNullOrEmpty(value))
            throw new PersonException(message);
        var pattern = @"^(?:\+38)?(0[1-9][0-9]{8})$";
        if (Regex.IsMatch(value, pattern))
            return FixPhoneNumberFormat(value);
        throw new PersonException(message);
    }

    private static string FixNameFormat(string value)
    {
        var pattern = @"\b\w";
        return Regex.Replace(value, pattern, match => match.Value.ToUpper());
    }

    private static string FixPhoneNumberFormat(string value) => $"+38{value}";
}