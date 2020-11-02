using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;


namespace Qnet
{
    public class QnUID
    {
        //fields
        private QnUID owner;
        private int _QnetUID;
        
        //accesors
        public int QnetID { get { return _QnetUID; } }

        //constructors
        public QnUID()
        {
            _QnetUID = -1;
        }
    }

    public partial class Server : QnUID
    {
        private QnUID ServerID;

        private static Dictionary<int, QnUID> UIDDictionary;
        private static List<int> DiscardedUIDs;

        private bool _isInitialized = false, _running = false;






        public bool isRegistered(QnUID input)
        {

            return false;
        }
        public bool Register(ref QnUID input)
        {

            return false;
        }

        public bool InitServer()
        {

            return false;
        }
    }
}