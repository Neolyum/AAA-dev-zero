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

        public void reset()
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(0, 0, -10), Quaternion.identity);
        }

        public Vector2 getPosition()
        {
            return transform.position;
        }

        public float getSpeed()
        {
            return speed;
        }

        public static void StartGame()
        {
            GuiController.Instance.hideGameOver();
            SceneController.Instance.LoadScene(enums.GameScenes.Level);
        }

        private void FixedUpdate()
        {
            if (speed < 12) speed += 0.0015f;
        }

    }
}