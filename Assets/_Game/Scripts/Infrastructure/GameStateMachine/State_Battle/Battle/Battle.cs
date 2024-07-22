using System;
using System.Collections.Generic;

public class Battle
{
    private Team[] _teamList;
    private TurnQueue _turnMgr;
    public event Action<Battle> OnClick;

    private PlayerHand _playerHand;

    // player stuff
    // playerhand
    // discarddeck
    // deck

    // mixin cards

    // pre turn events
    // post turn events

    public Battle(Team[] teams, TurnQueue turnStructure, IBattleRenderer _renderer)
    {
        _turnMgr = turnStructure;
        _teamList = teams;
        _renderer.TeamLeft = teams[0];
        _renderer.TeamRight = teams[1];
    }

    public void HandleClick()
    {
        OnClick?.Invoke(this);
    }

    public object GetBattleReport()
    {
        return null;
    }

    public void PlayATurn()
    {
        Action<Battle> turn = _turnMgr.ProgressTurn();
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