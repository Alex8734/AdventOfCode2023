using System.Numerics;
using System.Text.RegularExpressions;

namespace Level3.Extensions;

public static class Extensions
{
    public static IEnumerable<(int num,int idx)> ExtractNumbers(this string str)
    {
        var regex = new Regex(@"\d+");
        var matches = regex.Matches(str);
        return matches.Select(s => (int.Parse(s.Value),s.Index));
    }
    public static string TryElementAt(this IEnumerable<string> list, int index)
    {
        try
        {
            return list.ElementAt(index);
        }
        catch (Exception)
        {
            return "";
        }
    }
    public static string TrySubstring(this string str, int start, int length)
    {
        if(start < 0) start = 0;
        if(start+length > str.Length) length = str.Length - start;
        try
        {
            return str.Substring(start, length);
        }
        catch (Exception)
        {
            return "";
        }
    }
    public static char TryElementAt(this string str, int index)
    {
        try
        {
            return str.ElementAt(index);
        }
        catch (Exception)
        {
            return '.';
        }
    }
}