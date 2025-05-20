namespace LAL;

public class Human: Player
{

    public Card Play(int count)
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

        while (true)
        {
            Console.Write("Pick the card you want to play: ");
            Card card = GetCardFromHand();
            if (count + Math.Min(card.Rank, 10) <= 31)
            {
                hand.Remove(card);
                return card;
            }
            Console.WriteLine("That card will have you go above 31");
        }
        return null;
    }
    public List<Card> Crib()
    {
List<Card> crib = new List<Card>();
        Console.WriteLine("Choose which cards you want to add to the crib");
        Console.WriteLine("Card 1: ");
        Card card1 = GetCardFromHand();
        crib.Add(card1);
        hand.Remove(card1);
        Console.WriteLine("Card 2: ");
        Card card2 = GetCardFromHand();
        crib.Add(card2);
        hand.Remove(card2);
        return crib;
    }

    public Card GetCardFromHand()
    {
        while (true)
        {
            int rank = GetValidRank();
            string suit = GetValidSuit();
            foreach (Card c in hand)
            {
                if (c.Rank == rank && c.Suit == suit)
                {
                    return c;
                }
            }
            Console.WriteLine("You don't have that card in your hand");
            
        }
    }
    
    private int GetValidRank()
    {
        while (true)
        {
            Console.Write("Enter your card's number (1 = Ace, 2–10, 11 = Jack, 12 = Queen, 13 = King): ");
            if (int.TryParse(Console.ReadLine(), out int rank) && rank >= 1 && rank <= 13)
            {
                return rank;
            }
            Console.WriteLine();
            Console.WriteLine("That is not a valid number, please enter a number between 1 and 13");
        }
    }
    
    private string GetValidSuit()
    {
        string[] validSuits = { "Hearts", "Clubs", "Diamonds", "Spades" };
        while (true)
        {
            Console.Write("Enter suit (Hearts, Clubs, Diamonds, Spades): ");
            string input = Console.ReadLine();
            string suit = char.ToUpper(input[0]) + input.Substring(1).ToLower();
            if (validSuits.Contains(suit))
            {
                return suit;
            }
            Console.WriteLine("That isn't a valid suit.");
        }
    }
}