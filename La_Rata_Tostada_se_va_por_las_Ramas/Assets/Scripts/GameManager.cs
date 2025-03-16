using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MapManager mapManager;

    public float totalTimeToRagnarok;
    public float remainingTimeToRagnarok;

    public SnakeBar snake;
    //float time = 0f;

    public int score;
    public bool timerPaused = false;

    private bool init = false;

    private int actionDownCounters = 0;

    StudioEventEmitter emitter;

    void Awake()
    {
        emitter = GetComponentInChildren<StudioEventEmitter>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            remainingTimeToRagnarok = totalTimeToRagnarok;
        }
        else
            Destroy(this.gameObject);
        
    }

    void Update()
    {
        if (!timerPaused && remainingTimeToRagnarok >= 0f)
            remainingTimeToRagnarok -= Time.deltaTime;

        if (remainingTimeToRagnarok <= 0)
            OnDefeat(); 

        emitter.SetParameter("RemainingTime", remainingTimeToRagnarok / totalTimeToRagnarok);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnDefeat()
    {
        ChangeScene("End");
        emitter.SetParameter("End", 1);
    }

    public void OpenMapScene()
    {
        SceneManager.LoadScene("Delivery_Road");
        mapManager.gameObject.SetActive(true);
    }

    public void increaseTimeToRagnarok(float n)
    {
        remainingTimeToRagnarok += n;
        if (remainingTimeToRagnarok > totalTimeToRagnarok)
            remainingTimeToRagnarok = totalTimeToRagnarok;

        snake.DelaySnake();

        if (remainingTimeToRagnarok >= 1)
            return; 
    }

    public void decreaseTimeToRagnarok(float n)
    {
        remainingTimeToRagnarok -= n;

        if (remainingTimeToRagnarok <= 0)
            remainingTimeToRagnarok = 0;
    }

    // Devuelve el tiempo restante como float de 0 a 1 (tiempo m�ximo)
    public float GetRemainingTimePortion() { return remainingTimeToRagnarok / totalTimeToRagnarok; }

    public void SetSnake(SnakeBar snek)
    {
        snake = snek;
    }

    public void SetMusicAction(bool action)
    {
        if (action && --actionDownCounters <= 0)
            emitter.SetParameter("Action", 1f);
        else
        {
            emitter.SetParameter("Action", 0f);
            actionDownCounters++;
        }
    }
}
