using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsLib : Singleton<SoundsLib>
{
    #region Variables
    [SerializeField] private AudioClip[] clips;
    private AudioSource source;

    #endregion

    private void Start()
    {
        source = GameObject.Find("Main Camera").AddComponent<AudioSource>();

    }

    public void play2D(enums.Sounds sound)
    {
        source.clip = clips[(int)sound];
        source.Play();
    }
    public void play(Vector2 position, enums.Sounds sound)
    {
        AudioSource.PlayClipAtPoint(clips[(int)sound], position);
    }
}

namespace enums
{
    public enum Sounds
    {
        explosion,
        shoot,
        button,
        jump,
        gameOver,
        Length
    }
}