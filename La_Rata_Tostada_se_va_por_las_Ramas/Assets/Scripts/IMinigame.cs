using UnityEngine;
using UnityEngine.UI;

public abstract class IMinigame : MonoBehaviour
{
    [SerializeField] protected Text textHint;

    [SerializeField] protected MinigameData data;

    void Awake()
    {
        //textHint.text = data.textHint; 
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
