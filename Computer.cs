namespace LAL;

public class Computer: Player
{
    public Card Play(int count, List<Card> prev)
    {
        bool legal = false;
        foreach (Card c in hand)
        {
            if (count + c.Rank <= 31)
            {
                legal = true;
            }
        }
        if (!legal)
        {
            return null;
        }
        Card maxCard = null;
        foreach (Card c in hand)
        {
            if (count + c.Rank > 31)
            {
                continue;
            }
            if (RunCheck(prev, c))
            {
                return c;
            }

            if (count + c.Rank == 15 || (prev.Count > 0 && c.Rank == prev[^1].Rank))
            {
                return c;
            }

            if (maxCard == null || c.Rank > maxCard.Rank)
            {
                maxCard = c;
            }
        }

        return maxCard;
    }
    public List<Card> Crib()
    {
        List<List<Card>> options = new List<List<Card>>
        {
            new() { hand[0], hand[1], hand[2], hand[3] },
            new() { hand[0], hand[1], hand[2], hand[4] },
            new() { hand[0], hand[1], hand[2], hand[5] },
            new() { hand[0], hand[1], hand[3], hand[4] },
            new() { hand[0], hand[1], hand[3], hand[5] },
            new() { hand[0], hand[1], hand[4], hand[5] },
            new() { hand[0], hand[2], hand[3], hand[4] },
            new() { hand[0], hand[2], hand[3], hand[5] },
            new() { hand[0], hand[2], hand[4], hand[5] },
            new() { hand[0], hand[3], hand[4], hand[5] },
            new() { hand[1], hand[2], hand[3], hand[4] },
            new() { hand[1], hand[2], hand[3], hand[5] },
            new() { hand[1], hand[2], hand[4], hand[5] },
            new() { hand[1], hand[3], hand[4], hand[5] },
            new() { hand[2], hand[3], hand[4], hand[5] }
        };
        int max = -1;
        List<Card> maxCards = new List<Card>();
        foreach (List<Card> c in options)
        {
            int handScore = Scoring.ScoreHand(c);
            if (handScore > max)
            {
                max = handScore;
                maxCards = c;
            } 
        }
        List<Card> cribCards = new List<Card>();
        foreach (Card card in hand)
        {
            if (!maxCards.Contains(card))
            {
                cribCards.Add(card);
            }
        }

        foreach (Card card in cribCards)
        {
            hand.Remove(card);
        }

        return cribCards;
    }

    private bool RunCheck(List<Card> prev, Card curr)
    {
        List<Card> combined = new List<Card>(prev);
        combined.Add(curr);
        List<int> ranks = new List<int>();
        foreach (Card c in combined)
        {
            ranks.Add(c.Rank);
        }

        for (int i = 3; i <= Math.Min(5, ranks.Count); i++)
        {
            List<int> tempList = ranks.Skip(ranks.Count - i).ToList();
            tempList.Sort();
            bool run = true;
            for (int j = 1; j < tempList.Count; j++)
            {
                if (tempList[j] - tempList[j - 1] != 1)
                {
                    run = false;
                }
            }

            if (run)
            {
                return true;
            }
        }

        return false;
    }
}