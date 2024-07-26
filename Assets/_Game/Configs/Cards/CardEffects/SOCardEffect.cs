using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect_", menuName = "ScriptableObjects/Cards/CardEffect")]
public class SOCardEffect : ScriptableObject
{
    public EnumTarget targeter;
    public int targetAmount = 1;
    public bool affectAll = false;
    public string EffectPLACEHOLDER; //todo remove

    public bool Predicate(Entity callerEnt, Entity targetEnt)
    {
        if (affectAll)
            return true;

        if (targeter == EnumTarget.ME)
            return callerEnt == targetEnt;

        if (targeter == EnumTarget.ENEMYTEAM)
            return !callerEnt.FriendlyTowards(targetEnt);

        if (targeter == EnumTarget.MYTEAM)
            return callerEnt.FriendlyTowards(targetEnt);

        return false;
    }

    public bool RequiresTargets()
    {
        return !affectAll || targeter == EnumTarget.ME;
    }

    public void ApplyEffect(Entity callerEnt, Entity targetEnt)
    {
        targetEnt.ReceiveDamage(3);
    }

    public override string ToString()
    {
        return $"Does {EffectPLACEHOLDER} to {targeter.ToString()}:{targetAmount}";
    }
}

[Flags]
public enum EnumTarget
{
    MYTEAM = 1,
    ENEMYTEAM = 2,
    ME = 4
}


public interface ICardEffect
{
}

public interface ICardIntent
{
    void Play(Battle context);
}

//public enum TargetType
//{
//    Self, 
//    ChooseFriend,
//    AllFriend,
//    ChooseFoe,
//    AllFoe,
//    Everyone
//}