using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store the secret keys used to connect to the Twitch API
/// </summary>
public static class TwitchAuthInfo
{
    //Not secret info (i.e bot name)
    public static string botName = "lewish_unity";

    //Secret info for connecting to twitch
    public static string client_id = "";
    public static string client_secret = "";
    public static string bot_access_token = "";
    public static string bot_refresh_token = "";
}
