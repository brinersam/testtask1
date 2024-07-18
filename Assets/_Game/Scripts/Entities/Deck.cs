using System.Collections.Generic;
using UnityEngine;


public class Deck
{
    private Dictionary <Card, int> _cards = new();
    public Deck()
    {

    }

    public Deck(IEnumerable<SOCard> cards)
    {
        foreach (Card card in cards)
        {
            AddCard(card);
        }
    }

    public void AddCard(Card card)
    {
        if (!_cards.ContainsKey(card))
            _cards[card] = 0;
        _cards[card]++;
    }
    public void RemoveCard(Card card)
    {
        if (!_cards.ContainsKey(card))
            return;

        _cards[card]--;

        if (_cards[card] == 0)
            _cards.Remove(card);
    }

    
}