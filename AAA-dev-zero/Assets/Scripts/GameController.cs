using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Controller
{

    public class GameController : Singleton<GameController>
    {
        #region Variables
        private string lastPlayer;
        private bool gameIsRunning;
        public PlayerInputManager manager;
        List<GameObject> players = new List<GameObject>();
        Color[] colors = {
            new Color(1, 0, 0, 1), 
            new Color(0, 1, 0, 1), 
            new Color(0, 0, 1, 1), 
            new Color(1, 1, 0, 1), 
            new Color(0, 1, 1, 1),
            new Color(1, 0, 1, 1),
            new Color(1, 1, 1, 1),
            new Color(0, 0, 0, 1)
        };
        private bool gameOver = false;


        #endregion


        private string colorToString(Color c)
        {
            if (c.ToString() == new Color(1, 0, 0, 1).ToString()) return "Red";
            else if (c.ToString() == new Color(0, 1, 0, 1).ToString()) return "Green";
            else if (c.ToString() == new Color(0, 0, 1, 1).ToString()) return "Blue";
            else if (c.ToString() == new Color(1, 1, 0, 1).ToString()) return "Pink";
            else if (c.ToString() == new Color(0, 1, 1, 1).ToString()) return "Yellow";
            else if (c.ToString() == new Color(1, 0, 1, 1).ToString()) return "Purple";
            else if (c.ToString() == new Color(1, 1, 1, 1).ToString()) return "White";
            else if (c.ToString() == new Color(0, 0, 0, 1).ToString()) return "Black";
            else return "Colourful";

        }
        private void Start()
        {
            SceneController.Instance.LoadScene(enums.GameScenes.Menu);
        }
        public IEnumerator GameOver()
        {
            gameOver = true;
            //reset all players, delete all Grids, go to menu
            GuiController.Instance.ShowGameOver(lastPlayer);
            SoundsLib.Instance.play2D(enums.Sounds.gameOver);

            yield return new WaitForSeconds(3);
            
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(p);
            }
            players = new List<GameObject>();

            gameIsRunning = false;
            GuiController.Instance.hideGameOver();

            CameraController.Instance.reset();
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;

           
            SceneController.Instance.LoadScene(enums.GameScenes.Menu);
            manager.EnableJoining();
           
        }

        private void Update()
        {
            if (!gameIsRunning) startIfReady();
        }
        private void FixedUpdate()
        {

           
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine("GameOver");
            }
            if (SceneController.Instance.activeScene == enums.GameScenes.Level)
            {
                if (GameObject.FindGameObjectsWithTag("Player").Length == 1 && !gameOver) StartCoroutine("GameOver");
                else lastPlayer = colorToString(GameObject.Find("Player 2.0(Clone)").GetComponent<SpriteRenderer>().color);
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
            GuiController.Instance.hideGameOver();
            manager.DisableJoining();
            gameIsRunning = true;
            int i = 0;
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerController2>().disableReadyText(colors[i]);
                i += 1;
            }
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
            SoundsLib.Instance.play(CameraController.Instance.getPosition(), enums.Sounds.button);
            SceneController.Instance.LoadScene(enums.GameScenes.Level);
        }




    }
}