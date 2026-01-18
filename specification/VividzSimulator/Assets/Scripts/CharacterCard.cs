using UnityEngine;
using System.Collections.Generic;

public class CharacterCard : MonoBehaviour
{
    public string cardName;
    public int cost;
    public int power;
    public int powerBoost; // ←追加
    public bool isCyber;
    public bool isKyouka;

    public List<string> expNames = new List<string>();

    public bool HasExp(string expCardName)
    {
        return expNames.Contains(expCardName);
    }

    public bool HasAttribute(string attr)
    {
        return attr == "サイバー" && isCyber;
    }

    public bool HasAttackedSuccessfullyThisTurn()
    {
        // 仮実装（実際にはアタック後の状態管理が必要）
        return true;
    }
}
