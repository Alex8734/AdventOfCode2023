namespace Level5;

public static class Extensions
{
    public static int GetIndexOf(this IEnumerable<string> src, string str)
    {
        var index = 0;
        foreach (var s in src)
        {
            if (s == str)
            {
                return index;
            }
            index++;
        }
        return -1;   
    }
    public static IEnumerable<IEnumerable<T>> GroupByDelimiter<T>(this IEnumerable<T> src, Func<T,bool> delimiter)
    {
        var lines = new List<List<T>>();
        var temp = new List<T>();
        foreach (var s in src)
        {
            if (delimiter(s))
            {
                lines.Add(new List<T>(temp));
                temp.Clear();
            }
            else
            {
                temp.Add(s);
            }
        }
        lines.Add(new List<T>(temp));
        return lines;
    }
}