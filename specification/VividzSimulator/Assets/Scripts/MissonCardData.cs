using UnityEngine;

[System.Serializable]
public class MissionCardData
{
    public string id;       // 例: "F01-004"
    public string name;     // 例: "パワーでフィニッシュ!"
    public string type;     // 例: "ミッション"
    [TextArea]
    public string text;     // 効果テキスト

    public Sprite frontSprite;

     public MissionCardData(string id, Sprite frontSprite)
    {
        this.id = id;
        this.frontSprite = frontSprite;
    }
}
