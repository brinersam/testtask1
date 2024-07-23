using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class EndTurnBtn : MonoBehaviour, IPointerDownHandler
{
    private IBattleModel _battleModel;

    [Inject]
    private void Init(IBattleModel model)
    {
        _battleModel = model;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _battleModel.HandleClick(eventData, new BattleClickInfo_endturn(eventData));
    }

}
