using System;
using System.Collections.Generic;

public class Entity : IEntity, IDisposable
{
    private int _maxHealth;
    private int _maxEnergy;

    private int _curHealth;
    private int _curEnergy;

    public EntityVisual _myVisual;
    public List<ICardEffect> currentEffects = new(); // both buffs and debuffs
    public List<ICardIntent> QueuedActions = new();

    public (int current, int max) Health => (_curHealth, _maxHealth);
    public (int current, int max) Energy => (_curEnergy, _maxEnergy);

    public Team InTeam;
    public bool IsDead => _curHealth <= 0;
    public Deck Deck { get; }
    public SOEntity EntityData { get; }

    public Entity(SOEntity data) 
    {
        EntityData = data;

        _maxHealth = data.StartingStats.MaxHealth;
        _curHealth = _maxHealth;

        _maxEnergy = data.StartingStats.MaxEnergy;
        _curEnergy = _maxEnergy;

        if (data.StartingDeck == null)
            throw new Exception($"Deck was not set for entity: {data.name}");

        Deck = data.StartingDeck;
        currentEffects = data.StartingEffects ?? new();
    }

    public void ReceiveDamage(int dmg)
    {
        _curHealth -= dmg;
        //if (IsDead)

    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    internal bool FriendlyTowards(Entity targetEnt)
    {
        throw new NotImplementedException();
    }
}


public interface IEntity
{
    (int current, int max) Health { get; }
    (int current, int max) Energy { get; }
    bool IsDead { get; }
}