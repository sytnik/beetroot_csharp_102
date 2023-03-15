namespace Lesson7;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
    }

    static bool Compare(string str1, string str2)
    {
        if (str1.Length != str2.Length)
        {
            return false;
        }

        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                return false;
            }
        }

        return true;
    }

    static void Analyze(string input, out int numAlphabetic, out int numDigits, out int numSpecial)
    {
        numAlphabetic = 0;
        numDigits = 0;
        numSpecial = 0;
        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                numAlphabetic++;
            }
            else if (char.IsDigit(c))
            {
                numDigits++;
            }
            else // if not letter or digit
            {
                numSpecial++;
            }
        }
    }
    
    public static string Sort(string str)
    {
        char[] chars = str.ToCharArray();
        Array.Sort(chars, StringComparer.OrdinalIgnoreCase);
        str = new string(chars);
        return str;
    }

    public static char[] Duplicate(string str)
    {
        char[] duplicates = new char[str.Length];
        char[] seen = new char[str.Length];
        int duplicateIndex = 0;
        int seenIndex = 0;

        foreach (char c in str)
        {
            bool isDuplicate = false;
            for (int i = 0; i < seenIndex; i++)
            {
                if (c == seen[i])
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (isDuplicate)
            {
                bool isAlreadyAdded = false;
                for (int i = 0; i < duplicateIndex; i++)
                {
                    if (c == duplicates[i])
                    {
                        isAlreadyAdded = true;
                        break;
                    }
                }
                if (!isAlreadyAdded)
                {
                    duplicates[duplicateIndex++] = c;
                }
            }
            else
            {
                seen[seenIndex++] = c;
            }
        }

        Array.Resize(ref duplicates, duplicateIndex);

        return duplicates;
    }
    
    public static List<char> DuplicateUsingList(string str)
    {
        List<char> duplicates = new List<char>();
        List<char> seen = new List<char>();

        foreach (char c in str)
        {
            if (seen.Contains(c) && !duplicates.Contains(c))
            {
                duplicates.Add(c);
            }
            else // if is not seen or is in the duplicates list
            {
                seen.Add(c);
            }
        }

        return duplicates;
    }
}