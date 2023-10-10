using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image cardFrontImage; // Reference to the front of the card image.
    [SerializeField] private  Image cardBackImage; // Reference to the back of the card image.

    private CardData cardData; // The data associated with the card.
    private bool isFlipped = false; // Tracks whether the card is flipped.

    public void SetCardData(CardData data)
    {
        cardData = data;
        cardFrontImage.sprite = cardData.cardImage;
    }

    public void ResetCard()
    {
        cardData = null;
        cardFrontImage.sprite = null;
    }


}
