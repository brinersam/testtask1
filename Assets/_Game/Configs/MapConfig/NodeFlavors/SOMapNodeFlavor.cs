using UnityEngine;

[CreateAssetMenu(fileName = "MapNodeFlavor", menuName = "ScriptableObjects/MapNode/Flavor")]
// Visuals, sounds and aesthetics of the room
public class SOMapNodeFlavor : ScriptableObject
{
    [Header("Map view")]
    public Sprite MapIcon_Locked;
    public Sprite MapIcon_Open;
    public Sprite MapIcon_Explored;

    [Space]

    [Header("Combat view")]
    public Sprite Background;
}