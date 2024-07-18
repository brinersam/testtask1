using UnityEngine;

[CreateAssetMenu(fileName = "Card_0", menuName = "ScriptableObjects/Cards/Card")]
public class SOCard : ScriptableObject
{
    public int CardCost;
    public string CardName;
    public Sprite CardImage;
    public SOCardEffect[] Effects;

    public static implicit operator Card(SOCard soCard)
    {
        return new Card(soCard);
    }
}
