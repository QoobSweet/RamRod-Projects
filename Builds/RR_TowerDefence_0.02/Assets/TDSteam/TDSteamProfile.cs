using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SocialPlatforms.Impl;

public static class TDLocalProfile
{
    //profile page stats

    private static string Nickname;

    public static string LocalUserName { get { return SteamInfo.GetDisplayName; } }
    public static string LocalNickname { get { return Nickname; } }
    public static CSteamID LocalSteamID { get { return SteamInfo.GetSteamID; } }

    public static class SteamInfo
    {
        public static string GetDisplayName { get { return SteamFriends.GetPersonaName(); } }
        public static CSteamID GetSteamID { get { return SteamUser.GetSteamID(); } }
    }

    //Cached Profile for export or saving
    private static TDProfile _profileSnapshot = new TDProfile(LocalUserName, LocalNickname, LocalSteamID);
    public static TDProfile ProfileSnapshot { get { return _profileSnapshot; } set { _profileSnapshot = value; } }


    public static void SetLocalNickname(string Nickname)
    {
        TDLocalProfile.Nickname = Nickname;
    }
    public class TDProfile : TDSteamUtils._ProfileAPI
    {
        private string _username;
        private string _nickname;
        private CSteamID _steamID;
        public string Username { get { return _username; } }
        public string Nickname { get { return _nickname; } set { _nickname = value; } }
        public CSteamID SteamID { get { return _steamID; } }

        public TDProfile(string Username, string Nickname, CSteamID SteamID)
        {
            this._username = Username;
            this._nickname = Nickname;
            this._steamID = SteamID;
        }

        public static void SyncLocal()
        {
            ProfileSnapshot._username = LocalUserName;
            ProfileSnapshot._nickname = LocalNickname;
            ProfileSnapshot._steamID = LocalSteamID;
        }

    }
}