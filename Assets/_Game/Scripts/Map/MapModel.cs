using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class MapModel : INodeProvider, IMapModel, IWarmupableSystem
{
    private readonly SOMap _config;
    private Dictionary<int, MapNodeData> _nodeTree;
    private IMapRenderer _renderer;
    private IGameStateManager _stateMgr;

    public MapModel(SOMap config, IGameStateManager stateManager)
    {
        _config = config;
        _nodeTree = new();
        _stateMgr = stateManager;
    }
    
    public void WarmUp()
    {
        _nodeTree = new NodeTreeAssembler(_config).Assemble();
    }

    public void HandleNodeInteraction(MapNodeData node)
    {
        _stateMgr.EnterCombat(node);

        //node.SetState(MapNodeState.Explored);
        //_renderer.RefreshRoomVisuals();
    }

    public void RegisterRenderer(IMapRenderer renderer)
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
