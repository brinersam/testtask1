public class Team
{
    private TeamDriver _driver;
    public Entity[] entities { get; }
    public Team(TeamDriver driver, Entity[] entities) // todo entity -> entitySO, driver implements entity factory to make player ents and AI ents
    {
        _driver = driver;
        driver.SetTeam(this);
        this.entities = entities;

        foreach (Entity entity in entities)
            entity.Team = this;
    }

    public Entity this[int idx]
    {
        get { return entities[idx]; }
    }

    public void Execute(Battle context) =>
        _driver.Execute(context);

    public void Plan(Battle context) =>
        _driver.Plan(context);

    public void PlanExecute(Battle context) =>
        _driver.PlanExecute(context);

}
