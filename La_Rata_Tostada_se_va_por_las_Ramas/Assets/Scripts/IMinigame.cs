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

    public void MinigameComplete(bool success)
    {
        GameManager.instance.score += CalculateScore();
        GameManager.instance.OpenMapScene();
    }

    public abstract int CalculateScore();
}
