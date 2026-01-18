public class CardData
{
    public string cardId;     // 画像ファイル名など
    public string cardName;   // カード名
    public string ability;    // 効果テキスト

    public CardData(string id, string name, string abilityText)
    {
        cardId = id;
        cardName = name;
        ability = abilityText;
    }
}
