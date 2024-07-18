using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    [SerializeField] private SOCard DEBUGCARDDATA; //todo delete

    [SerializeField] private Text _gObjName;
    [SerializeField] private Image _gObjImage;
    [SerializeField] private Text _gObjEnergyCost;
    [SerializeField] private Text _gObjDescription;

    private Card _lastCardInfo;

    private void Start()
    {
        _lastCardInfo = DEBUGCARDDATA;
        UpdateVisuals();
    }

    public void SetData(Card card)
    {
        _lastCardInfo = card;
        UpdateVisuals();
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
}

