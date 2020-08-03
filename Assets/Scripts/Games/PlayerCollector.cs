using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class for collecting players to join a game
/// </summary>
public class PlayerCollector
{
    //List of users
                     //userID, Twitch User
    public Dictionary<string, TwitchUser> joinedUsers { get; private set; }

    //Var for if we are allowing users to join
    private bool joinAllowed = false;

    //Command used to join the game without the !
    private string joinWord = "join";

    //Maximum number of players allowed
    private int maxPlayersInList = 0;

    //Events for player leaving joining
    public delegate void PlayerCollectEvent(string username);
    public event PlayerCollectEvent PlayerJoin;
    public event PlayerCollectEvent PlayerLeave;

    /// <summary>
    /// Constructor for creating the player collector
    /// </summary>
    public PlayerCollector(int maxPlayers = int.MaxValue)
    {
        //Initalise the user list
        joinedUsers = new Dictionary<string, TwitchUser>();

        //Subscribe to getting command notifications
        TwitchChatClient.Instance.chatClient.OnChatCommandReceived += ChatCommandReceived;

        //Set max players
        maxPlayersInList = maxPlayers;
    }

    /// <summary>
    /// Enables and disables if users can be added to this list by using "!join"
    /// </summary>
    /// <param name="enabled"></param>
    public void SetAllowJoin(bool enabled, string additionalReason = "")
    {
        joinAllowed = enabled;

        if (joinAllowed)
        {
            TwitchChatClient.Instance.SendChatMessage("You can now join the minigame. Join by using !" + joinWord + " " + additionalReason);
        }
        else
        {
            TwitchChatClient.Instance.SendChatMessage("You can no longer join the game! " + additionalReason);
        }
    }

    /// <summary>
    /// Gets a list of joined users 
    /// </summary>
    /// <returns></returns>
    public List<string> GetJoinedUsernames()
    {
        List<string> userlist = new List<string>(joinedUsers.Count);
        foreach(var name in joinedUsers)
        {
            userlist.Add(name.Value.username);
        }
        return userlist;
    }

    /// <summary>
    /// Gets the number of players connected
    /// </summary>
    /// <returns></returns>
    public int GetPlayerCount()
    {
        return joinedUsers.Count;
    }

    /// <summary>
    /// Function Triggered when we get a command from chat
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        //Get the UserID and Username from the command
        string incomingUserID = e.Command.ChatMessage.UserId;
        string incomingUsername = e.Command.ChatMessage.Username;
       
        //Check for join command
        if (e.Command.CommandText.ToLower() == joinWord)
        {
            HandlePlayerJoin(incomingUserID, incomingUsername);
        }
    }

    private void HandlePlayerJoin(string incomingUserID, string incomingUsername)
    {
        //If joining is not allowed then tell user and exit
        if (!joinAllowed)
        {
            TwitchChatClient.Instance.SendChatMessageTargeted(incomingUsername, "Joining the minigame is not currently allowed!");
            return;
        }

        //Add the user to our user list - if they are not already
        if (!joinedUsers.ContainsKey(incomingUserID))
        {
            TwitchUser user = new TwitchUser(incomingUserID, incomingUsername);
            joinedUsers.Add(incomingUserID, user);

            //Tell the user that we have added them
            TwitchChatClient.Instance.SendChatMessageTargeted(incomingUsername, "You have been added to the game!");
            PlayerJoin?.Invoke(incomingUsername);

            //We just added a new user, check if we have reached our max player count if we have then stop new joins
            if (joinedUsers.Count == maxPlayersInList)
            {
                SetAllowJoin(false, "Max players reached");
            }
        }
    }
}
