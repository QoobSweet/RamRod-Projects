using UnityEngine.SceneManagement;

namespace TDGame
{
    partial class GameManager //TitleScreen Logic
    {
        // Start is called before the first frame update

        public void UpdateTitleScreen()
        {

        }
        

        public void LoadGame()
        {
            SceneManager.LoadScene("Main_Game");
            gameState = GS.InGame;
        }
    }
}