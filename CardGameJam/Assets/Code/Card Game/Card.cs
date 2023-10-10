using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite cardFrontImage; // Reference to the front of the card image.
    [SerializeField] private  Sprite cardBackImage; // Reference to the back of the card image.

    public CardData cardData; // The data associated with the card.
    private bool isFlipped = false; // Tracks whether the card is flipped.
    [SerializeField] private Image displayImage;

    public void SetCardData(CardData data)
    {
        cardData = data;
        cardFrontImage = cardData.cardImage;
    }

    public void ResetCard()
    {
        cardData = null;
        cardFrontImage = null;
        displayImage.sprite = cardBackImage;
        isFlipped = false;
    }

    public void OnCardClick()
    {
        if (!isFlipped && !GameManager.instance.MatchingInProgress)
        {
            UncoverCard();
            GameManager.instance.OnCardFlipped(this);
        }
    }

    public void UncoverCard()
    {
        if (!isFlipped) 
        {
            StartCoroutine(FlipAnimation(cardFrontImage));
            isFlipped = true;
        } 
    }


    public void CoverCard()
    {
        if (isFlipped) 
        {
            StartCoroutine(FlipAnimation(cardBackImage));
            isFlipped = false;
        }
    }
    private IEnumerator FlipAnimation(Sprite newImage, float duration = 0.5f)
    {
        float elapsedTime = 0f;

        // Reduce the scale from 1 to 0.
        while (elapsedTime < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Apply the scale to the card's X-axis.
            Vector3 newScale = transform.localScale;
            newScale.x = scale;
            transform.localScale = newScale;

            // Wait for the next frame.
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        // Ensure the final scale is exactly 0.
        Vector3 finalScale = transform.localScale;
        finalScale.x = 0f;
        transform.localScale = finalScale;

        // Reset elapsedTime.
        elapsedTime = 0f;

        // Change the card's image.
        displayImage.sprite = newImage;

        // Scale back from 0 to 1.
        while (elapsedTime < duration)
        {
            float scale = Mathf.Lerp(0f, 1f, elapsedTime / duration);

            // Apply the scale to the card's X-axis.
            Vector3 newScale = transform.localScale;
            newScale.x = scale;
            transform.localScale = newScale;

            // Wait for the next frame.
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        // Ensure the final scale is exactly 1.
        Vector3 resetScale = transform.localScale;
        resetScale.x = 1f;
        transform.localScale = resetScale;
    }
}
