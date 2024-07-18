using System;
public class GameStateManager : IGameStateManager
{
    public void EnterCombat(MapNodeData node)
    {
        node.SetState(MapNodeState.Explored);
    }
}

public interface IGameState
{

}