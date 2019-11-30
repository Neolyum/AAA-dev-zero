using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{

    public class GameController : Singleton<GameController>
    {
        #region Variables
        private string lastPlayer;
        private bool gameIsRunning;
        List<GameObject> players = new List<GameObject>();
        Color[] colors = { new Color(0, 1, 0, 1), 
            new Color(1, 0, 0, 1), 
            new Color(0, 1, 0, 1), 
            new Color(0, 0, 1, 1), 
            new Color(1, 1, 0, 1), 
            new Color(0, 1, 1, 1),
            new Color(1, 0, 1, 1),
            new Color(1, 1, 1, 1),
            new Color(0, 0, 0, 1)};
        #endregion

        private void Start()
        {
            SceneController.Instance.LoadScene(enums.GameScenes.Menu);
        }
        public void GameOver()
        {
            gameIsRunning = false;
            //reset all players, delete all Grids, go to menu
            GuiController.Instance.ShowGameOver(lastPlayer);
            SoundsLib.Instance.play2D(enums.Sounds.gameOver);
            
            CameraController.Instance.reset();
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;
            SceneController.Instance.LoadScene(enums.GameScenes.Menu);

            while (GameObject.Find("Player 2.0(Clone)") != null)
            {
                Destroy(GameObject.Find("Player 2.0(Clone)"));
            }
        }

        private void Update()
        {
            if (!gameIsRunning) startIfReady();
        }
        private void FixedUpdate()
        {

            if (Input.GetKeyDown(KeyCode.G))
            {
                GameOver();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameOver();
            }
            if (SceneController.Instance.activeScene == enums.GameScenes.Level)
            {
                if (GameObject.Find("Player 2.0(Clone)") == null) GameOver();
                else lastPlayer = GameObject.Find("Player 2.0(Clone)").name;
            }
        }

        public void addPlayer(GameObject player)
        {
            players.Add(player);
            player.GetComponent<SpriteRenderer>().color = colors[players.Count - 1];
        }

        public void startIfReady()
        {
            if (players.Count >= 2)
            {
                foreach(GameObject player in players)
                {
                    if (!player.GetComponent<PlayerController2>().isPlayerReady())
                    {
                        return;
                    }
                }
                Startgame();
            }
        }

        public void Startgame()
        {
            gameIsRunning = true;
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerController2>().disableReadyText();
            }
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
            SoundsLib.Instance.play(CameraController.Instance.getPosition(), enums.Sounds.button);
            SceneController.Instance.LoadScene(enums.GameScenes.Level);
        }




    }
}