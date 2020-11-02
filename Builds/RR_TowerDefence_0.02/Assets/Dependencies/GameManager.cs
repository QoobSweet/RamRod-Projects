using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;
using UnityEditor;
using Placement;

namespace TDGame
{
    public partial class GameManager : MonoBehaviour
    {
        //Manage GameState for Update Threading
        public enum GS { Loading, TitleScreen, Lobby, InGame }
        private GS _gameState = GS.Loading;
        public GS gameState { get { return _gameState; } private set { _gameState = value; } }


        //Network status handles if a online server is available to build player info needed this will return Online
        private bool _isOnline = false;
        public bool IsOnline { get { return _isOnline; } }
        private bool GameLoaded = false;
        public int MaxSessionPlayers = 8;
        public Material TempMapMaterial;
        public Camera LocalPlayerCam;
        private float timeout = 0;

        private CSteamID _pGameID;




        #region MB Accessors & Registers


        //MonoBehavior Accessors & Registers

        //Self
        private static GameManager _gameManager;
        public static GameManager gameManager { get { return _gameManager; } }

        //Steam
        private static SteamManager _SteamLayer;
        public static SteamManager steamLayer { get { return _SteamLayer; } }

        //MenuManager (Scene 1 Title_Menu) w/ Register
        private MenuManager menuManager;
        private bool menuManager_set = false;
        public MenuManager MenuManager
        {
            get
            {
                if (menuManager_set)
                {
                    return menuManager;
                }
                else { Debug.LogError("No MenuManager Assigned!"); }
                return null;
            }
        }
        public GameManager register(MenuManager menuManager)
        {
            this.menuManager = menuManager;
            return this;
        }

        #endregion




        //Still dont understand how this shit works.
        public event EventHandler OnSceneChange;

        public class OnSceneChangedEventArgs : EventArgs
        {
            int SceneNumber;
        }

        public void g_SceneChanged(EventArgs e)
        {
            EventHandler handler = OnSceneChange;
            handler?.Invoke(this, e);
        }
        



        void Start()
        {
            UnityEngine.Object.DontDestroyOnLoad(this.gameObject); //Set Manager to not be destroyed when switching scenes   
            _gameManager = this;  //Set self to be globally accessible via "GameManager.gameManager"

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Run Scene Update
            if (gameState == GS.Loading)     { UpdateLoading();      }
            if (gameState == GS.TitleScreen) { UpdateTitleScreen();  }
            if (gameState == GS.Lobby)       { UpdateLobbyScreen();  }
            if (gameState == GS.InGame)      { UpdateInGame();       }

        }

        //When Exiting Game Shutdown the Steam API First!
        void OnApplicationQuit()
        {
            if (_isOnline)
            {
                _isOnline = false;
                SteamAPI.Shutdown();
            }
        }

    }
}
