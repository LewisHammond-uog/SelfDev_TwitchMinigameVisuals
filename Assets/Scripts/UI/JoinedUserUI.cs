using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class JoinedUserUI : MonoBehaviour
{
    //Minigame text to display on
    [SerializeField] private TextMeshProUGUI joinText, namesText;

    //Link to the minigame that we showing UI for
    [SerializeField] private MiniGame activeGame;

    private void Start()
    {
        //Trigger player list to update when the player collector updates
        if (activeGame != null)
        {
            if (activeGame.playerList != null)
            {
                activeGame.playerList.PlayerJoin += AddNewPlayerToList;
                activeGame.GameInit += InitUI;
            }
        }

        //On Start hide the UI
        joinText.enabled = false;
        namesText.enabled = false;
    }

    /// <summary>
    /// Initalises the UI
    /// </summary>
    private void InitUI()
    {
        if (namesText != null)
        {
            //Get a list of the current players when UI starts
            RefreshList();

            //Enable UI
            joinText.enabled = true;
            namesText.enabled = true;
        }
    }


    private void OnDisable()
    {
        if (activeGame != null)
        {
            if (activeGame.playerList != null)
            {
                activeGame.playerList.PlayerJoin -= AddNewPlayerToList;
                activeGame.GameInit -= InitUI;
            }
        }
    }

    /// <summary>
    /// Updates the player list with all the players that we have joined
    /// </summary>
    private void AddNewPlayerToList(string playerName)
    {
        if(namesText != null)
        {
            namesText.text += "\n" + playerName;
        }
    }

    /// <summary>
    /// Refreshes the list of names. Abandons the old list and gets 
    /// a new list of players from the player collector
    /// </summary>
    public void RefreshList()
    {
        //Clear the name list
        namesText.text = string.Empty;

        //Check mini game is valid
        if(activeGame == null)
        {
            return;
        }

        //Check that player list is valid
        if(activeGame.playerList == null)
        {
            return;
        }

        //Get the player names
        string[] names = activeGame.playerList.GetJoinedUsernames().ToArray();

        //Loop through the names and add a new line between them
        for(int i = 0; i < names.Length; ++i)
        {
            AddNewPlayerToList(names[i]);
        }
    }


}
