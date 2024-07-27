public class PlayerDriver : TeamDriver
{
    public override void Execute(Battle context)
    {

    }

    public override void Plan(Battle context)
    {

    }

    public override void PlanExecute(Battle context)
    {
        context.StartPlayerTurn();
    }
}
