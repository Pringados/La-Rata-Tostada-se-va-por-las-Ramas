using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject hint; 

    private bool pause; 

    private void OnEnable()
    {
        pause = true;
        GameManager.instance.timerPaused = true;

        Time.timeScale = pause ? 0 : 1;

        hint.SetActive(pause);
    }

    public void OnClickPause()
    {
        pause = !pause;
        GameManager.instance.timerPaused = pause;
        Time.timeScale = pause ? 0 : 1;

        hint.SetActive(pause);
    }
}
