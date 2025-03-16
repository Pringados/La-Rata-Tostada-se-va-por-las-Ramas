using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MapNode
{
    public int time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(int n)
    {
        time = n;
    }

    public int GetTime()
    {
        return time;
    }
}
