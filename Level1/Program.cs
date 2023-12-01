var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);
Console.WriteLine("*******STAR2*******");
Star2(lines);

static void Star1(string[] lines)
{
    var numbers = new List<int>();
    foreach(var line in lines)
    {
        var nums = line.Where(char.IsNumber).Select(c => int.Parse(c.ToString())).ToArray();
        numbers.Add(int.Parse($"{nums[0]}{nums[^1]}"));
    }
    Console.WriteLine(numbers.Sum());
}
static void Star2(string[] lines)
{
    var str2Numb = new Dictionary<string, int>()
    {
        {"zero", 0}, 
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };
    var newLines = new string[lines.Length];
    Array.Copy(lines, newLines, lines.Length);
    var numbers = new List<int>();
    for (var i = 0; i < lines.Length; i++)
    {
        var nums = new List<int>();
        for(int j = 0; j< lines[i].Length; j++)
        {
            if(char.IsNumber(lines[i][j]))
            {
                nums.Add(int.Parse(lines[i][j].ToString()));
                continue;
            }
            
            foreach (var n in str2Numb.Keys)
            {
                if(j + n.Length > lines[i].Length) continue;
                var sub = lines[i].Substring(j, n.Length);
                if (sub == n)
                {
                    nums.Add(str2Numb[n]);
                }
            }
        }
        numbers.Add(int.Parse($"{nums[0]}{nums[^1]}"));
    }
    Console.WriteLine(numbers.Sum());
}


