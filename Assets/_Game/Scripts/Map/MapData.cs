using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapData : IMapNodeProvider, IWarmupableSystem
{
    private readonly SOMap _config;
    private readonly Dictionary<int, MapNodeData> _nodeList;

    public MapData(SOMap config)
    {
        _config = config;
        _nodeList = new();
    }
    
    public void WarmUp()
    {
        CreateNodes();
    }

    private void CreateNodes()
    {
        foreach(MapNode nodeData in _config.MapNodes.OrderBy(x => x.Id))
        {
            MapNodeData node = new MapNodeData(
                nodeData.Id,
                nodeData.Flavor ?? _config.GlobalFlavor,
                nodeData.State);
            _nodeList.Add(node.Id, node);

            // link them here too TODO
        }
    }

    public IEnumerator GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator<MapNodeData> IEnumerable<MapNodeData>.GetEnumerator()
    {
        return _nodeList.Select(x => x.Value).OrderBy(x => x.Id).GetEnumerator();
    }
}

