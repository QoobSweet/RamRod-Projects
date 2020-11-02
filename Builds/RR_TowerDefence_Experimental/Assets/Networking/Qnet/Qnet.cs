using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Qnet
{
    //Interfaces for Qnet
    public class QConnectionInfo
    {
        private IPAddress _localIPAddress;
        private IPAddress _remoteAddress;
        private int _listenPort;
        private int _remotePort;

        public IPAddress LocalIPAddress { get { return _localIPAddress; } }
        public IPAddress RemoteAddress { get { return _remoteAddress; } }
        public int ListenPort { get { return _listenPort; } }
        public int RemotePort { get { return _remotePort; } }

        public QConnectionInfo(string  Server_IP)
        {
            this._localIPAddress = GetExternalIP();
            this._remoteAddress = IPAddress.Parse(Server_IP);
        }

        public IPAddress GetExternalIP()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return IPAddress.Parse(localIP);
        }

        public interface iConnection
        {
            IPAddress LocalIPAddress { get; }
            IPAddress RemoteAddress { get; }
            int ListenPort { get; }
            int RemotePort { get; }
        }
    }



}
