using System.Collections.Generic;

public class MapNodeData
{
    public int Id { get; }
    public HashSet<MapNodeData> ConnectedNodes { get; }
    public SOMapNodeFlavor Flavor { get; }
    public MapNodeState State { get; private set; }
    public SOEntitySpawner[] EntitySpawners { get; }

    public MapNodeData(int id, SOMapNodeFlavor flavor, MapNodeState state, SOEntitySpawner[] spawners)
    {
        ConnectedNodes = new();
        Id = id;
        Flavor = flavor;
        State = state;
        EntitySpawners = spawners;
    }

    public void AddTwoWayConnectionToNode(MapNodeData node)
    {
        if (ConnectedNodes.Contains(node))
            return;
        this.ConnectedNodes.Add(node);
        node.AddTwoWayConnectionToNode(this);
    }
}

