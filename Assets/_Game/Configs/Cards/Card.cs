using UnityEngine;

public class Card
{ 
    private SOCard _cardData;
    public CardVisual _myVisual;

    public Card(SOCard cardData)
    {
        _cardData = cardData;
    }

    public int CardCost => _cardData.CardCost;
    public string CardName => _cardData.CardName;
    public Sprite CardImage => _cardData.CardImage;
    public SOCardEffect[] Effects => _cardData.Effects;

}
