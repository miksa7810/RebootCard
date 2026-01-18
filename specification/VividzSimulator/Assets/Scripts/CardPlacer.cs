using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlacer : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform[] missionZones;
    public Sprite cardBackSprite;
    public MissionManager missionManager;

    private List<MissionCard> missionCards = new List<MissionCard>();

    public void PlaceMissionCards(List<MissionCardData> missionCardDataList)
    {
        missionCards.Clear();

        for (int i = 0; i < missionCardDataList.Count && i < missionZones.Length; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, missionZones[i]);
            MissionCard missionCard = cardObj.GetComponent<MissionCard>();

            // MissionCardData から設定をコピー
            missionCard.id = missionCardDataList[i].id;
            missionCard.frontSprite = missionCardDataList[i].frontSprite;
            missionCard.backSprite = cardBackSprite;

            // 表面の画像を設定
            Image img = cardObj.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = missionCard.frontSprite;
            }

            missionCards.Add(missionCard);
        }

        // MissionManager に登録
        if (missionManager != null)
        {
            missionManager.missionCards = missionCards;
        }
    }

    public void CheckMissionClear()
    {
        if (missionManager != null)
        {
            missionManager.CheckMissionClear();
        }
    }
}
