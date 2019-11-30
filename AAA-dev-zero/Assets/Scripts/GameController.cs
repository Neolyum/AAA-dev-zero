using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{

    public class GameController : Singleton<GameController>
    {
        #region Variables
        private string lastPlayer;
        #endregion


        private void Start()
        {
            SceneController.Instance.LoadScene(enums.GameScenes.Menu);
        }
        public void GameOver()
        {
            //reset all players, delete all Grids, go to menu
            GuiController.Instance.ShowGameOver(lastPlayer);
            SoundsLib.Instance.play2D(enums.Sounds.gameOver);
            
            CameraController.Instance.reset();
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;
            SceneController.Instance.LoadScene(enums.GameScenes.Menu);
        }
        private void FixedUpdate()
        {

            if (Input.GetKeyDown(KeyCode.G))
            {
                GameOver();
            }
            if (SceneController.Instance.activeScene == enums.GameScenes.Level)
            {
                if (GameObject.Find("Player 2.0(Clone)") == null) GameOver();
                else lastPlayer = GameObject.Find("Player 2.0(Clone)").name;
            }
        }




    }
}