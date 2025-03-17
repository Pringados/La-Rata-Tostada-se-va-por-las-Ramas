using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject hint;
    [SerializeField] private GameObject showHintButton;

    bool pause = true; 

    private void Start()
    {
        Debug.Log(pause);
        if (pause) Pause();
    }

    public void TogglePause()
    {
        Debug.Log("PAUSE");
        pause = !pause;
        Pause();
    }

    private void Pause()
    {
        GameManager.instance.timerPaused = pause;
        Time.timeScale = pause ? 0 : 1;
        hint.SetActive(pause);
        showHintButton.SetActive(!pause);
        GameManager.instance.SetMusicAction(false);
    }

    private void Update()
    {
        //Debug.Log(pause + "   " + Input.GetMouseButtonDown(0));
        if (pause && Input.GetMouseButtonDown(0))
            TogglePause();
    }
}
