using System;
using System.Collections.Generic;

public class TurnQueue
{
    private List<Action<Battle>> _actionQueue;

    private int _stageIndex = 0;
    public TurnQueue()
    {
        _actionQueue = new List<Action<Battle>>();
    }

    //public List<Action<Battle>> GetActionQueueForTeam(Team team)
    //{
    //    return _teamDelayedActions[team];
    //}

    public Action<Battle> ProgressTurn()
    {
        return _actionQueue[++_stageIndex % (_actionQueue.Count -1)];
    }
    public TurnQueue TurnStage_ActChoose(Team team)
    {
        _actionQueue.Add(team.ActChoose);
        return this;
    }

    public TurnQueue TurnStage_Act(Team team)
    {
        _actionQueue.Add(team.Act);
        return this;
    }
    public TurnQueue TurnStage_Choose(Team team)
    {
        _actionQueue.Add(team.Choose);
        return this;
    }

}
