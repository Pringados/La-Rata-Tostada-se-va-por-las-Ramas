using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void ReturnToMenu()
    {
        GameManager.instance.ResetGame();
    }

    public void StartGame()
    {
        GameManager.instance.OpenMapScene();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
