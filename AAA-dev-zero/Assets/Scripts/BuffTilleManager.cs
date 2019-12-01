using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuffTilleManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string buff;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Here!");
        GameObject player = collision.gameObject;

        GameObject[] player_array = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> all_players = new List<GameObject>(player_array);
        List<GameObject> other_player = new List<GameObject>();
        foreach (GameObject p in all_players)
        {
            if (p != player)
            {
                other_player.Add(p);
            }
        }
        if (buff == "slowdown")
        {
            Buff.init(buff, 0.1f, other_player);
        }
        else
        {
            Buff.init(buff, 500, player);
        }


    }
            
}

