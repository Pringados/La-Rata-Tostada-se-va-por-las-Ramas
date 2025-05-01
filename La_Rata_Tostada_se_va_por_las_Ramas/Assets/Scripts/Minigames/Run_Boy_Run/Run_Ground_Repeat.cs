using System.Collections.Generic;
using UnityEngine;

public class Run_Ground_Repeat : MonoBehaviour
{
    [SerializeField] private float speed;

    Vector3 startPos;

    bool scrolling = true;

    public List<RunHazard> hazards;

    void Awake()
    {
        startPos = transform.position;
        hazards = new List<RunHazard>();
    }

    private void Update()
    {
        if (scrolling) transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
    }

    public void Reset(float resetTime)
    {
        scrolling = false;
        LeanTween.move(this.gameObject, startPos, resetTime).setEase(LeanTweenType.easeOutQuad).setOnComplete(delegate () { scrolling = true; });
        foreach(RunHazard hazard in hazards)
        {
            hazard.Reset();
        }
    }
}
