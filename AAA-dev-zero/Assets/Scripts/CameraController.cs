using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{

    public class CameraController : MonoBehaviour
    {

        private float speed = 4;

        public static CameraController Instance;


        private void Awake()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(new Vector2(Time.deltaTime * speed, 0));
        }

        private void FixedUpdate()
        {
            if (speed < 12) speed += 0.001f;
            if (Input.GetKeyDown(KeyCode.G))
            {
                GameOver();
            }
        }

        
        public Vector2 getPosition()
        {
            return transform.position;
        }

        public float getSpeed()
        {
            return speed;
        }


        private void GameOver()
        {
            //reset all players, delete all Grids, go to menu
            GuiController.Instance.ShowGameOver("Player1");


        }
    }
}