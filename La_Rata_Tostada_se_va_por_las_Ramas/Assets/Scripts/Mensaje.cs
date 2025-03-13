using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Esto es básicamente un contenedor de datos del mensaje y poco más
public struct Mensaje 
{

    int destinatario;
    int color;
    float tiempoEntreEstados;
    int nEstados;
    int estadoActual; float tActual;
    Image UIrepresentation;
    Inventario inventario;
    bool destroyed;

    public void setAtributos(int dest, int col, int tEntreEstados, int nEstados)
    {
        destinatario = dest;
        color = col;
        tiempoEntreEstados = tEntreEstados;
        estadoActual = 0;
        destroyed = true;
    }

    public void setInventario(Inventario i)
    {
        inventario = i;
    }
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
