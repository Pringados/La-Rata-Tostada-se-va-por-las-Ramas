using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Facundo : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    int numberOfPathNodes;

    [SerializeField, Range(1f, 10f)]
    float maxSpeed;
    [SerializeField, Range(1f, 10f)]
    float acceleration;

    float speed;

    [SerializeField]
    GameObject trail;

    [SerializeField]
    FacundoManager manager;

    public bool moving = false;
    private bool trailComplete = false;
    private int nextNode;
    private Vector3[] path;
    private BoxCollider2D col;
    private SpriteRenderer sprite;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void StopMoving()
    {
        moving = false;
        LeanTween.cancelAll(this.gameObject);
    }
    private void Update()
    {
        if (moving) 
        {
            if (speed < maxSpeed) { speed += acceleration * Time.deltaTime; }
            sprite.flipX = transform.position.x > path[nextNode].x;

            transform.position = Vector3.MoveTowards(transform.position, path[nextNode], speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, path[nextNode]) < 0.1f && ++nextNode >= numberOfPathNodes)
            {
                Debug.Log("END of path");
                trailComplete = true;
                moving = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            manager.addTrailObject(Instantiate(trail, transform.position, Quaternion.identity));
        }
    }

    private void OnMouseDown()
    {
        if (!manager.trackingActive)
        {
            trailComplete = false;
            manager.trackingActive = true;
            moving = true;
            nextNode = 0;
            speed = 0f;
            path = new Vector3[numberOfPathNodes];
            for (int i = 0; i < numberOfPathNodes; i++)
            {
                path[i] = DestinationPoint(i);
            }
            /*LeanTween.move(this.gameObject, path[0], Vector3.Distance(path[0], path[1]) / midSpeed).setEase(LeanTweenType.linear);
            Debug.Log("Step number 0. Duration: " + Vector3.Distance(path[0], path[1]) / midSpeed + "   target position: " + path[0]);

            float delay = 0f, stepDuration, previousStepDuration = Vector3.Distance(path[0], path[1]) / midSpeed;
            for(int i = 1; i < numberOfPathNodes - 1; i++)
            {
                stepDuration = Vector3.Distance(path[i], path[i + 1]) / midSpeed;
                LeanTween.move(this.gameObject, path[i], stepDuration).setDelay(delay += previousStepDuration).setEase(LeanTweenType.linear);
                Debug.Log("Step number " + i + ". Duration: " + stepDuration + "   target position: " + path[i] + "   delay: " + delay);
                previousStepDuration = stepDuration;
            }
            LeanTween.move(this.gameObject, path[path.Length - 1], 3f).setEase(LeanTweenType.linear).setDelay(delay += previousStepDuration)
                .setOnComplete(delegate () {
                    Debug.Log("END of path");
                    trailComplete = true;
                    moving = false;
                });*/

        }
    }

    private Vector3 DestinationPoint(int index)
    {
        float x = Random.Range(-6f, 6f);
        float y = (index < numberOfPathNodes - 1) ?  3.5f - 8 * index / numberOfPathNodes + Random.Range(-1f, 1f) : -4f;

        return new Vector3(x, y, 0f);
    }

    private void OnMouseUp()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0f;
        Debug.Log("Mouse up.  Tracking: " + manager.trackingActive + "   Trail Complete: " + trailComplete + "   Mouse pos: " + mousePoint);
        if (col.bounds.Contains(mousePoint) && trailComplete && manager.trackingActive)
        {
            Debug.Log("COMPLETE!!!");
            manager.MinigameComplete(true);
        }
    }
}
