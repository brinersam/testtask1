using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class MapModel : INodeProvider, IMapModel, IWarmupableSystem
{
    private readonly GameStateMachine _GSM;
    private readonly SOMap _config;
    private Dictionary<int, MapNodeData> _nodeTree;
    private IGameStateRenderer _renderer;

    public MapModel(GameStateMachine gsm, SOMap config)
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
}
