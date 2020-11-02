using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_MM : MonoBehaviour
{
    private static GameObject _TD_GCGO;
    private static TD_GlobalControl _TD_GC;
    // Start is called before the first frame update
    void Start()
    {
        while (_TD_GCGO == null)
        {
            _TD_GCGO = GameObject.Find("TD_GlobalControl");
            //_TD_GC = _TD_GCGO.GetComponent<TD_GlobalControl>();
        }
    }


    public TD_GlobalControl TD_GlobalControl { get { return _TD_GC; } }

}
