using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoyaleGame : MiniGame
{
    //List of spawn points
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject playerPrefab;


    private void StartGame()
    {
        //Spawn Players
        
        if(spawnPoints != null && playerPrefab != null && playerList != null)
        {
            string[] players = playerList.GetJoinedUsernames().ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                GameObject player = Instantiate(playerPrefab);
                player.transform.position = spawnPoints[i].position;
                BRPlayer playerData = player.GetComponent<BRPlayer>();
                if (playerData != null)
                {
                    playerData.playerName = players[i];
                }
            }
        }
    }

    //Check that there are enough spawn points for the number of players
    private void OnValidate()
    {
        if (spawnPoints != null && settings != null)
        {
            if (spawnPoints.Length < settings.maxPlayerCount)
            {
                Debug.LogError("There are too few spawn points assigned for this game");
            }
        }
    }

    #region Event Subs/UnSubs
    private void OnEnable()
    {
        GameStart += StartGame;
    }
    private void OnDisable()
    {
        GameStart -= StartGame;
    }

    #endregion
}
