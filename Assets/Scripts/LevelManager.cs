using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject bgm = GameObject.Find("BGM");
        AudioSource audiosource = bgm.GetComponent<AudioSource>();

        audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
