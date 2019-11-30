using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Background : MonoBehaviour
{
    [SerializeField] private GameObject[] backgroundTiles;
   
    private void generateBackground(Vector2 camPosition)
    {
        float width = Screen.width / 30;
        float height = Screen.height / 64;
        for (float i = camPosition.x - width; i < camPosition.x + width; i += 2)
        {
            for (float j = camPosition.y - height; j < camPosition.y + height; j += 2)
            {
                Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], new Vector2(i, j),Quaternion.identity);
            }
        }
    }
    
    
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("generateBackground", Controller.CameraController.Instance.getPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
