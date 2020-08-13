using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BRPlayer : MonoBehaviour
{

    private string playerName;
    public string PlayerName {
        set { playerName = playerName == string.Empty ? value : playerName ; }
        get { return playerName; } 
    }
    public Text uiText;

    private void Update()
    {
        uiText.text = playerName;
    }

    private void ProcessMovement(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {

    }

    private void OnEnable() => TwitchChatClient.Instance.chatClient.OnChatCommandReceived += ProcessMovement;

    private void OnDisable()
    {
        
    }
}
