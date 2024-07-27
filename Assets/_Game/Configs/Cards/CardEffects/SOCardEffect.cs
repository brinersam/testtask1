using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect_", menuName = "ScriptableObjects/Cards/CardEffect")]
public class SOCardEffect : ScriptableObject
{
    public void ApplyEffect(Entity callerEnt, Entity targetEnt)
    {
        targetEnt.ReceiveDamage(3);
    }
}

