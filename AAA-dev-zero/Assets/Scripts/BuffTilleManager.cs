using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTilleManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string buff;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;
        var script = player.GetComponent<PlayerController2>();

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
        
        switch (buff)
        {
            case "speed":
                script.setSpeed(script.getSpeed() + 10);
                break;

            case "dashcooldown":
                script.setDashCooldown(script.getDashCooldown() / 2);
                break;

            case "slowdown":
                foreach (GameObject p in other_player)
                {
                    var ps = p.GetComponent<PlayerController2>();
                    ps.setSpeed(ps.getSpeed() - 10);
                }
                break;
        }
            
    }
}
