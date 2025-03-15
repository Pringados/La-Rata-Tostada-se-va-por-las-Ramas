using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EmptyData", menuName = "ScriptableObjects/MinigameData", order = 1)]

public class MinigameData : ScriptableObject
{
    public enum MinigameType { Pickup, Delivery};

    public MinigameType minigameType;

    public string textHint;

    public Sprite character;

    public Sprite hint;
}
