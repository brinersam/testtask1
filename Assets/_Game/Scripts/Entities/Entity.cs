using System;
using System.Collections.Generic;

public class Entity : IEntity
{
    private int _maxHealth;
    private int _maxEnergy;

    private int _curHealth;
    private int _curEnergy;

    private Deck _deck;
    private SOEntity _entityData;

    public EntityVisual _myVisual;
    public List<ICardEffect> currentEffects; // both buffs and debuffs

    public (int current, int max) Health => (_curHealth, _maxHealth);
    public (int current, int max) Energy => (_curEnergy, _maxEnergy);
    public Deck Deck => _deck;
    public SOEntity EntityData => _entityData;
    public bool IsEnemy { get; }

    public Entity(SOEntity data, bool isEnemy = true) 
    {
        _entityData = data;

        _maxHealth = data.StartingStats.MaxHealth;
        _curHealth = _maxHealth;

        _maxEnergy = data.StartingStats.MaxEnergy;
        _curEnergy = _maxEnergy;

        if (data.StartingDeck == null)
            throw new Exception($"Deck was not set for entity: {data.name}");

        _deck = data.StartingDeck;
        IsEnemy = isEnemy;
        currentEffects = data.StartingEffects ?? new();
    }
}

public class AiEntity : Entity
{
    private AiDriver _aiDriver;

    public AiEntity(SOEntity data, AiDriver driver, bool isEnemy = true) : base(data, isEnemy)
    {
        _aiDriver = driver;
    }

    public List<Action<Battle>> QueuedActions => _aiDriver.QueuedActions;
}

public interface IEntity
{
    (int current, int max) Health { get; }
    (int current, int max) Energy { get; }
}