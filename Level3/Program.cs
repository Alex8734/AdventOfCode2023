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
            
            var charsAround = lines.TryElementAt(i - 1).TrySubstring(start - 1, end - start + 3) + 
                              lines[i].TryElementAt(start - 1) + 
                              lines[i].TryElementAt(end + 1) + 
                              lines.TryElementAt(i + 1).TrySubstring(start - 1, end - start + 3);
            if(charsAround.Count(n =>  n != '.') > 0)
            {
                numbers.Add(number);
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
                              lines[i].TryElementAt(start - 1) + "*" +
                              lines[i].TryElementAt(end + 1) + ";" +
                              lines.TryElementAt(i + 1).TrySubstring(start - 1, 3);
            var numCount = charsAround.Count(char.IsNumber);
            if (numCount >= 2)
            {
                //look for whole number
                var segments = charsAround.Split(";");
                var digits = segments.Select(s => s.ExtractNumbers()).ToArray();
                var numbers = new List<string>();
                for (var j = 0; j < digits.Length; j++)
                {
                    var digitLine = digits[j];
                    foreach (var digit in digitLine)
                    {
                        var orientation = digit.idx - 1;
                        numbers.Add(lines[i+j-1]
                            .TrySubstring(index  + ((2 + digit.num.ToString().Length) * orientation), 3));
                    }
                }
            }
        }
    }
}

