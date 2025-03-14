using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeliveryHazard : MonoBehaviour
{
    public float speed;
    public DeliveryRoadManager manager;
    StudioEventEmitter emitter;
    public bool isBranch;

    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        if (isBranch)
        {
            manager.AddBranch(this);
            speed = manager.scrollSpeed;
        }
    }

    void Update()
    {
        if(manager.scrolling)
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FollowMouse>() != null) 
        {
            emitter.Play();
            manager.pauseScroll(1f);
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (isBranch) manager?.RemoveBranch(this);
    }
}
