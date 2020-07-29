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
    public static string client_id = "kv3dpei0jwikntf7qhzl3wt0bfpgqs";
    public static string client_secret = "wampndgfg2wdeozy239qxujhkvc9yu";
    public static string bot_access_token = "o9hmepxei6gy776yqfbpcy7gb91npp";
    public static string bot_refresh_token = "5rohzkjqlm3mierplon0hnp75e4w3ugtr39tc5bt6i0cir414x";
}
