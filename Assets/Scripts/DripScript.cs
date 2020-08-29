using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripScript : MonoBehaviour
{

    public float dripTime = 2f;
    public GameObject dripper;
    float livetime = 2;
    // Update is called once per frame
    public GameObject audioObject;
    public AudioClip clip;
    void Update()
    {
        dripTime -= Time.deltaTime;

        if(dripTime <= 0)
        {
            Instantiate(dripper, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 2, this.gameObject.transform.position.z), Quaternion.identity);
            dripTime = 2f;
        }
    }

    public void DIE()
    {
        var audio = Instantiate(audioObject);
        audio.GetComponent<AudioLoader>().clip = this.clip;
        audio.GetComponent<AudioLoader>().Load();
        audio.GetComponent<AudioLoader>().Play();
        this.gameObject.SetActive(false);
    }

}
