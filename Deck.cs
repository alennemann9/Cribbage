namespace LAL;

public class Deck
{
    public List<Card> FullDeck = new List<Card>();
    private List<String> suits = new List<string>{ "Clubs", "Diamonds", "Hearts", "Spades" };

    public Deck()
    {
        foreach (string s in suits)
        {
            for (int i = 1; i <= 13; i++)
            {
                FullDeck.Add(new Card {Suit = s, Rank = i});
            }
        }
        Random rng = new Random();
        for (int i = FullDeck.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (FullDeck[i], FullDeck[j]) = (FullDeck[j], FullDeck[i]);
        }
    }

    public Card Draw() 
    {
        Card drawn = FullDeck[0];
        FullDeck.RemoveAt(0);
        return drawn;
    }
}