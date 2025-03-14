using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] protected float timeToRagnarok;

    //float time = 0f;

    public int score;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    void Update()
    {
        //if ((int)time < (int)(time += Time.deltaTime))
            //Debug.Log((int)time);

        if (Input.GetKeyDown(KeyCode.Space))
            ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Example minigame");
    }

    public void OpenMapScene()
    {
        Debug.LogError("NOT IMPLEMENTED");
    }

    public void increaseTimeToRagnarok(float n)
    {
        timeToRagnarok += n;

        if (timeToRagnarok >= 1)
            return; 
    }

    public void decreaseTimeToRagnarok(float n)
    {
        timeToRagnarok -= n;

        if (timeToRagnarok <= 0)
            timeToRagnarok = 0;
    }

    public float getTimeToRagnarok() { return timeToRagnarok; }
}
