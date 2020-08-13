using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BRPlayer : MonoBehaviour
{

    //NavMesh Agent
    [SerializeField]
    private NavMeshAgent navigator;

    [HideInInspector]
    public string playerName; //todo fix this
    public Text uiText;

    [SerializeField]
    private float commandMoveAmount = 25f; //Amount to move when a command is entered

    private Vector3 targetPos;

    //Timeouts for commands (i.e the time you have to wait before sending the next command)
    [SerializeField]
    private float moveTO, lightAttackTO = 3f;

    //Prefab for player attack
    [SerializeField]
    private GameObject attackPrefab;

    private float health = 100f;

    //Attack Commands
    private string attackCommand = "attack";

    //List of commands and directions that they shoud go
    //Command, Direction
    private Dictionary<string, Vector3> commandDirectionPairs = new Dictionary<string, Vector3>()
    {
        { "left", Vector3.left },
        { "right", Vector3.right },
        { "forward", Vector3.forward },
        { "back", Vector3.back },
        { "up", Vector3.forward },
        { "down", Vector3.back }
    };


    private void Start()
    {
        //Set target pos to current pos
        targetPos = transform.position;
        navigator.SetDestination(targetPos);
    }

    private void Update()
    {
        uiText.text = playerName;
    }

    /// <summary>
    /// Process player chat commands
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProcessPlayerCommands(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        //If the commands are not for this player then about
        if(e.Command.ChatMessage.Username != playerName)
        {
            return;
        }


        string commandText = e.Command.CommandText;
        if (commandDirectionPairs.ContainsKey(commandText))
        {
            ProcessPlayerMovement(commandText);
        }else if(commandText == attackCommand)
        {
            //Attack all players within range
            CreateAttack();
        }
    }

    /// <summary>
    /// Does an amout of damage to the player
    /// </summary>
    /// <param name="damageAmount"></param>
    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("DAMAGED! For Debuggining I'm now going to delete you!");
        Destroy(gameObject);
    }

    /// <summary>
    /// Create an attack that damages all players apart from us
    /// </summary>
    private void CreateAttack()
    {
        GameObject attackGO = Instantiate(attackPrefab);
        attackGO.transform.position = transform.position;

        //Get the attack script to set the creator
        BRAttack attack;
        if (attackGO.TryGetComponent(out attack))
        {
            attack.Creator = this;
        }
    }

    /// <summary>
    /// Proccess player movement from a chat command
    /// </summary>
    /// <param name="movementCommandText"></param>
    private void ProcessPlayerMovement(string movementCommandText)
    {
        //Set target to be the direction assosciated with the command * by the amount to move
        targetPos += (commandDirectionPairs[movementCommandText] * commandMoveAmount);
        navigator.SetDestination(targetPos);
    }

    private void OnEnable()
    {
        if (TwitchChatClient.Instance.chatClient != null)
        {
            TwitchChatClient.Instance.chatClient.OnChatCommandReceived += ProcessPlayerCommands;
        }
    }

    private void OnDisable()
    {
        if (TwitchChatClient.Instance.chatClient != null)
        {
            TwitchChatClient.Instance.chatClient.OnChatCommandReceived -= ProcessPlayerCommands;
        }
    }
}
