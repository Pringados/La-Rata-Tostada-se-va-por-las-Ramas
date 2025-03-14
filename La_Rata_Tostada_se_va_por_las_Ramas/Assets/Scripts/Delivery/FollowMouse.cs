using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;

    [SerializeField, Tooltip("Límite en el eje X hasta el cual se puede mover.")]
    float xLimit;

    [SerializeField, Tooltip("Límite en el eje Y hasta el cual se puede mover.")]
    float yLimit;

    [SerializeField]
    bool followX = true;
    [SerializeField]
    bool followY = true;

    [SerializeField]
    float maxRotation = 0.12f;

    [SerializeField]
    float rotationSpeed = 4f;

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

        float oldPos = transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, camPos, maxSpeed * Time.deltaTime);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, (-maxRotation * (transform.position.x - oldPos) / (maxSpeed * Time.deltaTime))), rotationSpeed * Time.deltaTime);
    }
}
