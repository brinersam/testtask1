public class Entity
{
    //private Dictionary<IEntityState,List<TEntityState>> State; 
    // health and energy and anythign else could be generalized to a Type for super modularity

    private int _maxHealth;
    private int _maxEnergy;

    private int _curHealth;
    private int _curEnergy;

    private Deck _deck;
    private SOEntity _entityData;
    private bool _isEnemy;

    public SOEntity EntityData => _entityData;

    public Entity(SOEntity data, bool isEnemy = true) 
    {
        _entityData = data;
        _maxHealth = data.StartingStats.MaxHealth;
        _maxEnergy = data.StartingStats.MaxEnergy;
        _deck = data.StartingDeck;
        _isEnemy = isEnemy;
    }
}
