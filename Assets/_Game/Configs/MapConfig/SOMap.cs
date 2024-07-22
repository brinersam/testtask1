using UnityEngine;

[CreateAssetMenu(fileName = "Map_0", menuName = "ScriptableObjects/Configs/MapConfig")]
public class SOMap : ScriptableObject
{
    public MapNodeConfig[] MapNodes;
    [Tooltip("Gets overriden by local flavors, acts as default")]
    public SOMapNodeFlavor GlobalFlavor;
    [Tooltip("Gets overriden by local spawners, acts as default")]
    public SOEntitySpawner[] GlobalSpawner;
}