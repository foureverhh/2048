using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource effectMusic;
    public AudioSource backgroundMusic;
    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        effectMusic.clip = clip;
        effectMusic.Play();
        Debug.Log("Clip is: " + clip);
    }

    //public void PlayMusicList(params AudioClip[] audioClips)
    //{
        
    //}
}
