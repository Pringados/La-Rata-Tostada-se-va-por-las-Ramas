using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UIElements;

enum coloresIndex
{
    red = 0, green, blue
}
//Esto es básicamente un contenedor de datos del mensaje y poco más
public class Mensaje 
{

    int destinatario;
    float tiempoEntreEstados;
    int nEstados;
    int estadoActual; float tActual;
    GameObject UIrepresentation;
    bool destroyed;
    Inventario i;
    coloresIndex col;

    public void setAtributos(int id, int dest, int tEntreEstados, int nE, Inventario inv)
    {
        destinatario = dest;
        tiempoEntreEstados = tEntreEstados;
        col = (coloresIndex)id;
        tActual = 0;
        estadoActual = 0;
        nEstados = nE;
        i = inv;
        destroyed = false;
    }
    public bool isDestroyed() { return destroyed; }
    public void updateEstado()
    {
        tActual += Time.deltaTime;
        if (tActual >= tiempoEntreEstados)
        {
            estadoActual++;
            
            if(estadoActual >= nEstados)
            {
                destroyed = true;
            }
            else
            {
                i.changeUIstate((int)col, estadoActual);
            }
            //Debug.Log("reseteando tiempo");
            tActual = 0;
            
        }
    }

    public int getEstado() { return estadoActual; }
    public int getID() { return (int) col; }
}
