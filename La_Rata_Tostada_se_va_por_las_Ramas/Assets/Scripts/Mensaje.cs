using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UIElements;

//Esto es básicamente un contenedor de datos del mensaje y poco más
public class Mensaje 
{

    int destinatario;
    Color color;
    float tiempoEntreEstados;
    int nEstados;
    int estadoActual; float tActual;
    GameObject UIrepresentation;
    bool destroyed;

    public void setAtributos(GameObject im, int dest, int tEntreEstados, int nEstados)
    {
        destinatario = dest;
        tiempoEntreEstados = tEntreEstados;
        UIrepresentation = im;
        color = im.GetComponent<Image>().tintColor;
        estadoActual = 0;
        destroyed = false;
    }
    public bool isDestroyed() { return destroyed; }
    public void updateEstado()
    {
        tActual += Time.deltaTime;
        if (tActual >= tiempoEntreEstados)
        {
            estadoActual++;
            destroyed = true;
        }
    }
}
