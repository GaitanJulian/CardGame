using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject blockPanel;
    public BoardManager boardManager;
    private List<Card> flippedCards = new List<Card>();
    private bool matchingInProgress = false;

    private int numberOfPairs;
    private int cardsMatched = 0;

    // Declare a custom event for card matches.
    public delegate void CardMatchedEvent();
    public event CardMatchedEvent OnCardMatched;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject persistent across scenes.
        }
        else
        {
            Debug.LogError("Duplicate GameManager instance found and destroyed.");
            Destroy(gameObject);
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
        yield return new WaitForSeconds(0.6f); // Wait for 0.6 seconds to show the flipped cards.

        if (flippedCards.Count == 2)
        {
            Card card1 = flippedCards[0];
            Card card2 = flippedCards[1];

            if (card1.cardData.colorType == card2.cardData.colorType &&
                card1.cardData.cardID == card2.cardData.cardID)
            {
                // Cards match
                // Implement card match logic
                cardsMatched++;
                OnCardMatched?.Invoke();
                AudioManager.Instance.PlayRandomCorrectSound();
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
                AudioManager.Instance.PlayRandomErrorSound();
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