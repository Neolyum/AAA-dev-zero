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
                string outs = "";
                for (int i = 0; i < players[0].GetComponent<PlayerController2>().getDashCooldownTimer(); i++)
                {
                    outs += "#";
                }
                GUI.Label(new Rect(10, 10, 60, 50), "Player1 Dash " + outs);
            }
            if (players.Length > 1)
            {
                string outs = "";
                for (int i = 0; i < players[1].GetComponent<PlayerController2>().getDashCooldownTimer(); i++)
                {
                    outs += "#";
                }
                GUI.Label(new Rect(Screen.width -10 -50, 10, 60, 50), "Player2 Dash " + outs);
            }
            if (players.Length > 2)
            {
                string outs = "";
                for (int i = 0; i < players[2].GetComponent<PlayerController2>().getDashCooldownTimer(); i++)
                {
                    outs += "#";
                }
                GUI.Label(new Rect(10, Screen.height -10-50, 60, 50), "Player3 Dash " + outs);
            }
            if (players.Length > 3)
            {
                string outs ="";
                for (int i = 0; i < players[3].GetComponent<PlayerController2>().getDashCooldownTimer(); i++ )
                {
                    outs += "#";
                }
                GUI.Label(new Rect(Screen.width - 10 - 50, Screen.height - 10 - 50, 60, 50), "Player4 Dash " + outs);
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