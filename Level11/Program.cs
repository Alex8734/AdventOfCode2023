using System.Threading.Channels;
using Level2.Extensions;



var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);

//lines = File.ReadAllLines("Data/example2.txt.txt");
Console.WriteLine("*******STAR2*******");
Star2(lines);

static void Star1(string[] lines)
{
    var comets = new List<(int x, int y)>();
    var emptyColumns = Enumerable.Range(0, lines[0].Length)
        .Where(j => lines.All(line => line.Length > j && line[j] == '.'))
        .ToList();
    var emptyRows = Enumerable.Range(0, lines.Length )
        .Where(i => lines[i].All(c => c == '.'))
        .ToList();
    
    for (var i = 0; i < lines.Length; i++)
    {
        if (lines[i].Contains("#"))
        {
            comets.AddRange( lines[i].FindWordIndexes("#").Select(s => (s,i)));
        }
    }
    
    for (var i = 0; i < comets.Count; i++)
    {
        var com = comets[i];
        foreach (var emptyRow in emptyRows.Where(emptyRow => com.y > emptyRow))
        {
            comets[i] = (comets[i].x, comets[i].y+1 );
        }

        foreach (var emptyColumn in emptyColumns.Where(emptyColumn => com.x > emptyColumn))
        {
            comets[i] = (comets[i].x+1, comets[i].y );
        }
    }
    
    var minDistances = new List<(long distance,int a,int b)>();
    for (var i = 0; i < comets.Count; i++)
    {
        var com = comets[i];
        minDistances.AddRange(comets.Where(c => c != com).Select(c => (GetDistance(com, c), i+1, comets.IndexOf(c)+1)));
    }

    Console.WriteLine(minDistances.Sum(d=>d.distance)/2);
} 

static long GetDistance((int x, int y) a, (int x, int y) b)
{
    var xDistance = b.x - a.x;
    var yDistance = b.y - a.y;
    
    var distance = Math.Abs(xDistance)+Math.Abs(yDistance);
    
    return distance;
} 

static void Star2(string[] lines)
{
    var comets = new List<(int x, int y)>();
    var emptyColumns = Enumerable.Range(0, lines[0].Length)
        .Where(j => lines.All(line => line.Length > j && line[j] == '.'))
        .ToList();
    var emptyRows = Enumerable.Range(0, lines.Length )
        .Where(i => lines[i].All(c => c == '.'))
        .ToList();
    
    for (var i = 0; i < lines.Length; i++)
    {
        if (lines[i].Contains("#"))
        {
            comets.AddRange( lines[i].FindWordIndexes("#").Select(s => (s,i)));
        }
    }
    
    for (var i = 0; i < comets.Count; i++)
    {
        var com = comets[i];
        foreach (var emptyRow in emptyRows.Where(emptyRow => com.y > emptyRow))
        {
            comets[i] = (comets[i].x, comets[i].y+999999 );
        }

        foreach (var emptyColumn in emptyColumns.Where(emptyColumn => com.x > emptyColumn))
        {
            comets[i] = (comets[i].x+999999, comets[i].y );
        }
    }
    
    var minDistances = new List<(long distance,int a,int b)>();
    for (var i = 0; i < comets.Count; i++)
    {
        var com = comets[i];
        minDistances.AddRange(comets.Where(c => c != com).Select(c => (GetDistance(com, c), i+1, comets.IndexOf(c)+1)));
    }

    Console.WriteLine(minDistances.Sum(d=>d.distance)/2);
} 