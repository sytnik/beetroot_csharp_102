using System.Text;

namespace Lesson7;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        List<char> chs = DuplicateUsingList("hello world");
        char[] charArr = Duplicate("hello world");
        StrInterpolation();
    }

    static void StrInterpolation()
    {
        string someText = "This is a var of:";
        int vari = 15000;
        string endText = "is ready";
        // concatenation
        string concat = someText + " " + vari + " " + endText;
        // interpolation
        string inter = $"{someText} and text {vari} {endText}";
        // stringbuilder
        StringBuilder sb = new StringBuilder();
        sb = sb.Append(someText)
            .Append(" ")
            .Append(vari)
            .Append(" ")
            .Append(endText);
        string res = sb.ToString();
        StringBuilder sb1 = new StringBuilder();
        sb1 = sb1.AppendJoin(" ", someText, vari, endText);
        string re1 = sb1.ToString();
        // join to one string if separator is the same

        string res2 = 
            string.Join(" ", someText, vari, endText);
        string res3 =
            new StringBuilder()
                .AppendJoin(" ", someText, vari, endText)
                .ToString();
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

    static void Analyze(string input,
        out int numAlphabetic, out int numDigits, out int numSpecial)
    {
        numAlphabetic = 0;
        numDigits = 0;
        numSpecial = 0;
        // char[] arr = input.ToCharArray();
        // input = arr.ToString();
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

    static string Sort(string str)
    {
        // string => char[]
        char[] chars = str.ToCharArray();
        // sort array
        Array.Sort(chars, StringComparer.OrdinalIgnoreCase);
        // char[] => string
        str = new string(chars);
        return str;
    }

    static char[] Duplicate(string str)
    {
        // with resize new char[str.Length]
        char[] duplicates = new char[0];
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

                if (!isAlreadyAdded) // not true
                {
                    Array.Resize(ref duplicates, duplicates.Length + 1);
                    duplicates[duplicateIndex++] = c;
                }
            }
            else
            {
                seen[seenIndex++] = c;
            }
        }

        // with resize
        // Array.Resize(ref duplicates, duplicateIndex);

        return duplicates;
    }

    static List<char> DuplicateUsingList(string str)
    {
        List<char> duplicates = new List<char>(); // 0 elements
        List<char> seen = new List<char>(); // 0 elements
        // str = str.TrimStart(); // "   st r  " => "st r  "
        // str = str.TrimEnd();   // "   st r  " => "   st r"
        // str = str.Trim();      // "   st r  " => "st r"
        str = str.Replace(" ", ""); // => "str"
        foreach (char c in str)
        {
            // [q, w, e].Contains(e) = true;
            if (seen.Contains(c) && !duplicates.Contains(c))
            {
                duplicates.Add(c); // size +1
            }
            else // if is not seen or is in the duplicates list
            {
                seen.Add(c);
            }
        }

        char[] dupArray = duplicates.ToArray();
        List<char> dupList = dupArray.ToList();
        return duplicates;
    }
}