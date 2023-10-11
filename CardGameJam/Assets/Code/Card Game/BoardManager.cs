using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard
}

public class BoardManager : MonoBehaviour
{
    public CardObjectPooler cardPooler;
    public CardData[] blackCards;
    public CardData[] blueCards;
    public CardData[] grayCards;

    public DifficultyLevel difficulty;

    private int currentRound = 1;
    private int cardsPerRound = 6; // Starting number of cards per round.

    private List<Card> createdCards = new List<Card>();
    public GridLayoutGroup gridLayoutGroup; // Reference to the GridLayoutGroup.
    public GameObject gridPanel; // Reference to the gameObject that holds the grid group

    void Start()
    {
        difficulty = DifficultyManager.Instance.LoadDifficulty();
        StartNextRound();
    }

    void StartNextRound()
    {
        // Adjust the number of cards based on the current round.
        int cardsToSelect = cardsPerRound;
        GameManager.instance.SetCardsToMatch(cardsToSelect / 2);
        // Adjust the constriant of the grid
        ModifyGridLayoutGroup(currentRound);

        // Randomly select pairs of CardData objects based on the number of cards to select.
        List<CardData> selectedCards = SelectPairsOfCards(cardsToSelect);

        // Clear the list of created cards from the previous round.
        createdCards.Clear();

        // Create card prefabs and set their CardData.
        foreach (CardData cardData in selectedCards)
        {
            Card cardPrefab = cardPooler.GetNextCard();
            cardPrefab.SetCardData(cardData);
            createdCards.Add(cardPrefab);
        }

        // Shuffle the cards in the grid if desired.
        AddRandomCardsToGrid();
    }

    List<CardData> SelectPairsOfCards(int count)
    {
        List<CardData> selectedCards = new List<CardData>();

        // Choose which arrays to use based on difficulty.
        List<CardData> cardPool = new List<CardData>();
        if (difficulty == DifficultyLevel.Easy)
        {
            cardPool.AddRange(blackCards);
        }
        else if (difficulty == DifficultyLevel.Normal)
        {
            cardPool.AddRange(blackCards);
            cardPool.AddRange(blueCards);
        }
        else if (difficulty == DifficultyLevel.Hard)
        {
            cardPool.AddRange(blackCards);
            cardPool.AddRange(blueCards);
            cardPool.AddRange(grayCards);
        }

        // Shuffle the card pool.
        ShuffleList(cardPool);

        // Select unique pairs of cards from the shuffled pool.
        while (selectedCards.Count < count)
        {
            int randomIndex = Random.Range(0, cardPool.Count);
            CardData selectedCard = cardPool[randomIndex];

            if (!selectedCards.Contains(selectedCard))
            {
                selectedCards.Add(selectedCard);
                selectedCards.Add(selectedCard);
            }
        }

        return selectedCards;
    }


    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void ModifyGridLayoutGroup(int round)
    {
        int constraint;

        switch (round)
        {
            case 1: constraint = 3; break;
            case 2: constraint = 4; break;
            case 3: constraint = 6; break;
            case 4: constraint = 8; break;
            case 5: constraint = 10; break;
            default: constraint = 0; break;
        }
        if (gridLayoutGroup != null)
        {
            gridLayoutGroup.constraintCount = constraint;
        }
    }

    void AddRandomCardsToGrid()
    {
        int cardsToAdd = cardsPerRound;

        for (int i = 0; i < cardsToAdd; i++)
        {
            if (createdCards.Count > 0)
            {
                int randomIndex = Random.Range(0, createdCards.Count);
                Card card = createdCards[randomIndex];

                // Set the card's parent to the gridPanel, which holds the grid group.
                card.transform.SetParent(gridPanel.transform);

                // Ensure the card's local scale is (1, 1, 1) to avoid scaling issues.
                card.transform.localScale = Vector3.one;

                createdCards.RemoveAt(randomIndex);
            }
            else
            {
                break; // No more cards to add.
            }
        }
    }


    public void RoundWin()
    {

        if (currentRound == 5)
        {
            DifficultyManager.Instance.UnlockHardDifficulty();
            SceneManagement.Instance.LoadMainMenu();
        }
        else
        {
            currentRound++;
            cardsPerRound += 6;

            Card[] cards = gridPanel.GetComponentsInChildren<Card>();

            // Start the coroutine to return cards to the pool and then start the next round.
            StartCoroutine(ReturnCardsAndStartNextRound(cards));
        }    
    }

    private IEnumerator ReturnCardsAndStartNextRound(Card[] cards)
    {
        // Return all the cards to the pool.
        foreach (Card card in cards)
        {
            cardPooler.ReturnCardToPool(card);
        }

        // Wait for a short delay before starting the next round to allow cards to return.
        yield return new WaitForSeconds(2f); 

        // Start the next round.
        StartNextRound();
    }
}
