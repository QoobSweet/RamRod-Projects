using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;


namespace TDGame
{
    public interface iGameManager
    {

        TDPlayer GetLocalPlayer();

        void register(OverlayManager overlayManager);
    }

    public interface iPlayerInfo
    {
        string Name { get; }
        CSteamID SteamUID { get; }


        void PlayerInfoUpdate();
    }
}

