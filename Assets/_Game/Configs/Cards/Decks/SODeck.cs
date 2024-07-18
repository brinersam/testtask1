using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck_0", menuName = "ScriptableObjects/Cards/Deck")]
public class SODeck : ScriptableObject
{
    public SOCard[] Cards;

    public static implicit operator Deck(SODeck soDeck)
    {
        return new Deck(soDeck.Cards);
    }
}
