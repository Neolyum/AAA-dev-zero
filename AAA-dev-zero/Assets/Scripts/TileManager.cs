using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class TileManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject[] grids;
        private int move; //difference of movement after last instanciate
        private int last = -1; //position of last instantiate
        private int camx; //position of camera
        private GameObject gridsObject;
        [SerializeField] private GameObject startGrid;
        int rdm;
        #endregion
        // Start is called before the first frame update
        void Awake()
        {
            gridsObject = GameObject.Find("Grids");
            Destroy(Instantiate(startGrid, CameraController.Instance.getPosition(), Quaternion.identity,gridsObject.transform),60);
        }

        void FixedUpdate()
        {

            camx = (int)CameraController.Instance.getPosition().x;
            move = camx % 20;


            if (move == 0 && last != camx)
            {
                rdm = Random.Range(0, grids.Length);
                last = camx;
                StartCoroutine("instanc");
            }

          

        }

        private void instanc()
        {
            Destroy(Instantiate(grids[rdm], CameraController.Instance.getPosition() + new Vector2(20, 0), Quaternion.identity, gridsObject.transform), 60);
            StopCoroutine("instanc");
        }
    }
}