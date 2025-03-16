using UnityEngine;
using UnityEngine.UI;

public abstract class IMinigame : MonoBehaviour
{
    [SerializeField] protected Text textHint;

    [SerializeField] protected MinigameData data;

    void Awake()
    {
        textHint.text = data.textHint; 
    }

    private void Update()
    {
        if (GameManager.instance.remainingTimeToRagnarok <= 0)
            GameManager.instance.ChangeScene("End");
    }

    public void MinigameComplete(bool success)
    {
        int score = CalculateScore();
        if(data.minigameType == MinigameData.MinigameType.Bonus)
        {
            switch (data.bonusType)
            {
                case MinigameData.BonusType.None:
                    break;
                case MinigameData.BonusType.Time:
                    GameManager.instance.increaseTimeToRagnarok(score/50);
                    break;
                case MinigameData.BonusType.Shield:
                    GameManager.instance.shieldedRat();
                    break;
                case MinigameData.BonusType.Speed:
                    GameManager.instance.speedRat();
                    break;
            }
        }
        else { 
            GameManager.instance.increaseTimeToRagnarok(score / 100); 
        }
        GameManager.instance.score += score;
        GameManager.instance.OpenMapScene();
    }

    public abstract int CalculateScore();
}
