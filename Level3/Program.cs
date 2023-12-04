using Level3;
using Level3.Extensions;
using Level2.Extensions;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);
Console.WriteLine("*******STAR2*******");
Star2(lines);


static void Star1(string[] lines)
{
    var numbers = new List<int>();
    for (var i = 0; i < lines.Length; i++)
    {
        foreach (var (number,idx) in lines[i].ExtractNumbers())
        {
            var start = idx;
            var end = start + number.ToString().Length-1;
            var charsAround = lines.TryElementAt(i - 1).TrySubstring(start - 1, number.ToString().Length + 2) +
                              lines[i].TryElementAt(start - 1) +
                              lines[i].TryElementAt(end + 1) +
                              lines.TryElementAt(i + 1).TrySubstring(start - 1, number.ToString().Length + 2);
            if (charsAround.Any(c => !char.IsNumber(c) && c != '.'))
            {
                numbers.Add(number);
                Console.WriteLine(number);
            }
        }
    } 
    Console.WriteLine(numbers.Sum());
}

static void Star2(string[] lines)
{
    for (var i = 0; i < lines.Length; i++)
    {
        foreach (var index in lines[i].FindWordIndexes("*"))
        {
            var start = index;
            var end = start + 1;
            var charsAround = lines.TryElementAt(i - 1).TrySubstring(start - 1, 3) + ";" +
                              lines[i].TryElementAt(start - 1) + ";" +
                              lines[i].TryElementAt(end + 1) + ";" +
                              lines.TryElementAt(i + 1).TrySubstring(start - 1, 3);
            var numCount = charsAround.Count(char.IsNumber);
            if (numCount >= 2)
            {
                //look for whole number
                var segments = charsAround.Split(";");
                

            }
        }
    }
}

