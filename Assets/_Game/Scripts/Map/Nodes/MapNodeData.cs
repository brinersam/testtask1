using System;
using System.Collections.Generic;

public class MapNodeData
{
    public int Id { get; }
    public HashSet<MapNodeData> ConnectedNodes { get; }
    public SOMapNodeFlavor Flavor { get; }
    public MapNodeState State { get; private set; }
    public event Action OnExplored;

    public MapNodeData(int id, SOMapNodeFlavor flavor, MapNodeState state)
    {
        ConnectedNodes = new();
        Id = id;
        Flavor = flavor;
        State = state;

        OnExplored = UnlockNeighbouringNodes;
    }

    public void AddTwoWayConnectionToNode(MapNodeData node)
    {
        if (ConnectedNodes.Contains(node))
            return;
        this.ConnectedNodes.Add(node);
        node.AddTwoWayConnectionToNode(this);
    }

    public void SetState(MapNodeState state)
    {
        if (State != state && state == MapNodeState.Explored)
        {
            OnExplored();
        }

        State = state;
    }

    private void UnlockNeighbouringNodes()
    {
        foreach(MapNodeData node in ConnectedNodes)
        {
            if (node.State == MapNodeState.Locked)
                node.SetState(MapNodeState.Open);
        }
    }
}

