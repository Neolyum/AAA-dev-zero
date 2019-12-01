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
                GUI.Label(new Rect(Screen.width / 5f, Screen.height / 10f, 1000, 500), "Game Over! " + winner + " Player wins!", myStyle);
            }
            


        }

       
        private void Start()
        {
            myStyle.fontSize = 40;
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