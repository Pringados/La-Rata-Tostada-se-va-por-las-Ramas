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

        hint.SetActive(pause);
    }

    public void OnClickPause()
    {
        pause = !pause;

        //Time.timeScale = pause ? 0 : 1;

        hint.SetActive(pause);
    }
}
