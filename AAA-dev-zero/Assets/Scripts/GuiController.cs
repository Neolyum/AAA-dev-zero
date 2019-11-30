using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{

    public class GuiController : MonoBehaviour
    {
        private bool showGameOver = false;
        GUIStyle myStyle = new GUIStyle();
        private string winner;
        public static GuiController Instance;
        private GameObject [] players;
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), "Speed: " + CameraController.Instance.getSpeed().ToString());


            if (showGameOver)
            {
                GUI.Label(new Rect(Screen.width / 4f, Screen.height / 8f, 500, 500), "Game Over!\nWinner is: " + winner, myStyle);
            }
            else GUI.Label(new Rect(Screen.width / 4f, Screen.height / 8f, 500, 500), "\t\t\t\t\n\n\n ", myStyle);

            if (players.Length > 0)
            {
                GUI.Label(new Rect(10, 10, 50, 50), "Player1");
            }
            if (players.Length > 1)
            {
                GUI.Label(new Rect(Screen.width -10 -50, 10, 50, 50), "Player2");
            }
            if (players.Length > 2)
            {
                GUI.Label(new Rect(10, Screen.height -10-50, 50, 50), "Player3");
            }
            if (players.Length > 3)
            {
                GUI.Label(new Rect(Screen.width - 10 - 50, Screen.height - 10 - 50, 50, 50), "Player4");
            }
        }

        private void Update()
        {
            players = GameObject.FindGameObjectsWithTag("Player");

        }
        private void Start()
        {
            myStyle.fontSize = 30;
            myStyle.normal.textColor = Color.red;
        }

        private void Awake()
        {
            Instance = this;
        }
      
        public void ShowGameOver(string winner)
        {
            showGameOver = true;
            this.winner = winner;
        }

        public void hideGameOver()
        {
            showGameOver = false;
        }
    }
}