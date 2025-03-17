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
    //Controla si seguimos o no al jugador
    bool followPlayer = true;
    //La posición del hijo CameraPosition del jugador
    [SerializeField]
    GameObject playerPos;
    //La velocidad de la cámara cuando sigue al jugador
    [SerializeField]
    float playerFollowSpeed = 100;
    //Distancia al objetivo
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        speed = playerFollowSpeed;
    }
    private void Update()
    {
        if (followPlayer)
        {
            dest = new Vector3(playerPos.transform.position.x, 122, -10f);
        }
        distance = Vector3.Distance(transform.position, dest);
        //Si no estamos en el objetivo, movemos (se usa < 0.01 en vez de == porque son floats)
        if (!(distance < 0.01f))
        {
            //Movemos hacia el destino. El parámetro float limita cuánto se puede mover por frame
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime * distance / 4);
        }
    }

    //Este método sirve para ser llamado desde otras componentes e inicializar los datos de un desplazamiento nuevo
    public void MoveToAnchor(Vector3 destination, float spd)
    {
        speed = spd;
        dest = destination;
        followPlayer = false;
    }

    public void ReturnToPlayer()
    {
        speed = playerFollowSpeed;
        followPlayer = true;
    }
}
