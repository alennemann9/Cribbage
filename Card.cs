namespace LAL;

public class Card
{
    public string Suit { get; set; }
    public int Rank { get; set; }

    private Dictionary<string, char> suitMaker = new Dictionary<string, char>()
    {
        { "Hearts", '\u2665' }
        , { "Diamonds", '\u2666' }
        , { "Spades", '\u2660' }
        , { "Clubs", '\u2663' }
    };

    public override string ToString()
    {
        string printCard = "";
        if (Rank > 1 && Rank <= 10)
        {
            printCard += Rank;
        } 
        else if (Rank == 1)
        {
            printCard += "A";
        }
        else if (Rank == 11)
        {
            printCard += "J";
        }
        else if (Rank == 12)
        {
            printCard += "Q";
        }
        else
        {
            printCard += "K";
        }

        if (!suitMaker.ContainsKey(Suit))
        {
            printCard += "?";
        }
        else
        {
            printCard += suitMaker[Suit];
        }

        return printCard;
    }
}