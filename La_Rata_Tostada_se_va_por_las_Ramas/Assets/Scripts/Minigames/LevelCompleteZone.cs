using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteZone : MonoBehaviour
{
    [SerializeField]
    IMinigame levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelManager.MinigameComplete(true);
    }
}
