using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    private DeckManager deckManager;
    private CardPlacer cardPlacer;
    private MissionManager missionManager;

    private int turnCount = 0;

    public enum Phase
    {
        StartDraw,
        Main,
        Mission,
        Charge,
        EndDraw
    }

    public Phase currentPhase;

    void Start()
    {
        deckManager = Object.FindFirstObjectByType<DeckManager>();
        cardPlacer = Object.FindFirstObjectByType<CardPlacer>();
        missionManager = Object.FindFirstObjectByType<MissionManager>();
        StartCoroutine(StartTurn());
    }

    IEnumerator StartTurn()
    {
        yield return null;
        turnCount++;
        Debug.Log($"================== ターン {turnCount} 開始 ==================");
        currentPhase = Phase.StartDraw;
        ExecutePhase();
    }

    void ExecutePhase()
    {
        switch (currentPhase)
        {
            case Phase.StartDraw:
                PerformStartDraw();
                break;
            case Phase.Main:
                PerformMainPhase();
                break;
            case Phase.Mission:
                PerformMissionPhase();
                break;
            case Phase.Charge:
                PerformChargePhase();
                break;
            case Phase.EndDraw:
                PerformEndDraw();
                break;
        }
    }

    void PerformStartDraw()
    {
        Debug.Log("StartDraw フェイズ開始");

        if (deckManager == null)
        {
            Debug.LogError("DeckManager が見つかりません。");
            return;
        }

        int handCount = deckManager.GetHandCount();

        if (handCount < 5)
        {
            deckManager.DrawCards(5 - handCount);
        }
        else
        {
            deckManager.DrawCards(1);
        }

        Debug.Log("StartDraw フェイズ終了。メインフェイズへ進行。");
        currentPhase = Phase.Main;
        ExecutePhase();
    }

    void PerformMainPhase()
    {
        Debug.Log("メインフェイズ処理中...");

        // ミッションチェックをここで実行
        if (missionManager != null)
        {
            foreach (MissionCard card in missionManager.missionCards)
            {
                missionManager.CheckMissionCard(card);
            }
        }

        Debug.Log("メインフェイズ終了。次はミッションフェイズへ。");
        // 手動で進めるため遷移しない
    }

    void PerformMissionPhase()
    {
        Debug.Log("ミッションフェイズ処理中... ミッションカードをチェックします。");

        if (cardPlacer != null)
        {
            cardPlacer.CheckMissionClear();
        }

        // 手動で進めるため遷移しない
    }

    void PerformChargePhase()
    {
        Debug.Log("チャージフェイズ処理中... (任意の処理を実装可能)");
        Debug.Log("チャージフェイズ終了。次はエンドドローフェイズへ。");
        currentPhase = Phase.EndDraw;
        ExecutePhase();
    }

    void PerformEndDraw()
    {
        Debug.Log("EndDraw フェイズ開始");

        if (deckManager == null)
        {
            Debug.LogError("DeckManager が見つかりません。");
            return;
        }

        int handCount = deckManager.GetHandCount();

        if (handCount < 5)
        {
            deckManager.DrawCards(5 - handCount);
        }
        else
        {
            deckManager.DrawCards(1);
        }

        Debug.Log("EndDraw フェイズ終了。次のターンへ進行。");
        StartCoroutine(StartTurn());
    }

    public void ProceedToMissionPhase()
    {
        currentPhase = Phase.Mission;
        ExecutePhase();
    }

    public void ProceedToCheckPhase()
    {
        currentPhase = Phase.Charge;
        ExecutePhase();
    }

    public void EndTurn()
    {
        Debug.Log("ターン終了ボタンが押されました。エンドドローフェイズへ移行。");
        currentPhase = Phase.EndDraw;
        ExecutePhase();
    }

    public void OnEndTurnButtonPressed()
    {
        EndTurn();
    }

    public int GetTurnCount()
    {
        return turnCount;
    }

    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }
}
