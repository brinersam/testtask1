using UnityEngine.EventSystems;

public interface IBattleClickInfo
{
    PointerEventData EventData { get; }
}

public class BattleClickInfo_entity : IBattleClickInfo
{
    public PointerEventData EventData => _eventData;
    PointerEventData _eventData;

    Entity _ent;

    public BattleClickInfo_entity(PointerEventData eventData, Entity ent)
    {
        _eventData = eventData;
        _ent = ent;
    }

    
}

public class BattleClickInfo_card : IBattleClickInfo
{
    public PointerEventData EventData => _eventData;
    PointerEventData _eventData;
    Card _card;
    public BattleClickInfo_card(PointerEventData eventData, Card card)
    {
        _eventData = eventData;
        _card = card;
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