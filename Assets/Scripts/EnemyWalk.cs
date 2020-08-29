using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    private float direction = 1f;
    public GameObject audioObject;
    public AudioClip clip;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3( speed * Time.deltaTime * direction,0,0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
            this.direction *= -1f;
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
