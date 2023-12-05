using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using OpenCL.Net;
using Level3.Extensions;
using Level5;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);


Console.WriteLine("*******STAR2*******");
Star2(lines);
static void Star1(string[] lines)
{
    var seeds = lines[0].ExtractNumbers<long>().Select(n => n.num).ToList();
    var maps = lines.Skip(2).GroupByDelimiter(l => l == "")
        .Select(value => value.Skip(1));
    foreach (var map in maps)
    {
        var numbers = map.Select(n => n.ExtractNumbers<long>().Select(v => v.num).ToImmutableArray()).ToImmutableArray();
        var newValues = new List<long>();
        foreach (var seed in seeds)
        {
            var found = false;
            foreach (var number in numbers)
            {
                var range = new LongRange(number[1], number[1] + number[2]-1);
                if (range.Contains(seed))
                {
                    found = true;
                    newValues.Add( number[0] + seed - range.Start);
                    break;
                }
            }
            if(!found) newValues.Add(seed);
        }
        seeds = newValues;
    }

    Console.WriteLine(seeds.Min());
}
static void Star2(string[] lines)
{
    var seedsRaw = lines[0].ExtractNumbers<long>().Select(n => n.num).ToList();
    var seeds = new List<LongRange>();
    for (var i = 0; i < seedsRaw.Count; i+=2)
    {
        seeds.Add(new LongRange(seedsRaw[i],seedsRaw[i] + seedsRaw[i+1]));
    }
    var maps = lines.Skip(2).GroupByDelimiter(l => l == "")
        .Select(value => value.Skip(1));
    foreach (var map in maps)
    {
        var numbers = map.Select(n => n.ExtractNumbers<long>().Select(v => v.num).ToImmutableArray()).ToImmutableArray();
        var newValues = new List<LongRange>();
        while (seeds.Count > 0)
        {
            var seed = seeds[^1];
            seeds.Remove(seed);
            var found = false;
            foreach (var number in numbers)
            {
                var range = new LongRange(number[1], number[1] + number[2]);
                var overlap = new LongRange(Math.Max(seed.Start, range.Start), Math.Min(seed.End, range.End));
                if(overlap.Start < overlap.End)
                {
                    newValues.Add(new LongRange(overlap.Start - range.Start + number[0], overlap.End - range.Start + number[0]));
                    if (overlap.Start > seed.Start)
                    {
                        seeds.Add(new LongRange(seed.Start, overlap.Start));
                    }
                    if(seed.End > overlap.End)
                    {
                        seeds.Add(new LongRange(overlap.End, seed.End));
                    }

                    found = true;
                    break;
                }
            }
            if(!found) newValues.Add(seed);
        }
        seeds = newValues;
    }

    Console.WriteLine(seeds.Min().Start);
}

public class LongRange : IEnumerable<long> , IComparable<LongRange>
{
    
    public long Start { get; }
    public long End { get; }
    public bool Contains(long value)
    {
        return value >= Start && value <= End;
    }
    public LongRange(long start, long end)
    {
        Start = start;
        End = end;
    }
    
    public IEnumerator<long> GetEnumerator()
    {
        for (long i = Start; i <= End; i++)
        {
            yield return i;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"{Start} - {End}";
    }

    public int CompareTo(LongRange? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var startComparison = Start.CompareTo(other.Start);
        if (startComparison != 0) return startComparison;
        return End.CompareTo(other.End);
    }
}
