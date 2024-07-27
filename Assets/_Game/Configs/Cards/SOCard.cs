using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Card_0", menuName = "ScriptableObjects/Cards/Card")]
public class SOCard : ScriptableObject
{
    public int CardCost;
    public string CardName;
    public Sprite CardImage;
    public EffectWithTargeter[] Effects;

    public static implicit operator Card(SOCard soCard)
    {
        return new Card(soCard);
    }
}


[Serializable]
public struct EffectWithTargeter
{
    public SOCardEffect Effect;

    public EnumTarget targeter;
    public int targetAmount;
    public bool maxTargets;

    public bool WillTarget(Entity callerEnt, Entity targetEnt)
    {
        if ((targeter & EnumTarget.ME) != 0)
            return callerEnt == targetEnt;

        if ((targeter & EnumTarget.ENEMYTEAM) != 0)
            return !callerEnt.FriendlyTowards(targetEnt);

        if ((targeter & EnumTarget.MYTEAM) != 0)
            return callerEnt.FriendlyTowards(targetEnt);

        return false;
    }

    public bool RequiresTargets()
    {
        return !maxTargets || targeter == EnumTarget.ME;
    }
    public override string ToString()
    {
        return $"Does {Effect} to {targeter.ToString()}:{targetAmount}";
    }

    internal void Play(Battle context)
    {
        throw new NotImplementedException();
    }
}


[Flags]
public enum EnumTarget
{
    MYTEAM = 1,
    ENEMYTEAM = 2,
    ME = 4
}