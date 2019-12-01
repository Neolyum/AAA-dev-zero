using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsLib : Singleton<SoundsLib>
{
    #region Variables
    [SerializeField] private AudioClip[] clips;
    private AudioSource source;
    [SerializeField]  private AudioSource musicSource;

    #endregion

    private void Start()
    {
        source = GameObject.Find("Main Camera").AddComponent<AudioSource>();
        musicSource.volume = 0.13f;
        musicSource.Play();
    }

    public void play2D(enums.Sounds sound)
    {
        source.clip = clips[(int)sound];
        source.Play();
    }
    public void play(Vector2 position, enums.Sounds sound)
    {
        AudioSource.PlayClipAtPoint(clips[(int)sound], position, 2.0f);
    }

    public void play(Vector2 position, enums.Sounds sound, float volume)
    {
        AudioSource.PlayClipAtPoint(clips[(int)sound], position, volume);
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
        dash,
        gameOver,
        Length
    }
}