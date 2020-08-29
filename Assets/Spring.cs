using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public int Height = 10;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().SpringJump(Height);
        }
    }
}
