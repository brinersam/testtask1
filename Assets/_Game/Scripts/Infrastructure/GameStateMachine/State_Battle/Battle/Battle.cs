using Game.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
public class Battle
{
    private TurnLoop _turnMgr;

    private PlayerHand _playerHand;

    public Team[] Teams;
    public PlayerInteractor _playerInteractor;

    public IBattleRenderer _renderer;

    public List<Action<Battle>> PreTurnEffects;
    public List<Action<Battle>> PostTurnEffects;

    public Battle(Team[] teams, TurnLoop turnStructure, PlayerData playerData, IBattleRenderer renderer)
    {
        _turnMgr = turnStructure;
        Teams = teams;
        _renderer = renderer;
        _renderer.TeamLeft = teams[0];
        _renderer.TeamRight = teams[1];
        _playerInteractor = new PlayerInteractor(this , playerData);
    }

    public void StartBattle()
    {
        _turnMgr.PreTurnEffects += ApplyPreTurnEffects;
        _turnMgr.PostTurnEffects += ApplyPostTurnEffects;
        PlayNextMove();
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
        return Teams.Any(team => team.entities.All(ent => ent.IsDead));
    }

    internal void ProcessEffect(Entity callerEntity, SOCardEffect effect, IList<Entity> targets = null)
    {
        foreach
            (Entity ent in Teams
                .SelectMany(team => team.entities
                    .Where(entity => effect.Predicate(callerEntity,entity))
                )
             )
        {
            effect.ApplyEffect(callerEntity, ent);
        }
    }
}

public interface IBattleRenderer
{
    Team TeamLeft { get; set; }
    Team TeamRight { get; set; }
}

public struct BattleReport
{
}