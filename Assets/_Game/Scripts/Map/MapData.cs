using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapData : IMapNodeModel, IWarmupableSystem
{
    private readonly SOMap _config;
    private Dictionary<int, MapNodeData> _nodeList;

    public MapData(SOMap config)
    {
        _config = config;
        _nodeList = new();
    }
    
    public void WarmUp()
    {
        var assembler = new NodeTreeAssembler(_config);
        _nodeList = assembler.Assemble();
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

