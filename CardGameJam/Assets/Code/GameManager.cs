using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject blockPanel;
    public BoardManager boardManager;
    private List<Card> flippedCards = new List<Card>();
    private bool matchingInProgress = false;

    private int numberOfPairs;
    private int cardsMatched = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public bool MatchingInProgress { get { return matchingInProgress; } }

    public void OnCardFlipped(Card card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            matchingInProgress = true;
            StartCoroutine(MatchCards());
            blockPanel.SetActive(true);
        }
    }

    private IEnumerator MatchCards()
    {
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds to show the flipped cards.

        if (flippedCards.Count == 2)
        {
            Card card1 = flippedCards[0];
            Card card2 = flippedCards[1];

            if (card1.cardData.colorType == card2.cardData.colorType &&
                card1.cardData.cardID == card2.cardData.cardID)
            {
                cardsMatched++;
                if (cardsMatched == numberOfPairs)
                {
                    ResetCardsMatched();
                    boardManager.RoundWin();
                }
            }
            else
            {
                // Cards do not match.
                // Implement card mismatch logic.
                card1.CoverCard();
                card2.CoverCard();
            }

            flippedCards.Clear();
            matchingInProgress = false;
        }
        blockPanel.SetActive(false);
    }

    public void SetCardsToMatch(int numberOfPairs)
    {
        this.numberOfPairs = numberOfPairs;
    }

    public void ResetCardsMatched()
    {
        cardsMatched = 0;
    }
}