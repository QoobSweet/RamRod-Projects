using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class TD_LobbyControl
{
    private TD_Player[] _connectedPlayers;
    private int _maxPlayers = 8;

    public TD_LobbyControl(int MaxPlayers)
    {
        _maxPlayers = MaxPlayers;
        _connectedPlayers = new TD_Player[MaxPlayers];
    }


    public TD_Player[] ConnectedPlayers { get { return _connectedPlayers; } }
    public bool ConnectPlayer(TD_Player IncomingPlayer)
    {
        //if Player NetworkInfo is locked, it means it is properly setup
        if (IncomingPlayer.IsLocked)
        {
            for(int i = 0; i < _maxPlayers; i++)
            {
                if(ConnectedPlayers[i] == null)
                {
                    ConnectedPlayers[i] = IncomingPlayer;
                    return true;
                }
            }
            Debug.Log("Max Players reached.");
        }
        Debug.Log("IncomingConnection Failed.");
        return false;

    }
}
