﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    // public GameObject bullet;
    public bool spread = false;
    public float dashCD = 0;
    // Start is called before the first frame update
    public GameObject audioObject;
    public AudioClip clip;
    public GameObject DashFade, MutiFade, FireFade;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        dashCD += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // actually a dash not improveed speed, late change lmao
        if(other.gameObject.tag == "SpeedUp")
        {
            this.GetComponent<Player>().canDash = true;
          //  playSound();
            DashFade.SetActive(true);
            Vector3 temp = DashFade.transform.position;
            DashFade.transform.position = other.gameObject.transform.position;
            other.gameObject.transform.position = temp;
            other.gameObject.tag = "nil";
        }

        if (other.gameObject.tag == "Spread")
        {
           // playSound();
            spread = true;
            MutiFade.SetActive(true);
            Vector3 temp = MutiFade.transform.position;
            MutiFade.transform.position = other.gameObject.transform.position;
            other.gameObject.transform.position = temp;
            other.gameObject.tag = "nil";
            //Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "HealthUp")
        {
            playSound();

            this.GetComponent<Player>().Hp += 5;

            if(this.GetComponent<Player>().Hp >= 20)
            {
                this.GetComponent<Player>().Hp = 20;
            }

            Destroy(other.gameObject);
        }

        if(other.gameObject.tag  == "1up")
        {
            playSound();
            this.GetComponent<Player>().Lives += 1;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "FireUp")
        {
            this.GetComponent<Player>().hasFireBullets = true;
          //  playSound();
            FireFade.SetActive(true);
            Vector3 temp = FireFade.transform.position;
            FireFade.transform.position = other.gameObject.transform.position;
            other.gameObject.transform.position = temp;
            other.gameObject.tag = "nil";

            //Destroy(other.gameObject);
        }


    }

    void playSound()
    {
        var audio = Instantiate(audioObject);
        audio.GetComponent<AudioLoader>().timeout = 3f;
        audio.GetComponent<AudioLoader>().clip = this.clip;
        audio.GetComponent<AudioLoader>().Load();
        audio.GetComponent<AudioLoader>().pauseBackgroundMusic = true ;
        audio.GetComponent<AudioLoader>().PlayStopBackgroundMusic();
    }
}
