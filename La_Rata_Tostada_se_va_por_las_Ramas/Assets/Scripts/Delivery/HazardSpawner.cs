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

    private int branchRenderOrder = 300;

    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        float posX = transform.position.x + Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
        //float posX = transform.position.x + transform.localScale.x - 0.01f;
        float angle = -90 * Mathf.Asin(posX / transform.localScale.x);
        GameObject nuevo = Instantiate(hazards[Random.Range(0, hazards.Length)], new Vector3(posX, transform.position.y, 0f), Quaternion.Euler(0f, 0f, angle));
        nuevo.GetComponent<DeliveryHazard>().manager = manager;
        nuevo.GetComponent<SpriteRenderer>().sortingOrder = branchRenderOrder;
        branchRenderOrder--;

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
