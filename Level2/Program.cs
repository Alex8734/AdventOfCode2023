using Level2.Extensions;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);
Console.WriteLine("*******STAR2*******");
Star2(lines);


static void Star1(string[] lines)
{
    var games = new List<int>();
    for (var i = 0; i < lines.Length; i++)
    {
        var line = lines[i];
        
        var segments = line.Split(":")[1].Trim().Split("; ");
        var valid = true;
        foreach (var segment in segments)
        {
            var colorCounts = new Dictionary<Color, int>();
            var items = segment.Split(", ");
            foreach (var item in items)
            {
                var data = item.Split(" ");
                (Color color,int count) colorCount = data[1] switch
                {
                    "red" => (Color.Red, int.Parse(data[0])),
                    "green" => (Color.Green, int.Parse(data[0])),
                    "blue" => (Color.Blue, int.Parse(data[0])),
                    _ => throw new Exception("Unknown color")
                };
                if(!colorCounts.TryAdd(colorCount.Item1, colorCount.Item2))
                {
                    colorCounts[colorCount.Item1] += colorCount.Item2;
                }
                
                colorCounts.TryGetValue(Color.Red, out int redCount);
                colorCounts.TryGetValue(Color.Green, out int greenCount);
                colorCounts.TryGetValue(Color.Blue, out int blueCount);

                if (redCount > 12 || greenCount > 13 || blueCount > 14)
                {
                    valid = false;
                }
            }
        }
        if(valid)
        {
            games.Add(i+1);
        }
        
    }
    Console.WriteLine(games.Sum());
}

static void Star2(string[] lines)
{
    var games = new List<(int idx,int power,Dictionary<Color, int> colorCounts)>();
    for (var i = 0; i < lines.Length; i++)
    {
        var line = lines[i];
        var colorCounts = new Dictionary<Color, int>();
        foreach (var color in Enum.GetValues<Color>())
        {
            var colorCount = lines[i].FindWordIndexes(color.ToString().ToLower())
                .Select(idx => int.Parse(line.Substring(idx - 3,2)));
           var colorCount2 = colorCount.Max();     
            colorCounts.Add(color, colorCount2);
        }
        games.Add((i+1,colorCounts.Values.Product(),colorCounts));
            
    }

    Console.WriteLine(games.Select(g => g.power).Sum());

}

enum Color
{
    Red,
    Green,
    Blue
}