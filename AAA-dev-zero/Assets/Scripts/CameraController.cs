using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private float speed = 2;
    public Camera MainCamera;


    // Update is called once per frame
    void Update() {
        transform.Translate(new Vector2(Time.deltaTime * speed, 0));
    }
}
