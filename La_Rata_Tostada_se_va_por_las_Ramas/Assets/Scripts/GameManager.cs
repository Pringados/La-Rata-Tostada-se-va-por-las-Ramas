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

    void Awake()
    {
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

        //if ((int)time < (int)(time += Time.deltaTime))
            //Debug.Log((int)time);

        if (Input.GetKeyDown(KeyCode.Space))
            increaseTimeToRagnarok(1f);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
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

    public void shieldedRat()
    {
        GameManager.instance.GetComponent<Inventario>().SetShield(true);
    }

    public void speedRat()
    {
        GameManager.instance.GetComponent<Inventario>().SetSpeed(true);
    }
}
