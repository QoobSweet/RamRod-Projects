using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;
using UnityEditor;


namespace TDGame
{
    public partial class GameManager
    {
        private void UpdateLoading()
        {
            //Find and set globally accessible SteamLayer via "GameManager.GetSteamLayer"
            while (_SteamLayer == null && timeout < 10)
            {
                _SteamLayer = GameObject.Find("SteamLayer").GetComponent<SteamManager>();
                timeout += Time.deltaTime;
            }
            if (_SteamLayer == null) { throw new Exception("Timed Out. Steam Layer could not be found!"); }

            timeout = 0;


            //Check if Steam Is connected and live only if Offline
            if (!IsOnline && _SteamLayer != null)
            {

                //Test SteamAPI Connection
                bool _state = SteamAPI.Init();
                while (!_state && timeout < 10)
                {
                    Debug.Log((_state = SteamAPI.Init()) + " Steam State Result.");
                    timeout += Time.deltaTime;
                }

                //Check SteamAPI Status and adjust isOnline Accordingly
                if (_state)
                {
                    _isOnline = true;
                    _pGameID = SteamUser.GetSteamID();

                    Debug.Log("Steam is Connected with SteamID: " + _pGameID + "  - Starting in Online Mode.");
                }
                else
                {
                    _isOnline = false;
                    Debug.Log("Steam is not Connected. Starting in Offline Mode.");
                }
            }


            //If everything checks out good Start next scene
            if (_isOnline || !_isOnline)
            {
                if (!GameLoaded)
                {
                    SceneManager.LoadScene(1);
                    GameLoaded = true;
                    gameState = GameManager.GS.TitleScreen;
                }
            }
        }
    }
}