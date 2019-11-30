using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class TileManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject[] grids;
        private int move;
        private int last = -1;
        private int camx;
        private GameObject gridsObject;
        #endregion
        // Start is called before the first frame update
        void Awake()
        {
            gridsObject = GameObject.Find("Grids");
            Destroy(Instantiate(grids[0], CameraController.Instance.getPosition(), Quaternion.identity,gridsObject.transform),60);
        }

        // Update is called once per frame
        void Update()
        {
            if (gridsObject == null)
                gridsObject = GameObject.Find("Grids");
            camx = (int)CameraController.Instance.getPosition().x;
            move = camx % 16;

            if (move == 0 && last != camx)
            {
                last = camx;
                Destroy(Instantiate(grids[Random.Range(0, grids.Length)], CameraController.Instance.getPosition() + new Vector2(16, 0), Quaternion.identity, gridsObject.transform),60);
            }
            

        }
    }
}