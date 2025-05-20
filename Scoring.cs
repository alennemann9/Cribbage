namespace LAL;

public static class Scoring
{
    private static int Fifteens(List<Card> cards)
    {
        int points = 15;
        List<int> ranks = new List<int>();
        foreach (var card in cards)
        {
            ranks.Add(card.Rank);
        }

        for (int i = 0; i < ranks.Count; i++)
        {
            if (ranks[i] > 10)
            {
                ranks[i] = 10;
            }
        }

        for (int i = 0; i < ranks.Count; i++)
        {
            for (int j = i + 1; j < ranks.Count; j++)
            {
                if (i + j == 15)
                {
                    points += 2;
                }
            }
        }
        return points;
    }

    private static int Runs(List<Card> cards)
    {
        List<int> ranks = new List<int>();
        foreach (var card in cards)
        {
            ranks.Add(card.Rank);
        }

        ranks.Sort();
        int length = 1;
        int mul = 1;
        for (int i = 1; i < ranks.Count; i++)
        {
            if (ranks[i] - ranks[i - 1] == 1)
            {
                length++;
            }
            else if (ranks[i] - ranks[i - 1] == 0)
            {
                mul *= 2;
            }
            else if (length >= 3)
                return length * mul;
            else
            {
                length = 1;
            }
        }
        if (length >= 3)
        {
            return length * mul;
        }

        return 0;
    }

    private static int OfKind(List<Card> cards)
    {
        int points = 0;
        Dictionary<int, int> count = new Dictionary<int, int>();
        foreach (Card card in cards)
        {
            if (!count.TryAdd(card.Rank, 1))
            {
                count[card.Rank]++;
            }
        }

        foreach (var value in count.Values)
        {
            if (value == 2) { points += 2; }

            if (value == 3) { points += 6; }

            if (value == 4) { points += 12; }
        }
        return points;
    }

    public static int ScoreHand(List<Card> hand, Card? turnedCard = null)
    {
        int points = 0;
        List<Card> fullHand = new List<Card>(hand);
        if (turnedCard != null)
        {
            fullHand.Add(turnedCard);
        }
        points += OfKind(fullHand);
        points += Runs(fullHand);
        points += Fifteens(fullHand);

        return points;
    }

    public static void PrintBoard(int p1, int p2)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("You: \u25CF, Computer: \u25CB, Both: \u25C9");
        int bar1 = (int)(p1 / 3.025);
        int bar2 = (int)(p2 / 3.025);
        Console.Write("[");
        for (int i = 0; i <= 40; i++)
        {
            if (bar1 == i && bar2 == i)
            {
                Console.Write('\u25C9');
            }
            else if (bar1 == i)
            {
                Console.Write('\u25CF');
            }
            else if (bar2 == i)
            {
                Console.Write('\u25CB');
            }
            else
            {
                Console.Write('-');
            }
        }
        Console.WriteLine("]");
        Console.WriteLine($"You: {p1} / 121");
        Console.WriteLine($"Computer: {p2} / 121");
    }

    private static int RunCheck(List<Card> prev, Card curr)
    {
        List<Card> combined = new List<Card>(prev);
        combined.Add(curr);
        List<int> ranks = new List<int>();
        foreach (Card c in combined)
        {
            ranks.Add(c.Rank);
        }

        for (int i = Math.Min(5, ranks.Count); i >= 3; i--)
        {
            List<int> tempList = ranks.Skip(ranks.Count - i).ToList();
            tempList.Sort();
            bool run = true;
            for (int j = 1; j < tempList.Count; j++)
            {
                if (tempList[j] - tempList[j - 1] != 1)
                {
                    run = false;
                    break;
                }
            }

            if (run)
            {
                return i;
            }
        }

        return 0;
    }

    public static int PeggingScore(List<Card> prev, Card curr, int count)
    {
        int points = 0;
        if (prev.Count >= 1 && curr.Rank == prev[^1].Rank)
        {
            points += 2;
            if (prev.Count >= 2 && curr.Rank == prev[^2].Rank)
            {
                points += 4;
                if (prev.Count >= 3 && curr.Rank == prev[^3].Rank)
                {
                    points += 6;
                    Console.WriteLine("12 points for 4 of a kind");
                }
                else
                {
                    Console.WriteLine("6 points for 3 of a kind");
                }
            }
            else
            {
                Console.WriteLine("2 points for a pair");
            }
        }

        if (count == 31 || count == 15)
        {
            points += 2;
            Console.WriteLine($"2 points for {count}");
        }
        int run = RunCheck(prev, curr);
        points += run;
        if (run > 0)
        {
            Console.WriteLine($"{run} points for a run of {run}");
        }
        return points;
    }
}