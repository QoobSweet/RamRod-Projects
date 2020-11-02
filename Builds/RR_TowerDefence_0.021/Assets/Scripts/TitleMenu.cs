using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject Create_Join_LobbyMenu;

    // Start is called before the first frame update
    void Start()
    {
        OptionsMenu.SetActive(false);    
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseOptions()
    {
        OptionsMenu.SetActive(false);
    }
    public void OpenOptions()
    {
        OptionsMenu.SetActive(true);
    }
    public void OpenCreateJoinLobbySelect()
    {
        Create_Join_LobbyMenu.SetActive(true);
    }
    public void CloseCreateJoinLobbySelect()
    {
        Create_Join_LobbyMenu.SetActive(false);
    }


}
