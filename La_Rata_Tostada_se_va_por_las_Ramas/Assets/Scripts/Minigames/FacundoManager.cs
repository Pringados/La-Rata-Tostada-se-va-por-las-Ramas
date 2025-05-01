using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacundoManager : IMinigame
{
    [SerializeField] Facundo facundo;
    public bool trackingActive = false;
    List<RastroFacundo> trail;

    float immuneTime = 1f;
    public float clickTime;

    public override float CalculateScore()
    {
        return 500;
    }

    new void Awake()
    {
        base.Awake();
        trail = new List<RastroFacundo>();
    }

    public void AddTrailObject(RastroFacundo o)
    {
        Debug.Log("AddObject");
        trail.Add(o);
    }

    public void RemoveTrailObject(RastroFacundo o)
    {
        trail.Remove(o);
    }

    private void ClearTrail()
    {
        foreach (RastroFacundo o in trail)
        {
            o.RemoveTrail();
        }
        //trail.Clear();
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

