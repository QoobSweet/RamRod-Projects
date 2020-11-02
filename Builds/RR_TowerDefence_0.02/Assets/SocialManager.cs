using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TDGame;

public class SocialManager : MonoBehaviour
{
    public GameObject _ProfilePanel;
    public Text _UsernameField;

    // Start is called before the first frame update
    void Start()
    {
        SetDisplayName();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.IsOnline)
        {
            //Online Profile Updating ie. friends list/status, current status, etc.


        }
    }


    public void SetDisplayName()
    {
        if (GameManager.gameManager.IsOnline)
        {
            _UsernameField.text = TDLocalProfile.LocalUserName;
        }
    }
}
