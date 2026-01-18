using UnityEngine;
using UnityEngine.UI;

public class MissionCard : MonoBehaviour
{
    public string id;       // ミッションカードのID（例："F01-004"）
    public bool isSleeping = false;
    public bool isCleared = false;

    public Image cardImage;
    public Sprite frontSprite;
    public Sprite backSprite;

    private Quaternion uprightRotation;
    private Quaternion sleepRotation;

    private void Awake()
    {
        uprightRotation = Quaternion.identity;
        sleepRotation = Quaternion.Euler(0, 0, -90);
        cardImage = GetComponent<Image>();
    }

    public void SetSleep()
    {
        isSleeping = true;
        transform.rotation = sleepRotation;
    }

    public void WakeUp()
    {
        isSleeping = false;
        transform.rotation = uprightRotation;
    }

    public void Flip()
    {
        isCleared = true;
        cardImage.sprite = backSprite;
    }

    public bool IsSleeping()
    {
        return isSleeping; // 横向きかどうか
    }

    public bool IsCleared()
    {
        return isCleared; // 裏返し済かどうか
    }


}
