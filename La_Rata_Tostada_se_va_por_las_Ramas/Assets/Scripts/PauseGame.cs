using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject hint;
    [SerializeField] private GameObject showHintButton;
    [SerializeField] private GameObject menuButton;

    bool pause = true; 

    private void Start()
    {
        if (pause) Pause();
        menuButton.SetActive(false);
    }

    public void TogglePause()
    {
        pause = !pause;
        Debug.Log("PAUSE: " + pause);
        Pause();
    }

    private void Pause()
    {
        GameManager.instance.timerPaused = pause;
        Time.timeScale = pause ? 0 : 1;
        hint.SetActive(pause);
        menuButton.SetActive(pause);
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
