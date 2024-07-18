using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class MapData : INodeProvider, IMapModel, IWarmupableSystem
{
    private readonly SOMap _config;
    private Dictionary<int, MapNodeData> _nodeTree;
    private IMapRenderer _renderer;

    public MapData(SOMap config)
    {
        _config = config;
        _nodeTree = new();
    }
    
    public void WarmUp()
    {
        _nodeTree = new NodeTreeAssembler(_config).Assemble();
    }

    public void HandleNodeInteraction(MapNodeData node)
    {
        node.SetState(MapNodeState.Explored);
        _renderer.RefreshRoomVisuals();
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

