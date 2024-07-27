using Game.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
public class Battle
{
    private TurnLoop _turnMgr;
    private Team[] _teams;
    private PlayerInteractor _playerInteractor;
    private IBattleRenderer _renderer;

    public List<Action<Battle>> PreTurnEffects;
    public List<Action<Battle>> PostTurnEffects;

    public Battle(Team[] teams, TurnLoop turnStructure, PlayerData playerData, IBattleRenderer renderer)
    {
        _turnMgr = turnStructure;
        _teams = teams;
        _renderer = renderer;

        _renderer.TeamLeft = _teams[0];
        _renderer.TeamRight = _teams[1];

        _playerInteractor = new PlayerInteractor(
                                        this,
                                        teams[0],
                                        new PlayerHand(teams[0], 2, _renderer));
    }

    public void StartBattle()
    {
        _turnMgr.PreTurnEffects += ApplyPreTurnEffects;
        _turnMgr.PostTurnEffects += ApplyPostTurnEffects;
        PlayNextMove();
    }

    public void StartPlayerTurn()
    {
        _playerInteractor.BeginTurn();
    }

    public void ApplyPreTurnEffects()
    {

    }
    public void ApplyPostTurnEffects()
    {

    }

    public void HandleClick(PointerEventData eventData, IBattleClickInfo clickData)
    {
        _playerInteractor.Click(eventData, clickData);
    }

    public object GetBattleReport()
    {
        return null;
    }

    public async void PlayNextMove()
    {
        while (IsOneTeamDead() == false)
        {
            Action<Battle> step = _turnMgr.ProgressMove();
            step.Invoke(this);

            if (_playerInteractor.TurnRequested)
                await _playerInteractor.WaitForInputAsync();
        }
    }

    private bool IsOneTeamDead()
    {
        return _teams.Any(team => team.entities.All(ent => ent.IsDead));
    }

    internal void ProcessEffect(Entity callerEntity, EffectWithTargeter effectWTarget, IEnumerable<Entity> targets = null)
    {
        if (targets != null)
        {
            foreach (Entity ent in targets)
                effectWTarget.Effect.ApplyEffect(callerEntity, ent);
            return;
        }

        foreach(Entity ent in _teams
                    .SelectMany(team => team.entities
                        .Where(entity => effectWTarget.WillTarget(callerEntity,entity))
                    )
               )
        {
            effectWTarget.Effect.ApplyEffect(callerEntity, ent);
        }
    }
}

public interface IBattleRenderer
{
    void DisplayCardsForPlayableEntity(Deck deck);
    Team TeamLeft { get; set; }
    Team TeamRight { get; set; }
}

public struct BattleReport
{
}