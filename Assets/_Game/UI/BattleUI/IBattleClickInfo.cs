using System;
using UnityEngine.EventSystems;

public interface IBattleClickInfo
{
    PointerEventData EventData { get; }
}

public class BattleClickInfo_entity : IBattleClickInfo
{
    public PointerEventData EventData { get; }

    public Entity Entity;

    public BattleClickInfo_entity(PointerEventData eventData, Entity ent)
    {
        EventData = eventData;
        Entity = ent;
    }

}

public class BattleClickInfo_card : IBattleClickInfo
{
    public PointerEventData EventData { get; }
    public Card Card;
    public BattleClickInfo_card(PointerEventData eventData, Card card)
    {
        EventData = eventData;
        Card = card;
    }
}

public class BattleClickInfo_endturn : IBattleClickInfo
{
    public PointerEventData EventData => _eventData;
    PointerEventData _eventData;
    public BattleClickInfo_endturn(PointerEventData eventData)
    {
        _eventData = eventData;
    }
}