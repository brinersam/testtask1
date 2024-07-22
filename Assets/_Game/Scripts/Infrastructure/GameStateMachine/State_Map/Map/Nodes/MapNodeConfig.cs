using System;
using System.Collections.Generic;

[Serializable]
public struct MapNodeConfig
{
    public int Id;
    public List<int> ConnectedNodeIds;
    public SOMapNodeFlavor Flavor;
    public MapNodeState State;
    public SOEntitySpawner[] Spawner;
}

public enum MapNodeState
{
    Locked,
    Open,
    Explored
}