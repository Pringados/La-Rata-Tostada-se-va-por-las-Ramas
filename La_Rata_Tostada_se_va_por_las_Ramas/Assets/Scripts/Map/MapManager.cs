using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    public static MapManager instance;
    
    // Player
    [SerializeField] public GameObject playerNode;
    private int playerPosition;

    // Recogidas
    [SerializeField] public GameObject pickUpNode;

    // Entregas
    [SerializeField] public GameObject deliveryNode;

    // Nodos libres para spawnear
    public HashSet<int> freeNodes = new HashSet<int>();

    public GameObject squirrel;
    public List<GameObject> pickUpList;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        playerPosition = 10;

        // Inicia la lista de nodos libres
        initFreeNodes();
    }

    void Start()
    {
        // Posiciona al jugador en el nodo inicial
        placePlayer(playerPosition);

        // Generar nuevos nodes,  Spawnea puntos de recogida
        placePickUpNodes();

        // placeDeliveryNodes();

    }

    void initFreeNodes() {

        for (int i = 0; i < 18; i++) { 
            freeNodes.Add(i);
        }
    }

    void placePickUpNodes() {
        int n = Random.Range(1, 4);
        for (int i = 0; i < n; i++) {

            if (freeNodes.Count > 0) { 
                // Get free node
                int j = Random.Range(0, 18);
                while (!freeNodes.Contains(j)) { 
                    j = Random.Range(0, 18);
                }

                // Spawn pickup
                GameObject recogida = Instantiate(pickUpNode, this.transform);
                pickUpList.Add(recogida);
                pickUpList[pickUpList.Count -1].GetComponent<MapNode>().SetNode(j);

                int x = GetComponentInChildren<Graph>().getXNode(j);
                int y = GetComponentInChildren<Graph>().getYNode(j);
                recogida.transform.localPosition = new Vector3(x, y, 0);
                recogida.GetComponent<Button>().onClick.AddListener(delegate{Pulsado(recogida);});

                // Remove free node
                freeNodes.Remove(j);
            }

        }
    }

    void placeDeliveryNodes() {
        // int x = GetComponentInChildren<Graph>().getXNode(deliveryNode);
        // int y = GetComponentInChildren<Graph>().getYNode(deliveryNode);


    }

    void placePlayer(int pn) {
        int x = GetComponentInChildren<Graph>().getXNode(pn);
        int y = GetComponentInChildren<Graph>().getYNode(pn);

        if(squirrel == null) {
            squirrel = Instantiate(playerNode, this.transform);
            squirrel.GetComponent<MapNode>().SetNode(pn);
        }
        squirrel.transform.localPosition = new Vector3(x, y, 0);
        squirrel.GetComponent<MapNode>().SetNode(pn);

        freeNodes.Remove(pn);
    }

    public void updatePlayerMapPosition(int n)
    {
        freeNodes.Add(playerPosition);
        playerPosition = n;
    }

    public void Pulsado(GameObject destiny)
    {
        updatePlayerMapPosition(destiny.GetComponent<MapNode>().GetNode());
        placePlayer(playerPosition);
        Destroy(destiny);

    }

}
