using System.Collections.Immutable;

var lines = File.ReadAllLines("Data/input.txt");
Console.WriteLine("*******STAR1*******");
Star1(lines);

//lines = File.ReadAllLines("Data/example2.txt");
Console.WriteLine("*******STAR2*******");
Star2(lines);


static void Star1(string[] lines)
{
    
    List<(string hand, int numberToMultiply)> hands = lines.Select(l => (l.Split(" ")[0], int.Parse(l.Split(" ")[1]))).ToList();
    SortedList<string, int> sortedHands = new SortedList<string, int>(new HandComparer());
    foreach (var hand in hands)
    {
        if(!sortedHands.ContainsKey(hand.hand))
            sortedHands.Add(hand.hand,hand.numberToMultiply);
        else
        {
            sortedHands[hand.hand] += hand.numberToMultiply;
        }
    }
    var total = 0;
    for (int i = 1; i <= sortedHands.Count; i++)
    {
        total += sortedHands.Values[i-1] * i;
    }

    Console.WriteLine(total);
}
static void Star2(string[] lines)
{
    List<(string hand, int numberToMultiply)> hands = lines.Select(l => (l.Split(" ")[0], int.Parse(l.Split(" ")[1]))).ToList();
    SortedList<string, int> sortedHands = new SortedList<string, int>(new HandJokerComparer());
    foreach (var hand in hands)
    {
        if(!sortedHands.ContainsKey(hand.hand))
            sortedHands.Add(hand.hand,hand.numberToMultiply);
        else
        {
            sortedHands[hand.hand] += hand.numberToMultiply;
        }
    }
    var total = 0;
    for (int i = 1; i <= sortedHands.Count; i++)
    {
        total += sortedHands.Values[i-1] * i;
    }

    Console.WriteLine(total);
}
public class HandComparer : IComparer<string>
{
    public static readonly Dictionary<char, int> Cards =  new()
    {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'J', 11},
        {'T', 10},
        {'9' , 9},
        {'8' , 8},
        {'7',7},
        {'6', 6},
        {'5',5},
        {'4',4},
        {'3',3},
        {'2',2}

    };
    
    public int Compare(string? x, string? y)
    {
        if (x == null || y == null) return 0;
        if (x == y) return 0;
        var xHand = new List<int>();
        var yHand = new List<int>();
        for(int i = 0; i< x.Length; i++)
        {
            xHand.Add(Cards[x[i]]);
            yHand.Add(Cards[y[i]]);
        }

        var xTimes = new Dictionary<int, int>();
        var yTimes = new Dictionary<int, int>();
        foreach (var rating in Cards.Values)
        {
            xTimes.Add(rating,xHand.Count(r => r == rating));
            yTimes.Add(rating,yHand.Count(r => r == rating));
        }
        if(GetType(xTimes) == GetType(yTimes))
        {
            return CheckIfEqual(xHand, yHand);   
        }

        return GetType(xTimes) > GetType(yTimes) ? 1 : -1;
    }
    private int CheckIfEqual(List<int> xHand, List<int> yHand)
    {
        for (int i = 0; i < xHand.Count; i++)
        {
            if (xHand[i] == yHand[i]) continue;
            return xHand[i] > yHand[i] ? 1 : -1;
        }

        return 0;
    }
    private int GetType(Dictionary<int, int> hand )
    {
        
        if(hand.ContainsValue(5)) return 7;
        if (hand.ContainsValue(4)) return 6;
        if (hand.ContainsValue(3) && hand.ContainsValue(2)) return 5;
        if (hand.ContainsValue(3)) return 4;
        if (hand.Values.Count(i => i == 2) == 2) return 3;
        if (hand.ContainsValue(2)) return 2;
        return 1;
    }
} 
public class HandJokerComparer : IComparer<string>
{
    public static readonly Dictionary<char, int> Cards =  new()
    {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'T', 10},
        {'9' , 9},
        {'8' , 8},
        {'7',7},
        {'6', 6},
        {'5',5},
        {'4',4},
        {'3',3},
        {'2',2},
        {'J', 1}

    };
    public int Compare(string? x, string? y)
    {
        if (x == null || y == null) return 0;
        if (x == y) return 0;
        var xHand = new List<int>();
        var yHand = new List<int>();
        for(int i = 0; i< x.Length; i++)
        {
            xHand.Add(Cards[x[i]]);
            yHand.Add(Cards[y[i]]);
        }

        var xTimes = new Dictionary<int, int>();
        var yTimes = new Dictionary<int, int>();
        foreach (var rating in Cards.Values)
        {
            xTimes.Add(rating,xHand.Count(r => r == rating  || r == 1));
            yTimes.Add(rating,yHand.Count(r => r == rating || r == 1));
        }
        if(GetType(xTimes) == GetType(yTimes))
        {
            return CheckIfEqual(xHand, yHand);   
        }

        return GetType(xTimes) > GetType(yTimes) ? 1 : -1;
    }
    private float GetType(Dictionary<int, int> hand)
    {
        int jokers = hand.ContainsKey(Cards['J']) ? hand[Cards['J']] : 0;
        var amounts = hand.Values.Where(v => v != jokers).ToList();
        amounts.Sort();

        if (jokers >= 5 || (amounts.Count > 0 && amounts[^1] + jokers >= 5))
            return 5;
        if (jokers >= 4 || (amounts.Count > 0 && amounts[^1] + jokers >= 4))
            return 4;

        // Try a full house
        if (amounts.Count > 0 && amounts[^1] + jokers >= 3)
        {
            int remJokers = amounts[^1] + jokers - 3;
            if (amounts.Count >= 2 && amounts[^2] + remJokers >= 2 || remJokers >= 2)
                return 3.5F;
            return 3;
        }

        if (amounts.Count > 0 && amounts[^1] + jokers >= 2)
        {
            int remJokers = amounts[^1] + jokers - 2;
            if (amounts.Count >= 2 && amounts[^2] + remJokers >= 2 || remJokers >= 2)
                return 2.5F;
            return 2;
        }

        return 1;
    }
    private int CheckIfEqual(List<int> xHand, List<int> yHand)
    {
        for (int i = 0; i < xHand.Count; i++)
        {
            if (xHand[i] == yHand[i]) continue;
            return xHand[i] > yHand[i] ? 1 : -1;
        }

        return 0;
    }
}