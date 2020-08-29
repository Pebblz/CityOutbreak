using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{

    public GameObject player;
   // public GameObject bullet;
    public bool spread = false;
    public float pUpCoolDown = 2f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        pUpCoolDown -= Time.deltaTime;

        if(pUpCoolDown <=0 )
        {
            spread = false;
            this.GetComponent<Player>().Speed = 6;

            pUpCoolDown = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpeedUp")
        {
            SpeedUp();
            pUpCoolDown = 4;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Spread")
        {
            spread = true;
            pUpCoolDown = 4;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "HealthUp")
        {
            player.GetComponent<Player>().Hp += 5;

            if(player.GetComponent<Player>().Hp >= 20)
            {
                player.GetComponent<Player>().Hp = 20;
            }

            Destroy(other.gameObject);
        }

        if(other.gameObject.tag  == "1up")
        {
            player.GetComponent<Player>().Lives += 1;
            Destroy(other.gameObject);
        }


    }

    public void SpeedUp()
    {
        player.GetComponent<Player>().Speed = 10;
    }
}
