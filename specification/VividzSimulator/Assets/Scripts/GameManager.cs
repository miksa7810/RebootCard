using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Phase { StartDraw, Main, Mission, Charge, EndDraw }
    public enum Turn { Player, CPU }

    public Turn currentTurn = Turn.Player;
    public Phase currentPhase = Phase.StartDraw;

    public List<Card> playerHand = new List<Card>();
    public List<Card>[] playerRear = { new List<Card>(), new List<Card>(), new List<Card>() };
    public List<Card>[] cpuRear = { new List<Card>(), new List<Card>(), new List<Card>() };

    public GameObject cardPrefab;     // Unity上で登録する
    public Transform handArea;        // カードを並べるUIエリア

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        LoadCards();
        DisplayHand(); // 手札を表示
    }

    void LoadCards()
    {
        TextAsset json = Resources.Load<TextAsset>("Data/cards_with_images");
        CardDataList loaded = JsonUtility.FromJson<CardDataList>(json.text);
        playerHand = loaded.cards;
    }

void DisplayHand()
{
    foreach (Card card in playerHand)
    {
        GameObject cardObj = Instantiate(cardPrefab, handArea);
        Image img = cardObj.GetComponent<Image>();

        // パスや拡張子を除去して画像読み込み
        string fileName = System.IO.Path.GetFileNameWithoutExtension(card.image);
        Sprite sprite = Resources.Load<Sprite>("images/" + fileName); // ← images は小文字のフォルダ名

        if (sprite != null)
        {
            img.sprite = sprite;
        }
        else
        {
            Debug.LogError("画像が読み込めませんでした: " + card.image);
        }
    }
}


    [System.Serializable]
    public class CardDataList
    {
        public List<Card> cards;
    }
}
