using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float totalTimeToRagnarok;
    public float remainingTimeToRagnarok;
 

    //float time = 0f;

    public int score;
    public bool timerPaused = false;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        remainingTimeToRagnarok = totalTimeToRagnarok;
    }

    void Update()
    {
        if(!timerPaused)
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
    }

    public void increaseTimeToRagnarok(float n)
    {
        remainingTimeToRagnarok += n;
        if (remainingTimeToRagnarok > totalTimeToRagnarok)
            remainingTimeToRagnarok = totalTimeToRagnarok;

        UIManager.Instance.DelaySnake();

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

}
