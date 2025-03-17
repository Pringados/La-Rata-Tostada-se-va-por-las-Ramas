using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHazard : MonoBehaviour
{
    Run_Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Run_Player>();
        if (player != null)
            player.Reset();
    }
}
