using FMOD;
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
    [SerializeField] public List<GameObject> deliveryNodes;

    [SerializeField] public List<GameObject> bonusNodes;

    public DeliveryRoadManager deliveryRoadManager;

    [SerializeField] public List<NPCData> NPCs;
    NPCData currNPC;
    [SerializeField] public GameObject NPCPrefab;

    // Nodos libres para spawnear
    public HashSet<int> freeNodes = new HashSet<int>();

    public GameObject squirrel;
    public List<GameObject> pickUpList;
    private bool blocks = false;

    private int distance;
    private string destino;
    private bool init = false;

    static int counter = 0;

    private int lastNPC;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            playerPosition = 10;
            init = true;
            // Inicia la lista de nodos libres
            initFreeNodes();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        GameManager.instance.mapManager = this;
        // Posiciona al jugador en el nodo inicial
        placePlayer(playerPosition);
    }

    private void OnEnable()
    {
        //GameManager.instance.SetMusicAction(false);
    }

    void initFreeNodes() {

        for (int i = 0; i < 18; i++) { 
            freeNodes.Add(i);
        }
    }

    public void placePickUpNodes() {
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
                recogida.GetComponent<MapNode>().SetNode(j);

                float x = GetComponentInChildren<Graph>().getXNode(j);
                float y = GetComponentInChildren<Graph>().getYNode(j);
                recogida.transform.localPosition = new Vector3(x, y, 0);
                recogida.GetComponent<Button>().onClick.AddListener(delegate{Pulsado(recogida);});
                if (blocks)
                {
                    recogida.GetComponent<Button>().enabled = false;
                }

                // Remove free node
                freeNodes.Remove(j);
            }

        }
    }

    public void placeDeliveryNodes(int nSobre) {
        if (freeNodes.Count > 0)
        {
            // Get free node
            int j = Random.Range(0, 18);
            while (!freeNodes.Contains(j))
            {
                j = Random.Range(0, 18);
            }

            // Spawn pickup
            GameObject delivery = Instantiate(deliveryNodes[nSobre], this.transform);
            delivery.GetComponent<MapNode>().SetNode(j);

            float x = GetComponentInChildren<Graph>().getXNode(j);
            float y = GetComponentInChildren<Graph>().getYNode(j);
            delivery.transform.localPosition = new Vector3(x, y, 0);
            delivery.GetComponent<Button>().onClick.AddListener(delegate { Pulsado(delivery); });

            // Remove free node
            freeNodes.Remove(j);
        }

    }

    public void placeBonusNodes(int id)
    {
        if (freeNodes.Count > 0)
        {
            // Get free node
            int j = Random.Range(0, 18);
            while (!freeNodes.Contains(j))
            {
                j = Random.Range(0, 18);
            }

            // Spawn pickup
            GameObject bonus = Instantiate(bonusNodes[id], this.transform);
            bonus.GetComponent<MapNode>().SetNode(j);

            float x = GetComponentInChildren<Graph>().getXNode(j);
            float y = GetComponentInChildren<Graph>().getYNode(j);
            bonus.transform.localPosition = new Vector3(x, y, 0);
            bonus.GetComponent<Button>().onClick.AddListener(delegate { Pulsado(bonus); });

            // Remove free node
            freeNodes.Remove(j);
        }
    }

    void placePlayer(int pn) {
        float x = GetComponentInChildren<Graph>().getXNode(pn);
        float y = GetComponentInChildren<Graph>().getYNode(pn);

        if (squirrel == null) {
            squirrel = Instantiate(playerNode, this.transform);
            squirrel.GetComponent<MapNode>().SetNode(pn);
        }
        squirrel.transform.localPosition = new Vector3(x, y, 0);
        squirrel.GetComponent<MapNode>().SetNode(pn);

        freeNodes.Remove(pn);

        //Esto son las cosas que tienen que pasar cuando se abre el mapa
        placePickUpNodes();
        if (Random.Range(0, 4) == 3)
        {
            placeBonusNodes(Random.Range(0, 4));
        }
        checkNodes();
    }

    public void updatePlayerMapPosition(int n)
    {
        freeNodes.Add(playerPosition);
        playerPosition = n;
    }

    public void Pulsado(GameObject destiny)
    {
        //if(destiny.GetComponent<Delivery>() != null) {
        //    GameManager.instance.GetComponent<Inventario>().protectMensaje(destiny.GetComponent<Delivery>().GetId());
        //    destino = GameManager.instance.GetComponent<Inventario>().GetMensaje(destiny.GetComponent<Delivery>().GetId()).getDestino().sceneName;
        //}
        //else
        if(destiny.GetComponent<Bonus>() != null)
        {
            int id = destiny.GetComponent<Bonus>().GetId();

            switch (id)
            {
                case 0:
                    destino = "Vending_Machine_Scene";
                    break;
                case 1:
                    destino = "Captcha";
                    break;
                case 2:
                    destino = "Reloj";
                    break;
                //case 3:
                //    destino = "Letter";
                //    break;
            }
        }
        else
        {
            int n = Random.Range(0, NPCs.Count);
            destino = NPCs[n].sceneName;
            currNPC = NPCs[n];
            lastNPC = n;
        }

        distance = GetComponentInChildren<Graph>().distance2time(playerPosition, destiny.GetComponent<MapNode>().GetNode());
        UnityEngine.Debug.Log("Distancia: " + distance);
        updatePlayerMapPosition(destiny.GetComponent<MapNode>().GetNode());
        placePlayer(playerPosition);
        deliveryRoadManager.initialize();
        Destroy(destiny);
        this.gameObject.SetActive(false);

    }

    public void checkNodes()
    {
        PickUp[] objects = GetComponentsInChildren<PickUp>();
        foreach (PickUp obj in objects) {
            if (obj.GetTime() > 0)
            {
                obj.SetTime(obj.GetTime() - 1);
            }
            else
            {
                UnityEngine.Debug.Log("Adios " + obj.GetNode());
                Destroy(obj.gameObject);
            }
        }

        Bonus[] objetos = GetComponentsInChildren<Bonus>();
        foreach (Bonus obj in objetos) { 
            Destroy(obj.gameObject);
        }
    }

    public void blockPickUps()
    {
        blocks = true;
        PickUp[] objects = GetComponentsInChildren<PickUp>();
        foreach (PickUp obj in objects)
        {
            obj.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public void unblockPickUps()
    {
        blocks = false;
        PickUp[] objects = GetComponentsInChildren<PickUp>();
        foreach (PickUp obj in objects)
        {
            obj.gameObject.GetComponent<Button>().enabled = true;
        }
    }

    public void destroyDelivery(int id)
    {
        Delivery[] objects = GetComponentsInChildren<Delivery>();
        foreach (Delivery obj in objects)
        {
            if(obj.gameObject.GetComponent<Delivery>().GetId() == id)
            {
                Destroy(obj.gameObject);
                break;
            }
        }
    }

    public int getDistance()
    {
        return distance;
    }

    public string getDestino()
    {
        return destino;
    }
    public NPCData getNPCData()
    {
        return currNPC;
    }

    public void setDelivery()
    {
        int n = Random.Range(0, NPCs.Count);
        if(n == lastNPC)
        {
            n = (n + 1) % NPCs.Count;
        }

        //GameManager.instance.GetComponent<Inventario>().addMensaje(NPCs[n]);
    }

}
