public abstract class TeamDriver
{
    protected Team _team;
    public TeamDriver()
    {
    }
    abstract public void Act(Battle context);
    abstract public void Choose(Battle context);
    abstract public void ActChoose(Battle context);
}
