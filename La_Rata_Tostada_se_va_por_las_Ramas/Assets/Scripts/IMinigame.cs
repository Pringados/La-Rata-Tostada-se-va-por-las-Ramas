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
        GameManager.instance.increaseTimeToRagnarok(score / 100);
        GameManager.instance.score += score;
        GameManager.instance.OpenMapScene();
    }

    public abstract int CalculateScore();
}
