using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Mínimo tiempo que puede pasar entre spawns.")]
    float minCooldown;
    [SerializeField, Tooltip("Máximo tiempo que puede pasar entre spawns.")]
    float maxCooldown;

    [SerializeField]
    public DeliveryRoadManager manager;

    [SerializeField]
    GameObject[] hazards;

    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        Instantiate(hazards[Random.Range(0, hazards.Length)], new Vector3(transform.position.x + Random.Range(-transform.localScale.x, transform.localScale.x), 
            transform.position.y, 0f), Quaternion.identity).GetComponent<DeliveryHazard>().manager = manager;

        float cooldown = Random.Range(minCooldown, maxCooldown);
        while (cooldown > 0f)
        {
            yield return null;
            if (manager.scrolling)
                cooldown -= Time.deltaTime;
        }
        StartCoroutine(SpawnObstacle());
    }
}
