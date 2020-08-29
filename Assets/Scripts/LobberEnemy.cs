using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobberEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lobPrefab;
    private GameObject player;
    public GameObject audioObject;
    public AudioClip clip;
    public float ROF = 4f;
    private float initROF;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initROF = ROF;
    }

    // Update is called once per frame
    void Update()
    {
        ROF -= Time.deltaTime;
        if(ROF <= 0f)
        {
            shoot();
            ROF = initROF;
        }
    }

    public void shoot()
    {
        var bullet = Instantiate(lobPrefab);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
        bullet.GetComponent<LobScript>().direction = getDirectionMagnitudeTowardsPlayer();

    }

    public float getDirectionMagnitudeTowardsPlayer()
    {
        return Mathf.Sign(player.transform.position.x - this.transform.position.x);
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
