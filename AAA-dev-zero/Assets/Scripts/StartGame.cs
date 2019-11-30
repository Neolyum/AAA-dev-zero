using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class StartGame : MonoBehaviour
{
   
    public void Startgame()
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
        SoundsLib.Instance.play(CameraController.Instance.getPosition(), enums.Sounds.button);
        SceneController.Instance.LoadScene(enums.GameScenes.Level);
    }
}
