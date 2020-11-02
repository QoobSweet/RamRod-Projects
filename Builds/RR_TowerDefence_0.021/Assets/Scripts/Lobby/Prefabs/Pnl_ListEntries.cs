using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Pnl_ListEntries : MonoBehaviour
{
    public int MaxPlayers = 8;

    public GameObject DataTemplate;

    public bool Iterate = true;
    public Color BGColor_1;
    public Color BGColor_2;

    private Vector3 c_position;
    public Vector3 position = new Vector3(30, 323, 0);
    public Vector3 entryOrigin = new Vector3(0, 0, 0);

    private float c_spacing;
    public float Spacing = -33;

    public GameObject[] ObjEntries;
    public Dictionary<int, TD_Player> ConnectedPlayers = new Dictionary<int, TD_Player>();
    

    private void Start()
    {
        ClearandReset();
       

        c_spacing = Spacing;
        entryOrigin.y = this.position.y + (Spacing/2);
        c_position = position;
        this.transform.position = position;
        UpdatePlayerPanel();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdatePlayerPanel()
    {
        this.transform.position = position;


        //update from previous map lobby
        for(int i = 0; i < MaxPlayers; i++)
        {
            int PlayerNum = i + 1;
            GameObject _target;
            Data_LobbyPlayer lobbyPlayer;


            //Create new Datapoint if one does not exist
            if (ObjEntries[i] == null)
            {
                ObjEntries[i] = Instantiate(DataTemplate, this.transform);
            }

            //pull LobbyPlayer Display Info
            _target = ObjEntries[i];
            lobbyPlayer = _target.GetComponent<Data_LobbyPlayer>();

            if (ConnectedPlayers.ContainsKey(i))
            {

                if (lobbyPlayer.HasPlayer)
                {
                    if (!(lobbyPlayer.Name == ConnectedPlayers[i].Name))
                    {
                        lobbyPlayer.UpdatePlayerName(ConnectedPlayers[i].Name);
                    }
                }
                else
                {
                    lobbyPlayer.Init(i + 1, ConnectedPlayers[i].Name);
                }
            }
            else
            {
                lobbyPlayer.Init(PlayerNum);
            }


            if (PlayerNum % 2 == 0)
            {
                lobbyPlayer.SetBGColor(BGColor_2);
            }
            else
            {
                lobbyPlayer.SetBGColor(BGColor_1);
            }



            float Offset = Spacing * PlayerNum + this.transform.position.y - Spacing;
            Vector3 newPosition = _target.transform.position;
            newPosition.y = Offset;
            _target.transform.position = newPosition;

        }


        Debug.Log("Success");
    }

    public void ClearandReset()
    {
        foreach (GameObject child in ObjEntries)
        {
            Debug.Log("test 1");
            Destroy(child.gameObject);
            DestroyImmediate(child.gameObject);
            ObjEntries = new GameObject[MaxPlayers];
        }

        UpdatePlayerPanel();
    }

    public void AddPlayer()
    {
        TD_Player samplePlayer = new TD_Player("test name");
        int i = 0;
        bool _break = false;
        while (!_break && i < MaxPlayers)
        {
            if (!ConnectedPlayers.ContainsKey(i))
            {
                ConnectedPlayers[i] = samplePlayer;
                Debug.Log("Test");
                _break = true;
            }
            i++;
        }
        UpdatePlayerPanel();
    }

    public void SimulateUpdate()
    {
        UpdatePlayerPanel();
    }
}
