using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitySpawner_", menuName = "ScriptableObjects/Entity/Spawner")]
public class SOEntitySpawner : ScriptableObject
{
    public SOEntity[] PossibleEntities;
    public int AmountWanted;

    public IEnumerable<Entity> Spawn(bool isEnemy = true)
    {
        for (int i = 0; i < AmountWanted; i++)
        {
            yield return new Entity(PossibleEntities[Random.Range(0, PossibleEntities.Length)]);
        }
    }
}
