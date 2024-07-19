using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Game.Infrastructure;

public class GameState_Map : INodeProvider, IMapModel, IGameStateEnterable<GameState_Map>,
    IWarmupableSystem, IGameStateRendererUser //, IGameState<GameState_Map, GameState_Map_Params>
{
    private readonly GameStateMachine _GSM;
    private readonly SOMap _config;
    private Dictionary<int, MapNodeData> _nodeTree;
    private IGameStateRenderer _renderer;

    public GameState_Map(GameStateMachine gsm, SOMap config)
    {
        _config = config;
        _nodeTree = new();
        _GSM = gsm;
    }
    
    public void WarmUp()
    {
        _nodeTree = new NodeTreeAssembler(_config).Assemble();
    }

    public void HandleNodeInteraction(MapNodeData node)
    {
        _GSM.EnterState<GameState_Battle, GameState_Battle_Params>
            (new GameState_Battle_Params() { _mapNode = node});
    }

    public void RegisterRenderer(IGameStateRenderer renderer)
    {
        if (_renderer != null)
        {
            throw new Exception("Attempt at registering another renderer!");
        }
        _renderer = renderer;
    }

    public IEnumerator GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator<MapNodeData> IEnumerable<MapNodeData>.GetEnumerator()
    {
        return _nodeTree.Select(x => x.Value).OrderBy(x => x.Id).GetEnumerator();
    }

    //public void Enter(GameState_Map_Params stateConfig)
    //{
    //    Enter();
    //}

    public void Enter()
    {
        _renderer.Render();
    }

    public void Exit()
    {
        _renderer.Hide();
    }
}

//public struct GameState_Map_Params : IGameStateParams<GameState_Map>
//{
//}

internal interface IMapModel: IGameStateRendererUser
{
    void HandleNodeInteraction(MapNodeData node);
}