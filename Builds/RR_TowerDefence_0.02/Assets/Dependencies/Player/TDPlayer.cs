using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using Placement;
using System.Xml.Serialization;
using UnityEngine.SocialPlatforms;


[SerializableAttribute]
public class TDPlayer : MonoBehaviour
{
    //Dependencies


    //offline/online variables


    //ingame dependencies to be set
    private TDMasterGrid GameGridMaster; 
    private TDMasterGrid.LiveGrid PlayerGrid
    {
        get
        {
            if (InGame)
            {
                if (IsOnline)
                {
                    return GameGridMaster.GetPlayerGrid(_LocalPlayerNumber);
                }
                else
                {
                    return GameGridMaster.OfflineGrid;
                }
            }
            else
            {
                throw new Exception("User has not been initiated into the game. Please 'InitGame()'");
            }
        }
    }
    private int _LocalPlayerNumber = 1;
    private bool InGame = false;



    public TDMasterGrid.LiveGrid GetMapGrid() { 
        if (InGame)
        {  return GameGridMaster.GetMapGrid(); }
        else
        { throw new Exception("User has not been initiated into the game. Please 'InitGame()'"); }
    }
    public TDMasterGrid.LiveGrid GetPlayerGrid() { return PlayerGrid; }

    [SerializableAttribute]
    public class steamInfo
    {
        //where player information is stored/updated/retrieved
        private string _username;
        private string _status;
        private CSteamID _UID;
        private bool _onlineState;
        private int _onlineFriendCount;
        private Dictionary<int, CSteamID> _onlineFriendIDs;
        private int _recentPlayerCount;
        private Dictionary<int, CSteamID> _recentPlayerSIDs;


        public void UpdateSteamData()
        {
            if (!_onlineState)
            {
                if (SteamAPI.Init())
                {
                    _recentPlayerCount = SteamFriends.GetCoplayFriendCount();
                    _username = SteamFriends.GetPersonaName();
                    _status = SteamFriends.GetPersonaState().ToString();
                    _onlineFriendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
                    //need to instatiate ID Dictionaries
                }
                else
                {
                    //Break out of thread and stop program;  Will need a loophole for offline play
                    throw new Exception("Live SteamInfo not available. Steam is not online.");
                }
            }

        }
        
        public bool IsSteamReady() { return _onlineState; }

        public string GetUserName() { UpdateSteamData(); return _username; }
        public string GetSteamStatus() { UpdateSteamData(); return _status; }
        public CSteamID GetSteamUID() { UpdateSteamData(); return _UID; }
        public int GetSteamOnlineFriendCount() { UpdateSteamData(); return _onlineFriendCount; }
        public int GetRecentPlayerCount() { UpdateSteamData(); return _recentPlayerCount; }
        public Dictionary<int, CSteamID> GetRecentPlayerIDs() { UpdateSteamData(); return _recentPlayerSIDs; }

    }




    public enum PlayerState
    {
        Online,
        Offline
    }
    private PlayerState _PS = PlayerState.Offline;

    public bool IsOnline { get { if(_PS == PlayerState.Online) { return true; } else { return false; } } }


    public TDPlayer InitGame(TDMasterGrid MasterGrid, int PlayerNumber, CSteamID PlayerUID)
    {
        GameGridMaster = MasterGrid;
        _LocalPlayerNumber = PlayerNumber;
        InGame = true;

        return this;
    }

    public TDPlayer UnloadGame()
    {
        GameGridMaster = null;
        InGame = false;

        return this;
    }
}
