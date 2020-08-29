using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripperScripter : MonoBehaviour
{
    float timeAlive = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive -= Time.deltaTime;

        if(timeAlive <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Drip(Clone)" && col.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().Take3Damage();
            Destroy(this.gameObject);

        }
    }
}
