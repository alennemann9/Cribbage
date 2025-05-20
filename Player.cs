namespace LAL;

public class Player
{
    public List<Card> hand { get; set; } = new List<Card>();
    public int score {get; set;}

    public void NewHand(Deck deck)
    {
        hand.Add(deck.Draw());
        hand.Add(deck.Draw());
        hand.Add(deck.Draw());
        hand.Add(deck.Draw());
        hand.Add(deck.Draw());
        hand.Add(deck.Draw());
    }
}