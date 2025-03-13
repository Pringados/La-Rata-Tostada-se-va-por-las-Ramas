using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EmptyData", menuName = "ScriptableObjects/MinigameData", order = 1)]

public class MinigameData : ScriptableObject
{
    public enum MinigameType { Pickup, Delivery};

    public MinigameType minigameType;
    public bool showHint = true;
}
