using Level3.Extensions;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);
Console.WriteLine("*******STAR2*******");
Star2(lines);

static void Star1(string[] lines)
{
    var totalPoints = 0;
    foreach (var line in lines)
    {
        var points = 0;
        var numbers = line.Split(":")[1].Trim().Split("|");
        var winningNums = numbers[0].ExtractNumbers().ToList();
        var otherNums = numbers[1].ExtractNumbers().ToList();
        foreach (var winningNum in winningNums)
        {
            var count = otherNums.Count(n => n.num == winningNum.num);
            for (int i = 0; i < count; i++)
            {
                if(points == 0)
                {
                    points = 1;
                }
                else
                {
                    points *= 2;
                }
            }
        }
        totalPoints += points;
    }

    Console.WriteLine(totalPoints);
}
static void Star2(string[] lines)
{
    var totalPoints = 0;
    var copyies = new Dictionary<int, int>();
    for (var i = 0; i < lines.Length; i++)
    {
        copyies.TryAdd(i, 0);
        
        var numbers = lines[i].Split(":")[1].Trim().Split("|");
        var winningNums = numbers[0].ExtractNumbers().ToList();
        var otherNums = numbers[1].ExtractNumbers().ToList();
        var countOfMatchingNums = 0;
        foreach (var winningNum in winningNums)
        {
            var count = otherNums.Count(n => n.num == winningNum.num);
            if (count > 0)
            {
                countOfMatchingNums++;
            }
        }
        for (int k = -1; k < copyies[i]; k++)
        {
            if(countOfMatchingNums == 0)
            {
                break;
            }
            for(int j = 1; j<= countOfMatchingNums; j++)
            {
                if(lines.TryElementAt(i+j) != "")
                {
                    if(!copyies.TryAdd(i+j, 1))
                    {
                        copyies[i+j]++;
                    }
                }
            }
            Console.SetCursorPosition(0,3);
            Console.WriteLine($"{i}: {k}/{copyies[i],-10}     {(k+1D)/(copyies[i]+1D)*100:F1}%                   ");
        }
    }

    Console.WriteLine(copyies.Values.Select(v => v+1).Sum());
}