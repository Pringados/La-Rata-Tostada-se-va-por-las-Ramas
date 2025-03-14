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
    ImgScroll scroller;

    [SerializeField]
    GameObject[] hazards; 

    float cooldown;
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        Instantiate(hazards[Random.Range(0, hazards.Length)], new Vector3(transform.position.x + Random.Range(-transform.localScale.x, transform.localScale.x), 
            transform.position.y, 0f), Quaternion.identity).GetComponent<DeliveryHazard>().scroller = scroller;
        yield return new WaitForSeconds(Random.Range(minCooldown, maxCooldown));
        StartCoroutine(SpawnObstacle());
    }
}
