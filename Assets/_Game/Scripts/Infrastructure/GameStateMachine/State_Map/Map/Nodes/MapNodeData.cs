using System.Collections.Generic;

public class MapNodeData
{
    public int Id { get; }
    public HashSet<MapNodeData> ConnectedNodes { get; }
    public SOMapNodeFlavor Flavor { get; }
    public MapNodeState State { get; private set; }

    public MapNodeData(int id, SOMapNodeFlavor flavor, MapNodeState state)
    {
        ConnectedNodes = new();
        Id = id;
        Flavor = flavor;
        State = state;
    }

    public void AddTwoWayConnectionToNode(MapNodeData node)
    {
        if (ConnectedNodes.Contains(node))
            return;
        this.ConnectedNodes.Add(node);
        node.AddTwoWayConnectionToNode(this);
    }
}

