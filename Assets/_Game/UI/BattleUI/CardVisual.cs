using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CardVisual : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _gObjName;
    [SerializeField] private Image _gObjImage;
    [SerializeField] private Text _gObjEnergyCost;
    [SerializeField] private Text _gObjDescription;

    private Card _lastCardData;

    private IBattleModel _battleModel;
    [Inject]
    private void Init(IBattleModel model)
    {
        _battleModel = model;
    }

    public void SetData(Card card)
    {
        _lastCardData = card;
        _lastCardData._myVisual = this;
        UpdateVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _battleModel.HandleClick(eventData, new BattleClickInfo_card(eventData, _lastCardData));
        Debug.Log($"Card {_lastCardData.CardName} was clicked!");
    }

    private void UpdateVisuals()
    {
        _gObjName.text = _lastCardData.CardName;
        _gObjImage.sprite = _lastCardData.CardImage;
        _gObjEnergyCost.text = _lastCardData.CardCost.ToString();
        StringBuilder sb = new StringBuilder();

        foreach(EffectWithTargeter effect in _lastCardData.Effects)
        {
            sb.AppendLine(effect.ToString());
            sb.Append("\n");
        }

        _gObjDescription.text = sb.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector3.one * 1.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector3.one;
    }
}

