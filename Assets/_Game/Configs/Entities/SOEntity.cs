using System.Collections.Generic;
using UnityEngine;

// Bakes into Entity which is later used at runtime anywhere
[CreateAssetMenu(fileName = "Entity_0", menuName = "ScriptableObjects/Entity/Entity")]
public class SOEntity : ScriptableObject
{
    public SOEntityFlavor Flavor;
    public EntityStats StartingStats;
    public SODeck StartingDeck;
    public List<SOCardEffect> StartingEffects;
}

