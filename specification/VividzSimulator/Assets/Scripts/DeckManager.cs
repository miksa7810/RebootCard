// DeckManager.cs
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public TextAsset deckCsv;
    public GameObject cardPrefab;
    public Transform playerHandZone;
    public TMP_Text deckCountLabel;
    public Sprite cardBackSprite;
    public GameObject pDeck;
    public GameObject cDeck;
    public int startingHandSize = 5;

    public List<CardInfo> deckCards = new List<CardInfo>();
    public List<CardInfo> playerHand = new List<CardInfo>();
    private List<GameObject> handCards = new List<GameObject>();

    void Start()
    {
        LoadDeckFromCsv();
        ShuffleDeck();
        PlaceDeck();
        DrawCards(startingHandSize);
    }

    void LoadDeckFromCsv()
    {
        deckCards.Clear();
        using (StringReader reader = new StringReader(deckCsv.text))
        {
            string line;
            bool isFirstLine = true;
            while ((line = reader.ReadLine()) != null)
            {
                if (isFirstLine) { isFirstLine = false; continue; }
                string[] values = line.Split(',');
                if (values.Length < 2) continue;
                string cardId = values[0];
                int count = int.Parse(values[1]);
                string cardName = values[2];
                string type = values[3];
                if (type == "アカウント" || type == "ミッション") continue;
                for (int i = 0; i < count; i++)
                {
                    deckCards.Add(new CardInfo { cardId = cardId, cardName = cardName });
                }
            }
        }
    }

    void ShuffleDeck()
    {
        System.Random rand = new System.Random();
        int n = deckCards.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            var temp = deckCards[k];
            deckCards[k] = deckCards[n];
            deckCards[n] = temp;
        }
    }

    void PlaceDeck()
    {
        if (pDeck != null)
        {
            Image image = pDeck.GetComponent<Image>();
            if (image == null)
                image = pDeck.AddComponent<Image>();
            image.sprite = cardBackSprite;
            image.preserveAspect = true;
            RectTransform rect = pDeck.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(100, 140); // デッキ表示サイズ
        }
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deckCards.Count == 0) break;

            CardInfo drawnCard = deckCards[0];
            deckCards.RemoveAt(0);
            playerHand.Add(drawnCard);

            GameObject cardObject = Instantiate(cardPrefab, playerHandZone);
            cardObject.name = drawnCard.cardName;
            Image img = cardObject.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = Resources.Load<Sprite>($"images/{drawnCard.cardId}"); // 表面画像に修正
            }

            handCards.Add(cardObject);
        }
        ArrangeHandCards();
        UpdateDeckCountDisplay();
    }

    void ArrangeHandCards()
    {
        float spacing = 100f; // 重ならないように間隔広く
        float totalWidth = (handCards.Count - 1) * spacing;
        float startX = -totalWidth / 2;

        for (int i = 0; i < handCards.Count; i++)
        {
            RectTransform rect = handCards[i].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(startX + i * spacing, 0);
        }
    }

    void UpdateDeckCountDisplay()
    {
        if (deckCountLabel != null)
        {
            deckCountLabel.text = deckCards.Count.ToString();
        }
    }

    public int GetHandCount()
    {
        return playerHand.Count;
    }
}

[System.Serializable]
public class CardInfo
{
    public string cardId;
    public string cardName;
}
