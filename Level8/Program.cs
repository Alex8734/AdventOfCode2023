
using Level8;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);


//lines = File.ReadAllLines("Data/example2.txt");
Console.WriteLine("*******STAR2*******");
Star2(lines);

static void Star1(string[] strings)
{
    var states = strings[0].Select(s => s switch
    {
        'R' => Chose.Right,
        'L' => Chose.Left,
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    }).ToArray();
    var currentState = -1;
    Chose getNext()
    {
        currentState++;
        currentState %= states.Length ;
        return states[currentState];
    }

    var ways = new Dictionary<string, (string, string)>();
    foreach (var line in strings.Skip(2))
    {
        var data = line.Split(" = ");
        var way = data[1].TrimStart('(').TrimEnd(')').Split(", ");
        ways.Add(data[0],(way[0],way[1]));
    }

    var current = "AAA";
    var runs = 0;
    while ("ZZZ" != current)
    {
        var (left, right) = ways[current];
        var next = getNext();
        current = next switch
        {
            Chose.Right => right,
            Chose.Left => left,
            _ => throw new ArgumentOutOfRangeException(nameof(next), next, null)
        };
        runs++;
    }

    Console.WriteLine(runs);
}
static void Star2(string[] strings)
{
    var states = strings[0].Select(s => s switch
    {
        'R' => Chose.Right,
        'L' => Chose.Left,
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
    }).ToArray();
    var currentState = -1;
    Chose getNext()
    {
        currentState++;
        currentState %= states.Length ;
        return states[currentState];
    }

    var ways = new Dictionary<string, (string, string)>();
    foreach (var line in strings.Skip(2))
    {
        var data = line.Split(" = ");
        var way = data[1].TrimStart('(').TrimEnd(')').Split(", ");
        ways.Add(data[0],(way[0],way[1]));
    }
    
    var currents = ways.Keys.Where(k => k.EndsWith("A")).ToList();
    var multiRuns = new List<long>();
    for (var i = 0; i< currents.Count; i++)
    {
        var runs = 0L;
        
        while (!currents[i].EndsWith("Z"))
        {
            var next = getNext();
            var current = currents[i];
            var (left, right) = ways[current];
            currents[i] = next switch
            {
                Chose.Right => right,
                Chose.Left => left,
                _ => throw new ArgumentOutOfRangeException(nameof(next), next, null)

            };
                   
            runs++;
        }
        Console.WriteLine($"{i}: {runs}");
        multiRuns.Add(runs);
    }
    Console.WriteLine(Utils.GetSmallestCommonMultiple(multiRuns.ToArray()));
}

public enum Chose{
    Right = 1,
    Left = 0,
}