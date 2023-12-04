using System.Numerics;

namespace Level2.Extensions;

public static class Extensions
{
    public static List<int> FindWordIndexes(this string str, string word)
    {
        List<int> indexes = new List<int>();
        int index = 0;
        while ((index = str.IndexOf(word, index)) != -1)
        {
            indexes.Add(index);
            index += word.Length;
        }
        return indexes;
    }
    public static T Product<T>(this IEnumerable<T> list) where T : INumber<T>
    {
        return list.Aggregate(T.One, (current, item) => current * item);
    }
}