public class PlayerHand
{
    public Deck Pile = new();
    public Deck Hand = new();
    public Deck Discard = new();

    public PlayerHand(Deck playerHand) // renderer
    { 
        foreach (Card card in playerHand)
        {
            Pile.AddCard(card);
        }
    }

}
