
using UnityEngine;
using Steamworks;
using System.Runtime.InteropServices;

public class TDSteamUtils
{

	public interface _ProfileAPI
    {
        string Username { get;}
        string Nickname { get;}
        CSteamID SteamID { get;}
    }

}
