using System;
using System.Collections.Generic;

public class TurnLoop
{
    private List<Action<Battle>> _actionQueue;

    private int _stageIndex = 0;

    public event Action PreTurnEffects;
    public event Action PostTurnEffects;
    public TurnLoop()
    {
        _actionQueue = new ();
    }

    public Action<Battle> ProgressMove() //Action<Battle>
    {
        if (_stageIndex == 0)
            PreTurnEffects?.Invoke();

        else if (_stageIndex == _actionQueue.Count)
            PostTurnEffects?.Invoke();

        _stageIndex %= _actionQueue.Count;
        return _actionQueue[_stageIndex++];
    }
    public TurnLoop TurnStage_ActChoose(Team team)
    {
        _actionQueue.Add(team.ActChoose);
        return this;
    }

    public TurnLoop TurnStage_Act(Team team)
    {
        _actionQueue.Add(team.Act);
        return this;
    }
    public TurnLoop TurnStage_Choose(Team team)
    {
        _actionQueue.Add(team.Choose);
        return this;
    }

}
