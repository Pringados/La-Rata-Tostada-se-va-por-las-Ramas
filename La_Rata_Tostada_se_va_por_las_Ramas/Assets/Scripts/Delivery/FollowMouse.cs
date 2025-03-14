using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    [SerializeField, Tooltip("Límite en el eje X hasta el cual se puede mover.")]
    float xLimit;

    [SerializeField, Tooltip("Límite en el eje Y hasta el cual se puede mover.")]
    float yLimit;

    [SerializeField]
    bool followX = true;
    [SerializeField]
    bool followY = true;

    public bool stunned;

    void Start()
    {
        stunned = false;
    }

    void Update()
    {
        if (stunned) return;

        var camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camPos.z = 0f;

        if (!followX)
            camPos.x = transform.position.x;
        else
            camPos.x = Mathf.Clamp(camPos.x, -xLimit, xLimit);

        if (!followY)
            camPos.y = transform.position.y;
        else
            camPos.y = Mathf.Clamp(camPos.y, -yLimit, yLimit);

        transform.position = Vector3.MoveTowards(transform.position, camPos, moveSpeed * Time.deltaTime);
    }
}
