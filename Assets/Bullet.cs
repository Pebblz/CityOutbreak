using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float Timer = 1;
    void Update()
    {
        if (Timer <= 0)
        {
            Destroy(this.gameObject);
        }
        Timer -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider col)
    {
        //col.gameObject.tag != "Enemy" ||
        //if ( col.gameObject.tag == "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
