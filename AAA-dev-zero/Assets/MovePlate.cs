using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    [SerializeField] private int range = 3;
    private int sign = 1; //1 or -1
    private float moved = 0;
    [SerializeField] private float speed = 1.5f;


    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;

        transform.Translate(speed * time * sign, 0, 0);
        moved += time;

        if (moved > range)
        {
            moved = 0;
            sign *= -1;
        }
    }
}
