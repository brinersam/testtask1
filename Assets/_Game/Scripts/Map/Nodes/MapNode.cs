using System;
using System.Collections.Generic;

[Serializable]
public struct MapNode
{
    public int Id;
    public List<int> ConnectedNodeIds;
    public SOMapNodeFlavor Flavor;
    public MapNodeState State;
}

public enum MapNodeState
{
    Locked,
    Open,
    Explored
}