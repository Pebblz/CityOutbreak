using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float Timer = 1;
    public bool isEnemyBullet = false;
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
        if (isEnemyBullet)
        {
            if (col.gameObject.name != "Bullet(Clone)" && col.gameObject.tag != "Enemy" && col.gameObject.name != "Spring" && col.gameObject.name != "CheckPoint")
            {
                Destroy(this.gameObject);
            }
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<Player>().Take1Damage();
            }
        }
        else
        {
            if (col.gameObject.tag != "Player" && col.gameObject.name != "Bullet(Clone)" && col.gameObject.tag != "Enemy" && col.gameObject.tag != "Breakable" && col.gameObject.name != "Spring" && col.gameObject.name != "CheckPoint")
            {
                Destroy(this.gameObject);
            }
            if(col.gameObject.tag == "Breakable")
            {
                col.gameObject.GetComponent<BlockToggleScript>().turnOff();
                Destroy(this.gameObject);
            }
            if (col.gameObject.tag == "Enemy")
            {

                if (col.gameObject.GetComponent<EnemyScript>() != null)
                {
                    col.gameObject.GetComponent<EnemyScript>().state = EnemyScript.EnemyState.DEAD;
                   
                } else if( col.gameObject.GetComponent<EnemyWalk>() != null)
                {
                    col.gameObject.GetComponent<EnemyWalk>().DIE();
                    
                } else if( col.gameObject.GetComponent<LobberEnemy>() != null)
                {
                    col.gameObject.GetComponent<LobberEnemy>().DIE();
                }
                else if (col.gameObject.GetComponent<DripScript>() != null)
                {
                    col.gameObject.GetComponent<DripScript>().DIE();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
