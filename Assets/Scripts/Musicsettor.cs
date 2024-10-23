using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicsettor : MonoBehaviour
{
    public void SetMusic(AudioClip clip)
    {
        GameObject bgm = GameObject.Find("BGM"); //Find the gameobject called BGM'
        //BGM stands for background music
        AudioSource audiosource = bgm.GetComponent<AudioSource>(); //grab the audiosource from the BGM gameobject

        audiosource.clip = clip;
    }
}
