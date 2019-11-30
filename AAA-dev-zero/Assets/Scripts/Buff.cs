using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Threading;



public class Buff
{
    private int duration;
    private string name;
    private GameObject player;
    public Buff(string name, float duration, GameObject player)
    {

        this.duration = (int)duration;
        this.player = player;
        

        switch (name)
        {
            case "speed":
                speed(); break;

            case "dashcooldown":
                dashcooldown(); break;

            case "jumpboost":
                jumpboost(); break;

            case "slowdown":
                slowdown(); break;

            case "lowgrav":
                lowgrav(); break;

            case "mirror":
                mirror(); break;

            case "starpower":
                starpower(); break;

        }

    }

    private void speed()
    {
        int offset = 10;
        var script = this.player.GetComponent<PlayerController2>();
        float old_speed = script.getSpeed();
        script.setSpeed(old_speed + offset);
        Thread.Sleep(this.duration);
        script.setSpeed(old_speed - offset);
    }

    private void dashcooldown()
    {
        int offset = 2;
        var script = this.player.GetComponent<PlayerController2>();
        float old_dashcooldown = script.getDashCooldown();
        script.setDashCooldown(old_dashcooldown / offset);
        Thread.Sleep(this.duration);
    }

    private void jumpboost()
    {
        var script = this.player.GetComponent<PlayerController2>();

    }

    private void slowdown()
    {
        int offset = -10;
        var script = this.player.GetComponent<PlayerController2>();
        float old_speed = script.getSpeed();
        script.setSpeed(old_speed + offset);
        Thread.Sleep(this.duration);
        script.setSpeed(old_speed - offset);

    }

    private void lowgrav()
    {
        var script = this.player.GetComponent<PlayerController2>();

    }

    private void mirror()
    {
        var script = this.player.GetComponent<PlayerController2>();

    }

    private void starpower()
    {   

        var script = this.player.GetComponent<PlayerController2>();
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