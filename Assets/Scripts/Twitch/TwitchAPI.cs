using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using UnityEngine;

public class TwitchAPI : Singleton<TwitchAPI>
{

    //Twitch Lib API Object
    public Api api { private set; get; }

    private void Start()
    {
        api = new Api();
        api.Settings.AccessToken = TwitchAuthInfo.bot_access_token;
        api.Settings.ClientId = TwitchAuthInfo.client_id;
    }

    
}
