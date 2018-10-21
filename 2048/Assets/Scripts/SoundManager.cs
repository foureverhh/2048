using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource effectMusic;
    public AudioSource backgrumdMusic;
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    void PlayMusic(AudioClip clip)
    {
        effectMusic.clip = clip;
        effectMusic.Play();
    }
}
