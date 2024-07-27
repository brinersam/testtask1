using System;
using System.Collections.Generic;

public class PlayerHand
{
    private IBattleRenderer _renderer;
    private int _targetInHand;

    public Dictionary<Entity,Deck> Pile = new();
    public Dictionary<Entity, Deck> Hand = new();
    public Dictionary<Entity, Deck> Discard = new();

    public PlayerHand(Team playerTeam, int maxInHand, IBattleRenderer renderer)
    { 
        _renderer = renderer;
        _targetInHand = maxInHand;
        foreach (Entity entity in playerTeam.entities)
        {
            Deck battleDeck = new Deck();

            foreach(Card card in entity.Deck) // duplicate the deck to avoid corrupting entity's pile
                battleDeck.AddCard(card);

            Pile.Add(entity, battleDeck);
            Hand.Add(entity, new Deck());
            Discard.Add(entity, new Deck());
        }
    }
    public void RenderHand(Deck deck)
    {
        _renderer.DisplayCardsForPlayableEntity(deck);
    }

    public void DiscardCard(Entity entity, Card card)
    {
        Discard[entity].AddCard(Hand[entity].RemoveCard(card));
        RenderHand(Hand[entity]);
    }

    public Deck GetHandForEntity(Entity entity)
    {
        DiscardHand(entity);

        PileToHand(entity);

        if (Hand[entity].Count < _targetInHand)
        {
            DiscardToPile(entity);
        }

        PileToHand(entity);

        return Hand[entity];
    }

    private void DiscardToPile(Entity entity)
    {
        Pile = Discard;
        Discard = new();
    }

    private void PileToHand(Entity entity)
    {
        while (Hand[entity].Count < _targetInHand && Pile[entity].HasCards)
        {
            Hand[entity].AddCard(Pile[entity].RemoveCard());
        }
    }

    private void DiscardHand(Entity entity)
    {
        while (Hand[entity].HasCards)
        {
            Discard[entity].AddCard(Hand[entity].RemoveCard());
        }
    }
}
