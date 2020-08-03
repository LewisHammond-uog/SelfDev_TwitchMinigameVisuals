using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class to run a minigame from
/// </summary>
public class MiniGame : MonoBehaviour
{
    //Enum for the game state
    public enum GAME_STATE
    {
        Unitialised,
        WaitingForUsers,
        Playing,
        Finished
    }
    private GAME_STATE gameState = GAME_STATE.Unitialised;

    //Player collector for letting users join
    public PlayerCollector playerList { get; private set; }

    //Timers for warmup etc.
    private float initTimer = 0f;

    //Settings
    [SerializeField]
    protected MiniGameSettings settings;

    private void Awake()
    {
        //Check that we have some settings
        if(settings == null)
        {
            Debug.LogError("Minigame has no settings. Destroying");
            Destroy(this);
        }


        //Create the player collector
        playerList = new PlayerCollector(settings.maxPlayerCount);

        //---REMOVE---///
        Invoke("InitGame", 1);
    }

    private void Update()
    {
        //Countdown start timer
        if(gameState == GAME_STATE.WaitingForUsers)
        {
            initTimer -= Time.unscaledDeltaTime;

            //If timer has passed then attempt to start the game
            if(initTimer <= 0f)
            {
                if (IsValidPlayerCount())
                {
                    StartGameRunning();
                }
                else
                {
                    //If there is an invalid player count end the game
                    TwitchChatClient.Instance.SendChatMessage("Cannot start minigame. Too few players");
                    Destroy(this);
                }
            }
        }
    }

    /// <summary>
    /// Initalise the gmae by allowing players to join
    /// </summary>
    protected void InitGame()
    {
        gameState = GAME_STATE.WaitingForUsers;

        //Send a chat message
        TwitchChatClient.Instance.SendChatMessage(settings.gameName + " is now open! " + settings.minPlayerCount + " - " + settings.maxPlayerCount + " players must join in the next " 
            + settings.maxWaitToStartTime + " seconds");

        //Allow users to join
        playerList?.SetAllowJoin(true);

        //Start countdown
        initTimer = settings.maxWaitToStartTime;
    }

    /// <summary>
    /// Start the game
    /// </summary>
    private void StartGameRunning()
    {
        gameState = GAME_STATE.Playing;

        //Send chat message
        TwitchChatClient.Instance.SendChatMessageTargeted(playerList.GetJoinedUsernames(), "The game is now starting!");
    }

    /// <summary>
    /// Checks the player count, if there is enough players
    /// </summary>
    private bool IsValidPlayerCount()
    {
        return playerList.GetPlayerCount() >= settings.minPlayerCount && playerList.GetPlayerCount() <= settings.maxPlayerCount;
    }


}
