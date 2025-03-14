using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventario : MonoBehaviour
{
    // Start is called before the first frame update
    List<Mensaje> mensajes = new List<Mensaje>();
    [SerializeField]
    int mensajesMáximos;
    int mensajesActuales = 0;
    private UIManager UI;
    private GameManager gManager;
    [SerializeField]
    int nEstados;
    [SerializeField]
    int nTiempoEntreEstados;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento
        Debug.Log("awakening");
    }
    void Start()
    {
        gManager = gameObject.GetComponent<GameManager>();

        mensajes.Add(new Mensaje());
        mensajes[0].setAtributos(0, 0, nTiempoEntreEstados, nEstados, this);
        UI.setInitialState(mensajes);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("INVENTARIO PILLA EL UI MANAGER");
        UI = UIManager.Instance;
        //UI.setInitialState(mensajes);
    }

    public Mensaje addMensaje(int destinatario)
    {
        int im = UI.getFreeLetterSpace();
        Debug.Log(im);
        if (im == -1)
        {
            Debug.LogError("esto no debería pasar nunca Eduardo por favor detente");
        }
        else
        {
            mensajesActuales++;
            Mensaje men = new Mensaje();
            men.setAtributos(im, destinatario, nTiempoEntreEstados, nEstados, this);
            mensajes.Add(men);
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
                    mensajes.RemoveAt(i);
                    mensajesActuales--;
                    UI.deleteLetter(i);

                }
            }
        }
    }

    public void changeUIstate(int id, int nextState)
    {
        UI.changeLetterState(id, nextState);

    }
}
