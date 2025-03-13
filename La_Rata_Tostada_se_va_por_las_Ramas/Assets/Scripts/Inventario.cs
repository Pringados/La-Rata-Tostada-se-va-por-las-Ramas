using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    // Start is called before the first frame update
    List<Mensaje> mensajes;
    int mensajesMáximos;
    UIManager UI;
    GameManager gManager;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento
    }
    void Start()
    {
        gManager = gameObject.GetComponent<GameManager>();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("INVENTARIO ACTUALIZADO");
        UI = UIManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
