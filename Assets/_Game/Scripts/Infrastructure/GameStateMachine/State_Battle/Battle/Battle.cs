using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
public class Battle
{
    private Team[] _teamList;
    private TurnLoop _turnMgr;

    private PlayerHand _playerHand;

    public PlayerInteractor _playerInteractor;

    public IBattleRenderer _renderer;

    public List<Action<Battle>> PreTurnEffects;
    public List<Action<Battle>> PostTurnEffects;

    public Battle(Team[] teams, TurnLoop turnStructure, IBattleRenderer renderer)
    {
        _turnMgr = turnStructure;
        _teamList = teams;
        _renderer = renderer;
        _renderer.TeamLeft = teams[0];
        _renderer.TeamRight = teams[1];
        _playerInteractor = new();
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

            if (_playerInteractor.TurnInProgress == true)
                await _playerInteractor.WaitForInputAsync();
        }
    }

    private bool IsOneTeamDead()
    {
        return _teamList.Any(team => team.entities.All(ent => ent.Health.current <= 0));
    }
}

public class PlayerInteractor
{
    public bool TurnInProgress;
    private TaskCompletionSource<bool> _tcs;

    public void Click<T>(PointerEventData eventData, T data) where T: IBattleClickInfo
    {
        if (data is BattleClickInfo_endturn endturn)
            ClickHandle(eventData, endturn);
        if (data is BattleClickInfo_card card)
            ClickHandle(eventData, card);
        if (data is BattleClickInfo_entity entity)
            ClickHandle(eventData, entity);
    }

    private void ClickHandle(PointerEventData eventData, BattleClickInfo_endturn data)
    {
        _tcs.SetResult(true);
    }
    private void ClickHandle(PointerEventData eventData, BattleClickInfo_card data)
    {
    }
    private void ClickHandle(PointerEventData eventData, BattleClickInfo_entity data)
    {
    }

    public async Task WaitForInputAsync()
    {
        _tcs = new TaskCompletionSource<bool>();
        await _tcs.Task;
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