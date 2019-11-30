using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<Controller.CameraController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
