using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeout = 4f;
    public AudioClip clip;
    private bool played = false;
    public bool pauseBackgroundMusic = false;
    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
   
        if (timeout <= 0f)
        {
            if(pauseBackgroundMusic)
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
        timeout -= Time.deltaTime;
    }

    public void Play()
    {
        GetComponent<AudioSource>().Play();
        played = true;
    }

    public void PlayStopBackgroundMusic()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
        GetComponent<AudioSource>().Play();


    }

    public void Load()
    {
        GetComponent<AudioSource>().clip = clip;
    }
}
