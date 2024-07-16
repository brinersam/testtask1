using UnityEngine;

[CreateAssetMenu(fileName = "Map_0", menuName = "ScriptableObjects/MapNode/_Map")]
public class SOMap : ScriptableObject
{
    public MapNode[] MapNodes;
    [Tooltip("Gets overriden by local flavors, acts as default")]
    public SOMapNodeFlavor GlobalFlavor;
}