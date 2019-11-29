using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] tileprefabs;
    public int i = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= 9)
        {
            GameObject x;
            x = Instantiate(tileprefabs[i]) as GameObject;
            i++;
        }
    }   
}
