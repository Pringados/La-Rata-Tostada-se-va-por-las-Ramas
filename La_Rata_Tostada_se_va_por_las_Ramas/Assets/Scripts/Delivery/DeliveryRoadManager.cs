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
    GameObject stars;

    [SerializeField]
    GradientTransition gradient;


    BoxCollider2D playerCol;
    Animator playerAnim;

    public bool scrolling;

    // En el caso de que se hagan varias llamadas a pausar el scroll, esto asegura que no se retoma hasta que caduquen todas las pausas
    private int pauseCounter;

    private List<DeliveryHazard> branches;

    private bool map;

    void Start()
    {
        map = true;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!map)
        {

            if (scrolling)
                remainingTime -= Time.deltaTime;

            if (remainingTime < 0f && !GameManager.instance.timerPaused)
            {
                GameManager.instance.timerPaused = true;
                gradient.LevelEnd();
                Debug.Log("Road Complete");
                playerCol.enabled = false;
                LeanTween.moveY(player.gameObject, 17f, 2f);
                GameManager.instance.ChangeScene(MapManager.instance.getDestino());
            }
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
        stars.SetActive(true);
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
            stars.SetActive(false);
            playerAnim.enabled = true;
            //Esperamos un poquito más para rehabilitar la colisión del jugador
            yield return new WaitForSeconds(playerImmuneTime);
            playerCol.enabled = true;
        }
    }

    private void StartLevel()
    {
        playerCol.enabled = true;
        spawner.enabled = true;
    }

    public void initialize()
    {
        branches = new List<DeliveryHazard>();
        duration = MapManager.instance.getDistance() *2 +5;
        remainingTime = duration;
        spawner.manager = this;
        trunk.scrollSpeed = scrollSpeed / trunk.GetComponent<SpriteRenderer>().bounds.size.y;
        bgBranches.scrollSpeed = trunk.scrollSpeed * 0.4f;
        galaxy.scrollSpeed = trunk.scrollSpeed * 0.2f;
        scrolling = true;
        playerCol = player.GetComponent<BoxCollider2D>();
        playerAnim = player.GetComponent<Animator>();
        stars.SetActive(false);
        Time.timeScale = 1f;
        spawner.StartSpawning();
        player.unStun();
        player.transform.position = new Vector3(0f, -15f, 0f);
        LeanTween.moveY(player.gameObject, -2.65f, 2f);
        Invoke("StartLevel", 1.5f);
        map = false;
    }
}
