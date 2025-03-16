using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UnityEngine.Debug.Log("DontDestroy");
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
