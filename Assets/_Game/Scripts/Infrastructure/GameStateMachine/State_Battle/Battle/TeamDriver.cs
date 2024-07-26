using System;

public abstract class TeamDriver
{
    protected Team _team;
    public TeamDriver()
    {}
    public void SetTeam(Team team)
    {
        _team = team;
    }
    abstract public void Execute(Battle context);
    abstract public void Plan(Battle context);
    abstract public void PlanExecute(Battle context);

    
}
