using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public MissionEffectManager missionEffectManager;
    public FieldManager playerField;

    // CardPlacer.csから設定されるミッションカード一覧
    public List<MissionCard> missionCards = new List<MissionCard>();

    public void CheckMissionCard(MissionCard card)
    {
        if (CheckCondition(card))
        {
            card.SetSleep();
            missionEffectManager.ShowMissionText("MISSION CHECK!");
        }
    }

    public void ResolveMissionCard(MissionCard card)
    {
        if (ClearCondition(card))
        {
            card.Flip();
            missionEffectManager.ShowMissionText("MISSION CLEAR!");
        }
        else
        {
            card.WakeUp();
        }
    }

    bool CheckCondition(MissionCard card)
    {
        switch (card.id)
        {
            case "F01-004":
                return CheckCondition_F01004();
            default:
                return false;
        }
    }

    bool ClearCondition(MissionCard card)
    {
        switch (card.id)
        {
            case "F01-004":
                return ClearCondition_F01004(card);
            default:
                return false;
        }
    }

    // チェック条件：F01-004（パワーでフィニッシュ！）
    bool CheckCondition_F01004()
    {
        foreach (var chara in playerField.GetAllCharacters())
        {
            if (!chara.cardName.Contains("キョウカ")) continue;
            if (!chara.HasAttribute("サイバー")) continue;
            if (chara.cost < 5) continue;
            if (chara.powerBoost < 4000) continue;
            if (!chara.HasAttackedSuccessfullyThisTurn()) continue;

            return true;
        }
        return false;
    }

    // クリア条件：F01-004
    bool ClearCondition_F01004(MissionCard card)
    {
        var opponent = playerField.GetOpponentCharacterInSameLane(card);
        return opponent == null;
    }

    // 横向きのカードをすべてチェック → 裏返す or 戻す
    public void CheckMissionClear()
    {
        foreach (MissionCard card in missionCards)
        {
            if (card.IsSleeping())
            {
                ResolveMissionCard(card);
            }
        }

        if (missionCards.All(c => c.IsCleared()))
        {
            missionEffectManager.ShowMissionText("MISSION ALL CLEAR!");
            Debug.Log("ミッション3枚すべてクリア！勝利！");
            // TODO: 勝利処理
        }
    }
}
