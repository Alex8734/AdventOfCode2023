
using Level2.Extensions;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);

lines = File.ReadAllLines("Data/example2.txt");
Console.WriteLine("*******STAR2*******");
Star2(lines);


static void Star1(string[] lines)
{
    (int x,int y) currentIdx = (0,0);
    for (var i = 0; i < lines.Length; i++)
    {
        if (lines[i].Contains("S"))
        {
            currentIdx = (lines[i].IndexOf("S"),i);
            break;
        }
    }
    char CurrentPeace() => lines[currentIdx.y][currentIdx.x];
    var nextDirection  = Direction.East;
    foreach (var direction in Enum.GetValues<Direction>())
    {
        var idx = IdxOf(currentIdx,direction);
        if(idx.x < 0 || idx.y < 0 || idx.y >= lines.Length || idx.x >= lines[0].Length) continue;
        if(IsAttaching(lines[idx.y][idx.x], direction))
        {
            nextDirection = direction;
            break;
        }
    }

    currentIdx = IdxOf(currentIdx, nextDirection);
    int steps = 0;
    while(CurrentPeace() != 'S')
    {
        steps++;
        nextDirection = GetNextDirection(nextDirection, CurrentPeace());
        currentIdx = IdxOf(currentIdx, nextDirection);
    }

    Console.WriteLine(steps/2+1);
}
static void Star2(string[] lines)
{
    (int x,int y) currentIdx = (0,0);
    List<(int x, int y)> startDots = new List<(int x, int y)>();
    for (var i = 0; i < lines.Length; i++)
    {
        if (lines[i].Contains("."))
        {
            startDots.AddRange( lines[i].FindWordIndexes(".").Select(s => (s,i)));
        }
    }
    
    char CurrentPeace() => lines[currentIdx.y][currentIdx.x];
    var nextDirection  = Direction.East;

    for (var i = 0; i < startDots.Count; i++)
    {
        var dot = startDots[i];
        foreach (var direction in Enum.GetValues<Direction>())
        {
            var idx = IdxOf(dot, direction);
            if (idx.x < 0 || idx.y < 0 || idx.y >= lines.Length || idx.x >= lines[0].Length)
            {
                startDots.RemoveAt(i);
                break;
            }
            if(startDots.Contains(idx))
            {
                break;
            }
            if (IsAttaching(lines[idx.y][idx.x], direction))
            {
                startDots[i] = idx;
                break;
            }
        }
    }

    Console.WriteLine(startDots.Count);
    currentIdx = IdxOf(currentIdx, nextDirection);
    int steps = 0;
    while(CurrentPeace() != 'S')
    {
        steps++;
        nextDirection = GetNextDirection(nextDirection, CurrentPeace());
        currentIdx = IdxOf(currentIdx, nextDirection);
    }
}


static bool IsAttaching(char peace, Direction dir)
{
    if (peace == 'S') return true;
    if (!"LJ|-7F".Contains(peace)) return false;
    var dirs = GetDirections(peace);
    return dirs.Contains(Opposite(dir));
}
static Direction[] GetDirections(char peace)
{
    return peace switch
    {
        'L' => new[] {Direction.North, Direction.East},
        'J' => new[] {Direction.North, Direction.West},
        '|' => new[] {Direction.North, Direction.South},
        '-' => new[] {Direction.West, Direction.East},
        'F' => new [] {Direction.South, Direction.East},
        '7' => new [] {Direction.South, Direction.West},
        _ => throw new Exception("Invalid peace")
    };
}
static Direction GetNextDirection(Direction prev, char currentPeace)
{
    var dirs = GetDirections(currentPeace); 
    return dirs.First(d => d.ToString() != Opposite(prev).ToString());
}
static (int x,int y) IdxOf((int x,int y)cur, Direction dir)
{
    return dir switch
    {
        Direction.North => (cur.x ,cur.y-1),
        Direction.East => (cur.x+1,cur.y+0),
        Direction.South => (cur.x+0,cur.y+1),
        Direction.West => (cur.x-1,cur.y+0),
        _ => throw new Exception("Invalid direction")
    };
}

static Direction Opposite(Direction dir) => (Direction) (((int) dir + 2) % 4);
public enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}