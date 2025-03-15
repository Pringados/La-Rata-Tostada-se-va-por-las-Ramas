using UnityEngine;
using UnityEngine.UI;

public abstract class IMinigame : MonoBehaviour
{
    [SerializeField] protected Text textHint;

    [SerializeField] protected Image hintImage;

    [SerializeField] protected Image NPC;

    [SerializeField] protected MinigameData data;

    void Awake()
    {
        hintImage.sprite = data.hint;

        NPC.sprite = data.character; 

        textHint.text = data.textHint; 
    }

    public void MinigameComplete(bool success)
    {
        GameManager.instance.score += CalculateScore();
        GameManager.instance.OpenMapScene();
    }

    public abstract int CalculateScore();
}
