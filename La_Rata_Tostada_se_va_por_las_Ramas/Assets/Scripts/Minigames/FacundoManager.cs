using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacundoManager : IMinigame
{
    [SerializeField] Facundo facundo;
    public bool trackingActive = false;
    List<GameObject> trail;

    float immuneTime = 1f;
    public float clickTime;

    public override float CalculateScore()
    {
        return 500;
    }


    void Awake()
    {
        trail = new List<GameObject>();
    }

    public void addTrailObject(GameObject o)
    {
        trail.Add(o);
    }

    private void ClearTrail()
    {
        foreach (GameObject o in trail)
        {
            Destroy(o);
        }
        trail.Clear();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            ClearTrail();
            facundo.StopMoving();
            trackingActive = false; 
        }
        if (trackingActive)
        {
            if (Time.time < clickTime + immuneTime) return;
            Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward);
            RaycastHit2D hit;
            
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            //Debug.Log("hit: " + hit + "    collider: " + hit.collider + "   Layer: " + LayerMask.NameToLayer("Background"));
            if (hit.collider?.gameObject.layer == LayerMask.NameToLayer("Background"))
            {
                ClearTrail();
                facundo.StopMoving();
                trackingActive = false;
            }
        }
    }
}

