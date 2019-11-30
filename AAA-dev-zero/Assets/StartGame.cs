using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class StartGame : MonoBehaviour
{
 public void Startgame()
    {
        SceneController.Instance.LoadScene(enums.GameScenes.Level);
    }
}
