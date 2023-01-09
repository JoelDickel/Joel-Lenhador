using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioSource efeitos;

    public AudioClip fxCorte;
    public AudioClip fxMorte;
    public AudioClip fxPlay;

    // Start is called before the first frame update
    void Awake()    {

        if (instance == null)       {
            instance = this;

             } else {
            Destroy(instance);
        }

        DontDestroyOnLoad(gameObject);

    }

    public void PlayFx(AudioClip audio)    {
        efeitos.clip = audio;
        efeitos.Play();



    }
}
