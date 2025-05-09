using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    GameObject mapManagerPrefab;
    public MapManager mapManager;

    public float totalTimeToRagnarok;
    public float remainingTimeToRagnarok;

    public SnakeBar snake;
    //float time = 0f;

    public float score;
    public bool timerPaused = false;

    private int actionDownCounters = 0;

    StudioEventEmitter emitter;
    private bool musicPlaying = false;

    void Awake()
    {
        emitter = GetComponentInChildren<StudioEventEmitter>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            remainingTimeToRagnarok = totalTimeToRagnarok;
            mapManager = Instantiate(mapManagerPrefab).GetComponentInChildren<MapManager>();
            mapManager.gameObject.SetActive(false);
        }
        else
            Destroy(this.gameObject);
    }

    void Update()
    {
        if(musicPlaying == false && Input.anyKeyDown)
        {
            emitter.Play();
            musicPlaying = true;
        }

        if (!timerPaused && remainingTimeToRagnarok >= 0f)
        {
            remainingTimeToRagnarok -= Time.deltaTime;
            score += Time.deltaTime * 4;
        }

        if (remainingTimeToRagnarok <= 0)
            OnDefeat(); 

        if(emitter != null)
        {
            emitter.SetParameter("RemainingTime", remainingTimeToRagnarok / totalTimeToRagnarok);
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ResetGame()
    {
        remainingTimeToRagnarok = totalTimeToRagnarok;
        score = 0;
        if(emitter != null)
        {
            emitter.Stop();
            emitter.SetParameter("End", 0);
            emitter.Play();
        }
        ChangeScene("Menu");
    }

    public void OnDefeat()
    {
        remainingTimeToRagnarok = totalTimeToRagnarok;
        emitter.SetParameter("End", 1);
        timerPaused = true;
        ChangeScene("End");
    }

    public void OpenMapScene()
    {
        ChangeScene("Delivery_Road");
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
    
    public void shieldedRat()
    {
        GameManager.instance.GetComponent<Inventario>().SetShield(true);
    }

    public void speedRat()
    {
        GameManager.instance.GetComponent<Inventario>().SetSpeed(true);
    }

    public void QuitGame() { Application.Quit(); }
}
