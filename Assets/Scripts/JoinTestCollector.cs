using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Test for collecting a list of users that want to be in the game
public class JoinTestCollector : MonoBehaviour
{
    //List of users
                        //userID, Twitch User
    private Dictionary<string, TwitchUser> joinedUsers;


    private void Start()
    {
        //Initalise the user list
        joinedUsers = new Dictionary<string, TwitchUser>();

        //Subscribe to getting command notifications
        TwitchChatClient.Instance.chatClient.OnChatCommandReceived += ChatCommandReceived;
    }

    /// <summary>
    /// Function Triggered when we get a command from chat
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        //Check for join command
        if(e.Command.CommandText.ToLower() == "join")
        {
            //Get the UserID and Username from the command
            string incomingUserID = e.Command.ChatMessage.UserId;
            string incomingUsername = e.Command.ChatMessage.Username;

            //Add the user to our user list - if they are not already
            if (!joinedUsers.ContainsKey(incomingUserID)) {
                joinedUsers.Add(incomingUserID, new TwitchUser(incomingUserID, incomingUsername));

                //Tell the user that we have added them
                TwitchChatClient.Instance.SendChatMessageTargeted(incomingUsername, "You have been added to the game!");
            }
        }
    }
}
