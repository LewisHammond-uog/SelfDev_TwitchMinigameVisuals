using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Api.Models.v5.Users;
using UnityEngine;

/// <summary>
/// Struct to represent a twitch user with a username and userID
/// </summary>
public struct TwitchUser
{
    public string username { private set; get; } //Username of the user (can change if the user changes their name)
    public string userID { private set; get; } //Internal Twitch ID of the user (does not change)
    

    /// <summary>
    /// Create a Twitch user from their username
    /// </summary>
    /// <param name="createUserID">User's ID</param>
    public TwitchUser(string createUserID, string createUsername)
    {
        //Set the username and trigger and API lookup for the user ID
        userID = createUserID;
        username = createUsername;

        Debug.Log("Created User " + createUsername);
    }
}
