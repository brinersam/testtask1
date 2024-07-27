using System;
using System.Collections.Generic;

public class Entity : IEntity
{
    private int _maxHealth;
    private int _maxEnergy;

    private int _curHealth;
    private int _curEnergy;

    public EntityVisual _myVisual;
    public List<SOCardEffect> currentEffects = new(); // both buffs and debuffs
    public List<EffectWithTargeter> QueuedActions = new();

    public (int current, int max) Health => (_curHealth, _maxHealth);
    public (int current, int max) Energy => (_curEnergy, _maxEnergy);

    public Team Team;
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
        _myVisual.UpdateVisuals();
        //if (IsDead)
    }

    public bool FriendlyTowards(Entity targetEnt)
    {
        return Team == targetEnt.Team;
    }
}


public interface IEntity
{
    (int current, int max) Health { get; }
    (int current, int max) Energy { get; }
    bool IsDead { get; }
}