using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Qnet
{
    public partial class Client : MonoBehaviour
    {
        public string Server_IP;

        QConnectionInfo ConnectionInfo;

        public void Start()
        {
            ConnectionInfo = new QConnectionInfo(Server_IP);
        }

    }
}