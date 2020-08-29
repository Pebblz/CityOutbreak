using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockToggleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeout = 2f;
    private float initTimeout;
    public bool regenerate = false;
    private bool timedOut = true;


    void Start()
    {
        initTimeout = timeout;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timedOut && regenerate)
        {
            timeout -= Time.deltaTime;
            if(timeout <= 0)
            {
                turnOn();
                timedOut = true;
            }
        }
    }

    public void turnOn()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<MeshCollider>().enabled = true;
    }

    public void turnOff()
    {
        timeout = initTimeout;
        timedOut = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<MeshCollider>().enabled = false;
    }

}
