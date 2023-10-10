using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard
}

public class CardManager : MonoBehaviour
{
    public CardObjectPooler cardPooler;
    public CardData[] blackCards;
    public CardData[] blueCards;
    public CardData[] grayCards;

    public DifficultyLevel difficulty = DifficultyLevel.Easy;

    void Start()
    {
        StartNextRound();
    }

    void StartNextRound()
    {
        // Randomly select pairs of CardData objects based on difficulty.
        List<CardData> selectedCards = SelectPairsOfCards();

        // Create card prefabs and set their CardData.
        foreach (CardData cardData in selectedCards)
        {
            Card cardPrefab = cardPooler.GetNextCard();
            cardPrefab.SetCardData(cardData);
        }

        // Shuffle the cards in the grid if desired.
        ShuffleCardsInGrid();
    }

    List<CardData> SelectPairsOfCards()
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

        // Select pairs of cards from the shuffled pool.
        for (int i = 0; i < cardPool.Count; i++)
        {
            if (selectedCards.Count < cardPool.Count * 2)
            {
                selectedCards.Add(cardPool[i]);
                selectedCards.Add(cardPool[i]);
            }
            else
            {
                break;
            }
        }

        return selectedCards;
    }

    void ShuffleCardsInGrid()
    {
        // Implement logic to shuffle the cards within the grid.
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
}
