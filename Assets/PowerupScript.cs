using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{

    public GameObject player;
   // public GameObject bullet;
    bool speed = false;
    public bool spread = false;
    public float pUpCoolDown = 2f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(speed)
        {
            SpeedUp();
        }

        pUpCoolDown -= Time.deltaTime;

        if(pUpCoolDown <=0 )
        {
            spread = false;
            speed = false;
            pUpCoolDown = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpeedUp")
        {
            speed = true;
        }

        if (other.gameObject.tag == "Spread")
        {
            spread = true;
        }
    }

    public void SpeedUp()
    {
        player.GetComponent<Player>().Speed = 10;

        pUpCoolDown = 4;
    }
}
