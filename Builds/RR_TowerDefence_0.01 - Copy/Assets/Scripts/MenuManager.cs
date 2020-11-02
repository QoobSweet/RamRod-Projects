using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject EscapeMenu;
    public GameObject TitleMenu;
    public GameObject PlayerUI;
    public GameObject DevUI;
    public Dropdown MapSelector;

    private bool GameStarted = false;
    private bool EnableEscapeMenu = false;




    public Dictionary<int, Player> PlayerDatabase = new Dictionary<int, Player>();


    // Start is called before the first frame update

    void Start()
    {
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
        Application.Quit();
    }
    public void StartGame()
    {
        TitleMenu.SetActive(false);
        PlayerUI.SetActive(true);
        EnableEscapeMenu = true;
        GameStarted = true;
        SceneManager.LoadScene("Main_Game");
    }
}
