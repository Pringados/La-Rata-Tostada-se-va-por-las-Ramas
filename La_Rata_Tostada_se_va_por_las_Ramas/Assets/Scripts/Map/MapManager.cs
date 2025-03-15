using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        placePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void placePlayer() {
        int playerNode = GameManager.instance.getPlayerMapPosition();
        int x = GetComponentInChildren<Graph>().getXNode(playerNode);
        int y = GetComponentInChildren<Graph>().getYNode(playerNode);

        GameObject squirrel = Instantiate(playerPrefab, this.transform);
        squirrel.transform.localPosition = new Vector3(x, y, 0);

    }
}
