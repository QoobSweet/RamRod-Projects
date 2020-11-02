using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;
using System.Net.Sockets;

namespace TDGame {
    public partial class GameManager : MonoBehaviour
    {
        #region MonoBehavior Routines
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            gameManager = this;

            //Attempt to Connect to Steam
            #region Steam Authentication
            Debug.Log("Attempting to Connect to Steam.");

            if (isSteamTick)
            {
                for (float i = 0; i <= SteamConnTimeout; i += 1)
                {
                    new WaitForSeconds(2);

                    while ((_SteamConnAttempts <= SteamConnTimeout) && !_SteamConnStatus)
                    {
                        bool steamStatus = SteamAPI.Init();

                        Debug.Log(steamStatus == true ? "Steam Connection Successful." : "Steam Connection Attempt #" + _SteamConnAttempts + " Failed!");

                        if (steamStatus)
                        {
                            _SteamConnStatus = true;
                            Debug.Log("User: " + SteamFriends.GetPersonaName() + " is Connected.");
                        }
                        else
                        {
                            _SteamConnAttempts += 1;
                        }
                    }
                }

                if (!_SteamConnStatus)
                {
                    //Set Offline Mode if Steam Connection unsucessful
                    Debug.Log(_SteamConnStatus == true ? "Steam has Connected Successfully " : "Steam has failed to connect.");
                    _OfflineMode = true;
                }
            }
            #endregion

            #region Build LocalPlayer Data
            LocalTDPlayer = new TDPlayer(_SteamConnStatus);

            #endregion

        }
        // Update is called once per frame
        void Update()
        {
            if ((_SteamConnStatus & !_Loaded) || (_OfflineMode & !_Loaded))
            {
                SceneManager.LoadScene(1);
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1)) { _Loaded = true; }
            }



            //Not Used Yet
            if (_Loaded) { /* Do Loaded Things */ }




            //Update TickCounter for Update Intervals
            _TickCounter = +1;
            if (_TickCounter >= 1000) { _TickCounter = 0; }
        }
        #endregion

    }
}
