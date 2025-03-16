using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EmptyData", menuName = "ScriptableObjects/MinigameData", order = 1)]

public class MinigameData : ScriptableObject
{
    public enum MinigameType { Pickup, Delivery, Bonus};

    public MinigameType minigameType;

    public string textHint;

    public bool help;

    public enum BonusType { None, Time, Shield, Speed };

    public BonusType bonusType;

}
