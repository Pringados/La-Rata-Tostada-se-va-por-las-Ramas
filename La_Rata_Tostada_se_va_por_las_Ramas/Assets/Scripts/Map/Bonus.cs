using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MapNode
{

    public int Id; //0 es cafeina, 1 es escudo, 2 es reloj, 3 es tirita
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public int GetId()
    {
        return Id;
    }
}
