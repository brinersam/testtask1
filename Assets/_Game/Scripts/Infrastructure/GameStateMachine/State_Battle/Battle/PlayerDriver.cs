public class PlayerDriver : TeamDriver
{
    public override void Act(Battle context)
    {

    }

    public override void Choose(Battle context)
    {

    }

    public override void ActChoose(Battle context)
    {
        context._playerInteractor.TurnInProgress = true;
    }
}
