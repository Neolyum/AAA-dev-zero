using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Threading;
using System;

public class Buff : MonoBehaviour
{
    private float duration;
    private PlayerController2 script;
    private string buffName;

    public void setDuration(float d)
    {
        this.duration = d;
    }

    public void setBuffName(string b)
    {
        buffName = b;
    }

    public void setScript(PlayerController2 c)
    {
        script = c;
    }


    /*public Buff(string name, int duration, GameObject player)
    {

        this.duration = duration;

        PlayerController2 script = player.GetComponent<PlayerController2>();
        choose(name, script);

    }*/

    public string getName()
    {
        return "This is " + this.ToString()  + " with duration " + duration.ToString() + "and name " + buffName;
    }
    public static void init(string name, float duration, GameObject player)
    {
        if (p.GetComponent<Buff>() != null) return;
        player.AddComponent<Buff>();
        
        player.GetComponent<Buff>().setDuration(duration);
        player.GetComponent<Buff>().setBuffName(name);
        player.GetComponent<Buff>().setScript(player.GetComponent<PlayerController2>());
        Debug.Log(player.GetComponent<Buff>().getName());
        player.GetComponent<Buff>().start();
        

    }


    public static void init(string name, float duration, List<GameObject> players)
    {

        foreach(GameObject p in players)
        {
            if (p.GetComponent<Buff>() != null) break;
            p.AddComponent<Buff>();

            p.GetComponent<Buff>().setDuration(duration);
            p.GetComponent<Buff>().setBuffName(name);
            p.GetComponent<Buff>().setScript(p.GetComponent<PlayerController2>());
            Debug.Log(p.GetComponent<Buff>().getName());
            p.GetComponent<Buff>().start();

        }
        

    }

    private void start()
    {
        PlayerController2 script = gameObject.GetComponent<PlayerController2>();
        choose(buffName, script);
    }


    public void choose(string buff, PlayerController2 scipt)
    {
            switch (buff)
        {
            case "speed":
                StartCoroutine("speed",scipt); break;

            case "dashcooldown":
                StartCoroutine("dashcooldown",scipt); break;

            case "jumpboost":
                StartCoroutine("jumpboost",scipt); break;

            case "slowdown":
                StartCoroutine("slowdown",scipt); break;

            case "lowgrav":
                StartCoroutine("lowgrav",scipt); break;

            case "mirror":
                StartCoroutine("mirror",scipt); break;

            case "starpower":
                StartCoroutine("starpower",scipt); break;

    }
}

    private IEnumerator speed(PlayerController2 script)
    {
        int offset = 20;
        
        float old_speed = script.getSpeed();
        script.setSpeed(old_speed + offset);
        Debug.Log("Started speedbuff");
        yield return new WaitForSeconds(duration);
        Debug.Log("stopped Speedbuff");
        script.setSpeed(old_speed - offset);
        Destroy(script.gameObject);
    }

    private IEnumerator dashcooldown(PlayerController2 script)
    {
        int offset = 2;
        float old_dashcooldown = script.getDashCooldown();
        script.setDashCooldown(old_dashcooldown / offset);
        yield return new WaitForSeconds(duration);
    }


    private void jumpboost(PlayerController2 script)
    {

    }

    private IEnumerator slowdown(PlayerController2 script)
    {
        int offset = -1;

        float old_speed = script.getSpeed();
        script.setSpeed(old_speed + offset);
        yield return new WaitForSeconds(duration);
        script.setSpeed(old_speed - offset);

    }

    private void lowgrav(PlayerController2 script)
    {

    }

    private void mirror(PlayerController2 script)
    {

    }

    private IEnumerator starpower(PlayerController2 script)
    {   

        
        foreach (Collider2D hitbox in script.GetComponents<Collider2D>())
        {
            hitbox.enabled = false;
        }
        yield return new WaitForSeconds(duration);
        foreach (Collider2D hitbox in script.GetComponents<Collider2D>())
        {
            hitbox.enabled = true;
        }

    }


}