using UnityEngine;

public enum CardColorType
{
    Black,
    Blue,
    Gray
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card Data")]
public class CardData : ScriptableObject
{
    public CardColorType colorType;
    public int cardID;
    public Sprite cardImage;
}
