
using Level2.Extensions;
using Level3.Extensions;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);


Console.WriteLine("*******STAR2*******");
Star2(lines);

static void Star1(string[] lines)
{
    var timeValues = lines[0].ExtractNumbers().ToArray();
    var distanceValues = lines[1].ExtractNumbers().ToArray();
    var races = new List<(int time, int distance)>();
    for(int i = 0; i<timeValues.Count(); i++)
    {
        races.Add((timeValues[i].num, distanceValues[i].num));
    }

    var bestTimes = new Dictionary<int,List<(int speed,int distance)>>();
    for (var i = 0; i < races.Count; i++)
    {
        var race = races[i];
        bestTimes.Add(i,new List<(int speed, int distance)>());
        for (int j = 0; j <= race.time; j++)
        {
            var speed = j;
            var remainingTime = race.time - j;
            var distance = speed * remainingTime;
            if (distance > race.distance)
            {
                bestTimes[i].Add((speed,distance));
            }
        }
    }

    Console.WriteLine(bestTimes.Values.Select(s => s.Count).Product());
}
static void Star2(string[] lines)
{
    var timeValues = lines[0].Replace(" ","").ExtractNumbers<long>().ToArray();
    var distanceValues = lines[1].Replace(" ","").ExtractNumbers<long>().ToArray();
    (long time, long distance) race = (timeValues[0].num,distanceValues[0].num);
    var bestTimes = new List<(long speed, long distance)>();
    for (long j = 0; j <= race.time; j++)
    {
        var speed = j;
        var remainingTime = race.time - j;
        var distance = speed * remainingTime;
        if (distance > race.distance)
        {
            bestTimes.Add((speed,distance));
        }
    }

    Console.WriteLine(bestTimes.Count);
}
