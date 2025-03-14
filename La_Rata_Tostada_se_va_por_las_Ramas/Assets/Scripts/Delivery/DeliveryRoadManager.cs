using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryRoadManager : MonoBehaviour
{
    public float duration;
    public float scrollSpeed;
    public float playerImmuneTime;

    float remainingTime;

    [SerializeField]
    HazardSpawner spawner;

    [SerializeField]
    ImgScroll trunk;

    [SerializeField]
    FollowMouse player;

    BoxCollider2D playerCol;

    public bool scrolling;

    // En el caso de que se hagan varias llamadas a pausar el scroll, esto asegura que no se retoma hasta que caduquen todas las pausas
    private int pauseCounter;

    private List<DeliveryHazard> branches;

    void Start()
    {
        branches = new List<DeliveryHazard>();
        remainingTime = duration;
        spawner.manager = this;
        trunk.scrollSpeed = scrollSpeed / trunk.GetComponent<SpriteRenderer>().bounds.size.y;
        scrolling = true;
        playerCol = player.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (scrolling)
            remainingTime -= Time.deltaTime;
        if (remainingTime < 0f)
        {
            Debug.Log("Delivery Complete");
        }
    }

    public void AddBranch(DeliveryHazard branch)
    {
        if (branch != null) branches.Add(branch);
    }
    public void RemoveBranch(DeliveryHazard branch)
    {
        if (branch != null) branches?.Remove(branch);
    }

    public void pauseScroll(float seconds)
    {
        ++pauseCounter;
        scrolling = false;
        trunk.scrolling = false;
        playerCol.enabled = false;
        player.stunned = true;
        StartCoroutine(restoreScroll(seconds));
    }

    IEnumerator restoreScroll(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (--pauseCounter <= 0)
        {
            scrolling = true;
            trunk.scrolling = true;

            player.stunned = false;
            //Esperamos un poquito más para rehabilitar la colisión del jugador
            yield return new WaitForSeconds(playerImmuneTime);
            playerCol.enabled = true;
        }
    }
}
