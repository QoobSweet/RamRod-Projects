using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Controller for setting Initial Dependencies
public partial class TD_GlobalControl : MonoBehaviour
{
    public Component LoadingBarController;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(1);
    }
}
