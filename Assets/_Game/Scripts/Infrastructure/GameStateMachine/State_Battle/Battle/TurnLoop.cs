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
        _actionQueue = new();
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
    public TurnLoop TurnStage_PlanAndExecute(Team team)
    {
        _actionQueue.Add(team.PlanExecute);
        return this;
    }

    public TurnLoop TurnStage_Execute(Team team)
    {
        _actionQueue.Add(team.Execute);
        return this;
    }
    public TurnLoop TurnStage_Plan(Team team)
    {
        _actionQueue.Add(team.Plan);
        return this;
    }

}
