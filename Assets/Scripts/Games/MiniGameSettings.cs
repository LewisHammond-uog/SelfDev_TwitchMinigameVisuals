using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable objects for the settings of a minigame
/// </summary>
[CreateAssetMenu(fileName = "MG Settings", menuName = "ScriptableObjects/Minigame Settings")]
public class MiniGameSettings : ScriptableObject
{
    //Name
    public string gameName = "Untitled Minigame";

    //Wait Timer
    public float maxWaitToStartTime = 60; //Max time from 1st player join til game starts
    
    //Player Count
    [Min(0)]
    public int minPlayerCount = 1; //Min number of players for the game to start
    public int maxPlayerCount = 12; //Max number of players allowed

    private void OnValidate()
    {
        //On validate make sure that max > min
        maxPlayerCount = maxPlayerCount < minPlayerCount ? minPlayerCount : maxPlayerCount;
    }
}
