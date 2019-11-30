using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Threading;
using System;

public class Buff
{
    private int duration;
    private string name;
    private GameObject player;
    public Buff(string name, int duration, GameObject player)
    {

        this.duration = duration;

        PlayerController2 script = player.GetComponent<PlayerController2>();
        choose(name, script);

    }


    public Buff(string name, int duration, List<GameObject> players)
    {



        this.duration = duration;

        foreach(GameObject p in players)
        {
            new Buff(name, duration, p);
        }

        

    }


    public void choose(string buff, PlayerController2 scipt)
    {
            switch (buff)
        {
            case "speed":
                speed(scipt); break;

            case "dashcooldown":
                dashcooldown(scipt); break;

            case "jumpboost":
                jumpboost(scipt); break;

            case "slowdown":
                slowdown(scipt); break;

            case "lowgrav":
                lowgrav(scipt); break;

            case "mirror":
                mirror(scipt); break;

            case "starpower":
                starpower(scipt); break;

    }
}

    private void speed(PlayerController2 script)
    {
        int offset = 10;
        
        float old_speed = script.getSpeed();
        script.setSpeed(old_speed + offset);
        Thread.Sleep(this.duration);
        script.setSpeed(old_speed - offset);
    }

    private void dashcooldown(PlayerController2 script)
    {
        int offset = 2;
        float old_dashcooldown = script.getDashCooldown();
        script.setDashCooldown(old_dashcooldown / offset);
        Thread.Sleep(this.duration);
    }

    private void jumpboost(PlayerController2 script)
    {

    }

    private void slowdown(PlayerController2 script)
    {
        int offset = -10;

        float old_speed = script.getSpeed();
        script.setSpeed(old_speed + offset);
        Thread.Sleep(this.duration);
        script.setSpeed(old_speed - offset);

    }

    private void lowgrav(PlayerController2 script)
    {

    }

    private void mirror(PlayerController2 script)
    {

    }

    private void starpower(PlayerController2 script)
    {   

        
        foreach (Collider2D hitbox in script.GetComponents<Collider2D>())
        {
            hitbox.enabled = false;
        }
        Thread.Sleep(duration);
        foreach (Collider2D hitbox in script.GetComponents<Collider2D>())
        {
            hitbox.enabled = true;
        }

    }


}