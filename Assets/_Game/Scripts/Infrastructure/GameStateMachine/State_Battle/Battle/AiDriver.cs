using System.Linq;

public class AiDriver : TeamDriver
{
    public override void Execute(Battle context)
    {
        foreach (Entity ent in _team.entities)
        {
            foreach (ICardIntent intent in ent.QueuedActions)
                intent.Play(context);
        }
    }

    public override void Plan(Battle context)
    {
        foreach (Entity ent in _team.entities)
        {
            Card card = ent.Deck.GetRandomCard();
            foreach (var effect in card.Effects)
            { } // new Intent(context, effect.Predicate)
                //  ent.QueuedActions.Add(intent);
        }
    }

    public override void PlanExecute(Battle context)
    {
    }
}
