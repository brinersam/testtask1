public class Team
{
    TeamDriver driver;
    public Entity[] entities { get; }
    public Team(TeamDriver type, Entity[] entities)
    {
        driver = type;
        this.entities = entities;
    }

    public void Act(Battle context)
    {
        driver.Act(context);
    }

    public void Choose(Battle context)
    {
        driver.Choose(context);
    }

    public void ActChoose(Battle context)
    {
        driver.ActChoose(context);
    }

}
