using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeliveryHazard : MonoBehaviour
{
    public ImgScroll scroller;
    StudioEventEmitter emitter;

    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FollowMouse>() != null) 
        {
            emitter.Play();
            scroller.pauseScroll(1f);
        }
        Destroy(this.gameObject);
    }
}
