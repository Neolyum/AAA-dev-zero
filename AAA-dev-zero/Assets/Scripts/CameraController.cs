using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update


    public float speed = 2;
    public GameObject[] tileprefabs;
    public Camera MainCamera;
   
    void Start()
    {
        Instantiate(tileprefabs[0], new Vector3(2,2,2),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()


        
    {
        
        Vector2 offset = new Vector2(Time.time * speed, 1);
        this.transform.position = offset;
    }
}
