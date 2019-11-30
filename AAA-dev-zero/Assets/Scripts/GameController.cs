using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{


    public class GameController : MonoBehaviour
    {
        #region Variables

        #endregion

        private void Start()
        {
            SceneController.Instance.LoadScene(enums.GameScenes.Menu);
        }



    }
}