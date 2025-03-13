using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventario : MonoBehaviour
{
    // Start is called before the first frame update
    List<Mensaje> mensajes;
    int mensajesMáximos;
    int mensajesActuales;
    private UIManager UI;
    private GameManager gManager;
    int nEstados;
    int nTiempoEntreEstados;

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

    public void addMensaje(int destinatario)
    {
        Image im = UI.getFreeLetterSpace();
        if (im == null)
        {
            Debug.LogError("esto no debería pasar nunca Eduardo por favor detente");
        }
        else
        {
            Mensaje men = new Mensaje();
            men.setAtributos(im, destinatario, )
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
