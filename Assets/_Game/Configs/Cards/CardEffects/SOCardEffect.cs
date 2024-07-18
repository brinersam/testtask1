using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect_", menuName = "ScriptableObjects/Cards/CardEffect")]
public class SOCardEffect : ScriptableObject
{
    public EnumTarget targeter;
    public string TargetPLACEHOLDER; //todo remove
    public string EffectPLACEHOLDER; //todo remove

    //public Action<Entity> GetCardEffectOn(Entity user, Entity target)
    //{

    //}

    public override string ToString()
    {
        return $"Does {EffectPLACEHOLDER} to {TargetPLACEHOLDER}";
    }
}

[Flags]
public enum EnumTarget
{
    ME = 0,
    MYTEAM = 1,
    MYENEMY = 2,
    ALL = 4,
    ONE = 8,
    TWO = 16,
    FOUR = 32
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