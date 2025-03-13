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
    [SerializeField]
    int nEstados;
    [SerializeField]
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
        Debug.Log("INVENTARIO PILLA EL UI MANAGER");
        UI = UIManager.Instance;
    }

    public Mensaje addMensaje(int destinatario)
    {
        GameObject im = UI.getFreeLetterSpace();
        if (im == null)
        {
            Debug.LogError("esto no debería pasar nunca Eduardo por favor detente");
        }
        else
        {
            Mensaje men = new Mensaje();
            men.setAtributos(im, destinatario, nTiempoEntreEstados, nEstados);
            return men;
        }

        return null;

    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mensajes.Count; i++)

        {
            if (mensajes[i] != null)
            {
                mensajes[i].updateEstado();
                if (mensajes[i].isDestroyed())
                {
                    //comprobación de q está yendo el jugador
                    mensajes[i] = null;

                }
            }
        }
    }
}
