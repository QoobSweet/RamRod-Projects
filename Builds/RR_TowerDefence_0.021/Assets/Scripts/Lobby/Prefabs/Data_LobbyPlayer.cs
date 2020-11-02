using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Data_LobbyPlayer : MonoBehaviour
{
    public int _PlayerNumber = 1;
    public string _DefaultPlayerUsername = "Empty";
    public string _PlayerUsername;
    public string Name { get { return _PlayerUsername; } }
    public bool _PlayerReadyStatus = false;
    public bool ReadyStatus { get { return _PlayerReadyStatus; } }
    private bool _HasPlayer = false;
    public bool HasPlayer { get { return _HasPlayer; } }


    public Image BGHighlight;
    public Text _textCom_PlayerNumber;
    public Text _textCom_PlayerUsername;
    public Toggle _togCom_PlayerStatus;

    // Update is called once any methods have been used
    void UpdatePlayer()
    {
        _textCom_PlayerNumber.text = _PlayerNumber.ToString();
        _textCom_PlayerUsername.text = _PlayerUsername.ToString();
        _togCom_PlayerStatus.isOn = _PlayerReadyStatus;
    }

    // Start is called before the first frame update
    public void Init(int PlayerNumber)
    {
        Init(PlayerNumber, _DefaultPlayerUsername);
    }

    public void Init(int PlayerNumber, string PlayerUsername)
    {
        _PlayerNumber = PlayerNumber;
        _PlayerUsername = PlayerUsername;

        if (PlayerUsername != _DefaultPlayerUsername)
        {
            _HasPlayer = true;
        }
        UpdatePlayer();
    }

    public void RemovePlayer()
    {
        _HasPlayer = false;
        Init(_PlayerNumber);
    }

    public void UpdatePlayerNumber(int PlayerNumber)
    {
        _PlayerNumber = PlayerNumber;
        UpdatePlayer();
    }

    public void UpdatePlayerName(string PlayerUsername)
    {
        _PlayerUsername = PlayerUsername;
        UpdatePlayer();
    }



    public void SetBGColor(Color color)
    {
        if(BGHighlight == null) { BGHighlight = this.transform.Find("BGHighlight").GetComponent<Image>(); }
        this.BGHighlight.color = color;
        BGHighlight.enabled = true;
        UpdatePlayer();
    }
}
