using Level3.Extensions;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);

//lines = File.ReadAllLines("Data/example2.txt");
Console.WriteLine("*******STAR2*******");
Star2(lines);


static void Star1(string[] lines)
{
    var result = 0L;
    foreach (var line in lines)
    {
        var numbers = line.ExtractNumbers<long>().Select(n => n.num).ToArray();
        var diffs = GetDifferences(numbers);
        diffs.Last().Add(0);
        for (int i = diffs.Count-2; i >= 0; i--)
        {
            diffs[i].Add(diffs[i+1].Last() + diffs[i].Last());
        }
        result += diffs[0].Last() + numbers.Last();
    }

    Console.WriteLine(result);
}

static List<List<long>> GetDifferences(long[] numbers)
{
    List<List<long>> differences = new List<List<long>>();
    List<long> diffs = new List<long>();
    for (int i = 0; i < numbers.Length-1; i++)
    {
        diffs.Add( numbers[i+1] - numbers[i] );
    }
    differences.Add(diffs);    
    if(diffs.All(d => d == 0))
    {
        return differences;
    }
    

    differences.AddRange(GetDifferences(diffs.ToArray()));
    return differences;
}

static void Star2(string[] lines)
{
    var result = 0L;
    foreach (var line in lines)
    {
        var numbers = line.ExtractNumbers<long>().Select(n => n.num).ToArray();
        numbers = numbers.Reverse().ToArray();
        var diffs = GetDifferences(numbers);
        diffs.Last().Add(0);
        for (int i = diffs.Count-2; i >= 0; i--)
        {
            diffs[i].Add(diffs[i+1].Last() + diffs[i].Last());
        }
        result += diffs[0].Last() + numbers.Last();
    }

    Console.WriteLine(result);
}