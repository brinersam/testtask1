using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _gObjName;
    [SerializeField] private Image _gObjImage;
    [SerializeField] private Text _gObjEnergyCost;
    [SerializeField] private Text _gObjDescription;

    private Card _lastCardInfo;

    public void SetData(Card card)
    {
        _lastCardInfo = card;
        UpdateVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Card {_lastCardInfo.CardName} was clicked!");
    }

    private void UpdateVisuals()
    {
        _gObjName.text = _lastCardInfo.CardName;
        _gObjImage.sprite = _lastCardInfo.CardImage;
        _gObjEnergyCost.text = _lastCardInfo.CardCost.ToString();
        StringBuilder sb = new StringBuilder();

        foreach(SOCardEffect effect in _lastCardInfo.Effects)
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

