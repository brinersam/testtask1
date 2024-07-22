using System.Collections.Generic;

public class NodeTreeAssembler
{
    private readonly SOMap _config;

    public NodeTreeAssembler(SOMap mapData)
    {
        _config = mapData;
    }  
    public Dictionary<int, MapNodeData> Assemble()
    {
        Dictionary<int, MapNodeData> result = new();
        Queue<(MapNodeData from, int toId)> nodesTolinkQueue = new();

        foreach (MapNodeConfig nodeData in _config.MapNodes)
        {
            MapNodeData node = new MapNodeData(
                nodeData.Id,
                nodeData.Flavor ?? _config.GlobalFlavor,
                nodeData.State,
                nodeData.Spawner.Length != 0 ? nodeData.Spawner : _config.GlobalSpawner);
            result.Add(node.Id, node);

            foreach (int neighbourID in nodeData.ConnectedNodeIds)
            {
                if (!result.TryGetValue(neighbourID, out MapNodeData neighbourNode))
                {
                    nodesTolinkQueue.Enqueue((node, neighbourID));
                    continue;
                }

                node.AddTwoWayConnectionToNode(neighbourNode);
            }
        }

        AddMissingConnections(nodesTolinkQueue, result);

        return result;
    }
    private void AddMissingConnections(IEnumerable<(MapNodeData from, int toId)> connectionList, Dictionary<int, MapNodeData> result)
    {
        foreach (var value in connectionList)
        {
            value.from.AddTwoWayConnectionToNode(result[value.toId]);
        }
    }
}