using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Steamworks;
using TDGame;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject EscapeMenu;
    public GameObject TitleMenu;
    public GameObject PlayerUI;
    public GameObject DevUI;



    private bool GameStarted = false;
    private bool EnableEscapeMenu = false;




    // Start is called before the first frame update

    void Start()
    {
        //Handshake with GameManager
        gameManager = GameManager.gameManager.register(this);

        if (!EscapeMenu)    { EscapeMenu = GameObject.Find("EscapeMenu").gameObject;    }
        if (!TitleMenu)     { TitleMenu = GameObject.Find("TitleMenu").gameObject;      }
        if (!PlayerUI)      { PlayerUI = GameObject.Find("PlayerUI").gameObject;        }
        if (!DevUI)         { DevUI = GameObject.Find("DevUI").gameObject;              }


        if (!EscapeMenu || !TitleMenu || !PlayerUI || !DevUI)
        {
            throw new Exception("Game Interfaces not assigned properly");
        }
        else
        {
            //Set all sub menus as inactive
            EscapeMenu.SetActive(false);
            TitleMenu.SetActive(true);
            PlayerUI.SetActive(false);
            DevUI.SetActive(false);


            DontDestroyOnLoad(this.gameObject);
        }
    }


    private void Update()
    {
        CheckDevConsoleHandle();
        

        if (!GameStarted)
        {
            //TitleMenu Functions
            TitleMenu.SetActive(true);
            EnableEscapeMenu = false;

        }
        else
        {
            //Game Functions
            CheckEscMenuHandle();

        }
    }

    private void CheckDevConsoleHandle()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            bool temp = DevUI.activeSelf;
            if (DevUI.activeSelf)
            {
                DevUI.SetActive(false);
            }
            else
            {
                DevUI.SetActive(true);
            }
            
        }
    }
    private void CheckEscMenuHandle()
    {
        if (EnableEscapeMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //toggle visibility
                if (EscapeMenu.activeSelf)
                {
                    EscapeMenu.SetActive(false);
                    PlayerUI.SetActive(true);
                } 
                else
                {
                    EscapeMenu.SetActive(true);
                    PlayerUI.SetActive(false);
                }
            }
        }
    }
   

    public void ReturnToGame()
    {
        EscapeMenu.SetActive(false);
        PlayerUI.SetActive(true);
    }
    public void ExitGame()
    {

        if (GameManager.gameManager.IsOnline)
        {
            SteamAPI.Shutdown();
            Destroy(GameManager.steamLayer.gameObject);
            Debug.Log("Attempting to Disconect from Steam");
        }
        else
        {
            Debug.Log("Steam is Offline. Closing App.");
        }
        Application.Quit();
    }
    public void StartGame()
    {
        TitleMenu.SetActive(false);
        PlayerUI.SetActive(true);
        EnableEscapeMenu = true;
        GameStarted = true;
        gameManager.LoadGame();
    }
}
