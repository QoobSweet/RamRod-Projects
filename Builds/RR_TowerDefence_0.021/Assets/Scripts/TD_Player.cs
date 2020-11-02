using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class TD_Player : NetworkInfo
{
    //fields
    private string _name;


    //accessors
    public string Name { get { return _name; } }


    //constructors
    public TD_Player(string Username)
    {
        _name = Username;
        SetNetworkInfo();


    }


    //Methods
}


public class NetworkInfo
{
    private IPAddress _ipAddress;
    private IPEndPoint _localIPEndPoint;
    private EndPoint _localEndPoint;
    private int _port = 60050;
    private bool _locked = false;

    public bool IsLocked { get { return _locked; } }
    public IPAddress IPAddress { get { return _ipAddress; } internal set { _ipAddress = value; } }
    public IPEndPoint LocalIPEndPoint { get { return _localIPEndPoint; } }
    public EndPoint LocalEndPoint { get { return _localEndPoint; } }

    //Methods
    public void SetNetworkInfo()
    {
        if (!_locked)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                _localEndPoint = socket.LocalEndPoint;
                _localIPEndPoint = _localEndPoint as IPEndPoint;
                _ipAddress = IPAddress.Parse(_localIPEndPoint.Address.ToString());
            }
        }
        _locked = true;
    }
}