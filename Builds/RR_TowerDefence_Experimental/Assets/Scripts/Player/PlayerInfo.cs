using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

namespace TDGame {
    public class TDPlayer : iPlayerInfo
    {
        #region Core Player Info
        private string _name = "Local Player";
        private CSteamID _SteamUID;
        #endregion


        #region PlayerInfo Interface
        public string Name { get { return _name; } private set { _name = value; } }
        public CSteamID SteamUID { get { return _SteamUID; } private set { _SteamUID = value; } }

        public void PlayerInfoUpdate()
        {
            _name = SteamFriends.GetPersonaName();
            _SteamUID = SteamUser.GetSteamID();

        }
        #endregion

        public TDPlayer(bool isSteamOnline)
        {
            if (isSteamOnline)
            {
                PlayerInfoUpdate();
            }
            else
            {

            }
        }
    }
}
