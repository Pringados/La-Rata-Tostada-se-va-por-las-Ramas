using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : IMinigame
{
    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private float maxTime;

    [SerializeField] private int points;

    private float time;

    void Start()
    {
        time = maxTime;

        StartCoroutine(spawnerObstacles()); 
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            MinigameComplete(true); 

            time = maxTime; 
        }
    }

    private IEnumerator spawnerObstacles()
    {
        while (time > 0)
        {
            int n = Random.Range(0, obstacles.Length);

            Instantiate(obstacles[n], new Vector3(transform.position.x * 5, transform.position.y, 0f), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.8f, 1.6f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject); 
    }

    public override int CalculateScore()
    {
        return points;
    }
}
