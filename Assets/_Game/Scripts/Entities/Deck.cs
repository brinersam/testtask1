using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Deck : IEnumerable<Card>
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

    public Card GetRandomCard()
    {

        return _cards.ElementAt(new Random().Next(0, _cards.Keys.Count)).Key;
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

    public IEnumerator<Card> GetEnumerator()
    {
        foreach (KeyValuePair<Card, int> kv in _cards)
        {
            for (int i = 0; i < kv.Value; i++)
            {
                yield return kv.Key;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}