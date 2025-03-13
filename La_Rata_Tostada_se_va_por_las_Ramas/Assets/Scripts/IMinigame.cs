using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMinigame : MonoBehaviour
{
    [SerializeField]
    protected GameObject hintCanvas;
    [SerializeField]
    protected MinigameData data;

    void Awake()
    {
        if (data.showHint)
        {
            hintCanvas.SetActive(true);
            data.showHint = false;
        }
        else
            hintCanvas.SetActive(false);
    }

    public void MinigameComplete(bool success)
    {
        GameManager.instance.score += CalculateScore();
        GameManager.instance.OpenMapScene();
    }

    public abstract int CalculateScore();
}
