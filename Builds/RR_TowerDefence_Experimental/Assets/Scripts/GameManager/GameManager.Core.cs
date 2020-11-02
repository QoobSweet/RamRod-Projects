using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;
using System.Runtime.Remoting.Messaging;

namespace TDGame
{
    public partial class GameManager : iGameManager
    {
        #region Globally Accessible Variables
        public static GameManager gameManager;
        #endregion

        // GameState Variables
        private bool _Loaded = false;


        // SubManagers Handles
        private OverlayManager _overlayManager;


        #region Scene Control Variables
        public float SplashScreenWaitTime = 3;

        #endregion

        #region Steam Connection Variables
        private bool _OfflineMode = false;
        private bool _SteamConnStatus = false;
        private int _SteamConnAttempts = 0;
        public int SteamUpdateInterval = 100;
        public int SteamConnTimeout = 10;
        #endregion

        #region Thread Control Variables
        private int _TickCounter = 0;
        private float _SteamTickCounter = 0;
        private bool isSteamTick 
        {
            get
            {
                _SteamTickCounter += Time.deltaTime;
                int _STC = Mathf.FloorToInt(_SteamTickCounter);
                return _STC % SteamUpdateInterval == 0 ? true : false;
            }

        }
        #endregion

        #region Local Player Variables
        private TDPlayer LocalTDPlayer;
        #endregion

        #region SubManager Registers
        public void register(OverlayManager overlayManager)
        {
            if (_overlayManager != null) { _overlayManager = overlayManager; }
        }
        #endregion

        public TDPlayer GetLocalPlayer() { return LocalTDPlayer; }





    }
}
