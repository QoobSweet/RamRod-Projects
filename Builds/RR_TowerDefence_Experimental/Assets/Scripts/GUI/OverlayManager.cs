using System.Collections;
using System.Collections.Generic;
using TDGame;
using UnityEngine;

public partial class OverlayManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Register with local GameManager
        GameManager.gameManager.register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
