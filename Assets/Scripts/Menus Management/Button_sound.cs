using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_sound : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        sound.clip = clip;
    }

    public void Play_sound()
    {
        sound.Play();
    }
}
