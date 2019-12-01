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
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), "Speed: " + CameraController.Instance.getSpeed().ToString());


            if (showGameOver)
            {
                GUI.Label(new Rect(Screen.width / 4f, Screen.height / 8f, 500, 500), "Game Over!\nWinner is: " + winner, myStyle);
            }
            else GUI.Label(new Rect(Screen.width / 4f, Screen.height / 8f, 500, 500), "\t\t\t\t\n\n\n ", myStyle);
<<<<<<< HEAD
      }

        private void Update()
        {
            players = GameObject.FindGameObjectsWithTag("Player");
=======
>>>>>>> 77c9f538c1ed79386b2a34851d200f67a588aab2

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