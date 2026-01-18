using UnityEngine;
using TMPro;
using System.Collections;

public class MissionEffectManager : MonoBehaviour
{
    public TextMeshProUGUI missionLabel;

    public void ShowMissionText(string text)
    {
        StartCoroutine(ShowTextRoutine(text));
    }

    IEnumerator ShowTextRoutine(string text)
    {
        missionLabel.text = text;
        missionLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        missionLabel.gameObject.SetActive(false);
    }
}
