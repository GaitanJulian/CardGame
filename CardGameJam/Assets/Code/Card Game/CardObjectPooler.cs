using System.Collections.Generic;
using UnityEngine;

public class CardObjectPooler : MonoBehaviour
{
    public GameObject cardPrefab; // Reference to the card prefab to pool.
    public int maxPoolSize = 30; // Maximum number of cards to pool.

    private Queue<Card> cardPool = new Queue<Card>();

    void Awake()
    {
        // Pre-fill the pool with card instances.
        FillPool(maxPoolSize);
    }

    public Card GetNextCard()
    {
        if (cardPool.Count == 0)
        {
            CreateNewCard();
        }

        Card card = cardPool.Dequeue();
        card.gameObject.SetActive(true);

        return card;
    }

    public void ReturnCardToPool(Card card)
    {
        card.ResetCard(); // Reset the card's state for the next round.
        card.transform.SetParent(transform);
        card.gameObject.SetActive(false);

        cardPool.Enqueue(card);
    }

    private void FillPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, transform);
            Card card = cardObj.GetComponent<Card>();
            card.gameObject.SetActive(false);

            cardPool.Enqueue(card);
        }
    }

    private void CreateNewCard()
    {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        Card card = cardObj.GetComponent<Card>();
        card.gameObject.SetActive(false);

        cardPool.Enqueue(card);
    }
}
