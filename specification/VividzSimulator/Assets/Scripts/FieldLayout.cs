using System.Collections.Generic;
using UnityEngine;

public class FieldLayout : MonoBehaviour
{
    public CardPlacer cardPlacer;
    public Sprite missionSprite1;
    public Sprite missionSprite2;
    public Sprite missionSprite3;

    void Start()
    {
        // MissionCardData という構造体に ID と frontSprite を持たせる
        List<MissionCardData> missions = new List<MissionCardData>
        {
            new MissionCardData("F01-004", missionSprite1),
            new MissionCardData("F01-008", missionSprite2),
            new MissionCardData("F01-013", missionSprite3)
        };

        cardPlacer.PlaceMissionCards(missions);
    }
}
