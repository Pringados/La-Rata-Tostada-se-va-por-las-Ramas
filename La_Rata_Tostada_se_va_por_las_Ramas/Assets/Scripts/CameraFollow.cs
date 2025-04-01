using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //La velocidad de la transición
    float speed;
    //El transform destino. No puede ser un simple vector3 porque puede ser un objeto en movimiento,
    //conque el destino puede variar durante la trayectoria
    Vector3 dest;
    //La posición del hijo CameraPosition del jugador
    [SerializeField]
    GameObject playerPos;
    //La velocidad de la cámara cuando sigue al jugador
    [SerializeField]
    float playerFollowSpeed = 100;

    [SerializeField]
    float yPos;

    // Start is called before the first frame update
    void Start()
    {
        speed = playerFollowSpeed;
    }

    private void Update()
    {
        dest = new Vector3(playerPos.transform.position.x, yPos, -10f);
        float distance = Vector3.Distance(transform.position, dest);
        //Si no estamos en el objetivo, movemos (se usa < 0.01 en vez de == porque son floats)
        if (distance > 0.01f)
        {
            //Movemos hacia el destino. El parámetro float limita cuánto se puede mover por frame
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime * distance / 4);
        }
    }
}
