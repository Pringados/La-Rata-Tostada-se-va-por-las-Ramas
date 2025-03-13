using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    bool followX = true;
    [SerializeField]
    bool followY = true;

    void Start()
    {
        
    }

    void Update()
    {
        var camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camPos.z = 0f;

        if (!followX)
            camPos.x = transform.position.x;
        if (!followY)
            camPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, camPos, moveSpeed * Time.deltaTime);
    }
}
