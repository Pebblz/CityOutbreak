using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    private float direction = 1f;

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
        
        this.gameObject.SetActive(false);
    }
}
