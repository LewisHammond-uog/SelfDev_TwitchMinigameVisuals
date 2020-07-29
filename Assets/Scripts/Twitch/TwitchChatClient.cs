using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Twitch Includes
using TwitchLib.Client.Models;
using TwitchLib.Unity;

/// <summary>
/// Class for a client that connects to chat
/// </summary>
public class TwitchChatClient : Singleton<TwitchChatClient>
{
    public Client chatClient { private set; get; } //TwitchLib Client that connects to chats
    private string channelName = "bubble"; //Channel to connect to. MUST BE LOWERCASE

    //Prefix for bot chat messages
    private const string chatMessagePrefix = "[MiniGames Bot]";

    //Event Triggered when we get a chat command
    public delegate void TwitchChatMessageEvent(string message);

    //Prevent non-singleton constructor use.
    protected TwitchChatClient() { }

    private void Awake()
    {
        //OnAwake create and login the chat bot
        //Set to run script in the background
        Application.runInBackground = true;

        //Set Bot Credentials
        ConnectionCredentials loginCreds = new ConnectionCredentials(TwitchAuthInfo.botName, TwitchAuthInfo.bot_access_token);
        chatClient = new Client();
        chatClient.Initialize(loginCreds, channelName);

        //Set events to listen to
        chatClient.OnConnected += ChatClient_OnConnected; //For sending a message when we connect
    }

    //OnEnable connect to chat
    void OnEnable()
    {
        //Connect to chat
        if (chatClient != null)
        {
            if (chatClient.IsInitialized && !chatClient.IsConnected)
            {
                chatClient.Connect();
            }
        }
    }

    //On disable disconnect from chat
    private void OnDisable()
    {
        if (chatClient != null)
        {
            if (chatClient.IsConnected)
            {
                //Send message that we are disconnecting
                SendChatMessage("Disconnecting from Chat!");

                //Disconnect from chat
                chatClient.Disconnect();
            }
        }
    }

    //OnDestroy remove from any events we have subscribed to
    private void OnDestroy()
    {
        if (chatClient != null)
        {
            chatClient.OnConnected -= ChatClient_OnConnected;
        }
    }

    /// <summary>
    /// Callback triggered when we connect to chat
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ChatClient_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
    {
        //Send a message to let the user know that we have connected
        SendChatMessage("Connected to Chat. If I'm not a mod please mod or VIP me so I can bypass spam limits!");
    }

    /// <summary>
    /// Send a chat message if we are connected to a chat
    /// </summary>
    /// <param name="message">Message to send</param>
    public void SendChatMessage(string message)
    {
        if (chatClient != null)
        {
            if (chatClient.IsConnected && chatClient.JoinedChannels.Count != 0)
            {
                chatClient.SendMessage(chatClient.JoinedChannels[0], chatMessagePrefix + " " + message);
            }
            else
            {
                Debug.LogWarning("Trying to send a chat message while not connected to a channel");
            }
        }
        else
        {
            Debug.LogError("Chat Client is Null");
        }
    }

    /// <summary>
    /// Sends a message that is targered (@user) in to chat
    /// </summary>
    public void SendChatMessageTargeted(string targetUsername, string message)
    {
        SendChatMessage("@" + targetUsername + " " + message);
    }
}
