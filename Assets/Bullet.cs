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
    void OnTriggerEnter(Collider col)
    {
       
        if (col.gameObject.tag != "Player" && col.gameObject.name != "Bullet(Clone)" && col.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        } 
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyScript>().state = EnemyScript.EnemyState.DEAD;
        }
    }
}
