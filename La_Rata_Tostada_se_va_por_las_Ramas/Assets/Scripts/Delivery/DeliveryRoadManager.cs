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
    ImgScroll bgBranches;

    [SerializeField]
    ImgScroll galaxy;

    [SerializeField]
    FollowMouse player;

    [SerializeField]
    GradientTransition gradient;

    BoxCollider2D playerCol;
    Animator playerAnim;

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
        bgBranches.scrollSpeed = trunk.scrollSpeed * 0.7f;
        galaxy.scrollSpeed = trunk.scrollSpeed * 0.2f;
        scrolling = true;
        playerCol = player.GetComponent<BoxCollider2D>();
        playerAnim = player.GetComponent<Animator>();

        player.transform.position = new Vector3(0f, -15f, 0f);
        LeanTween.moveY(player.gameObject, -2.65f, 2f);
    }

    void Update()
    {
        if (scrolling)
            remainingTime -= Time.deltaTime;

        if (remainingTime < 0f && !GameManager.instance.timerPaused)
        {
            GameManager.instance.timerPaused = true;
            gradient.LevelEnd();
            Debug.Log("Delivery Complete");
            playerCol.enabled = false;
            LeanTween.moveY(player.gameObject, 17f, 2f);
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
        bgBranches.scrolling = false;
        galaxy.scrolling = false;
        playerCol.enabled = false;
        playerAnim.enabled = false;
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
            bgBranches.scrolling = true;
            galaxy.scrolling = true;

            player.stunned = false;
            playerAnim.enabled = true;
            //Esperamos un poquito más para rehabilitar la colisión del jugador
            yield return new WaitForSeconds(playerImmuneTime);
            playerCol.enabled = true;
        }
    }
}
