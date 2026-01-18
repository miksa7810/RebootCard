using UnityEngine;
using TMPro;

public class PhaseStatusDisplay : MonoBehaviour
{
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI turnOwnerText;

    private TurnManager turnManager;

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        UpdateStatus();  // ゲーム開始時に一度表示
    }

    public void UpdateStatus()
    {
        if (turnManager == null) return;

        turnText.text = $"Turn {turnManager.GetTurnCount()}";
        phaseText.text = turnManager.GetCurrentPhase().ToString();
        turnOwnerText.text = "Player"; // 今はプレイヤー固定、今後CPUターン対応時に拡張
    }
}
