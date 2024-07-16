using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;

public class MapNodeData
{
    public int Id { get; }
    public List<MapNodeData> ConnectedNodes;
    public SOMapNodeFlavor Flavor { get; }
    public MapNodeState State { get; private set; }
    public Action OnExplored;

    public MapNodeData(int id, SOMapNodeFlavor flavor, MapNodeState state, Action onExplored = null)
    {
        Id = id;
        Flavor = flavor;
        State = state;

        if (onExplored == null)
            onExplored = UnlockNeighbouringNodes;

        OnExplored = onExplored;
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

