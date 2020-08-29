using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool upanddown;
    public float speed = 2.0f;
    float startpoint;
    public float AmountMoved;
    float endpoint;
    bool up = true, down;
    void Start()
    {
        if (upanddown == true)
        {
            startpoint = this.gameObject.transform.position.y;
        }
        else
        {
            startpoint = this.gameObject.transform.position.x;
        }
        endpoint = startpoint + AmountMoved;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (upanddown == true)
        {
            if (up == true)
            {
                this.gameObject.transform.Translate(new Vector3(0.0f, 1.0f * Time.deltaTime * speed, 0.0f), Space.World);
                if (this.gameObject.transform.position.y >= endpoint)
                {
                    up = false;
                    down = true;
                }
            }
            if (down == true)
            {
                this.gameObject.transform.Translate(new Vector3(0.0f, -1.0f * Time.deltaTime * speed, 0.0f), Space.World);
                if (this.gameObject.transform.position.y <= startpoint)
                {
                    down = false;
                    up = true;
                }
            }
        }
        else
        {
            if (up == true)
            {
                this.gameObject.transform.Translate(new Vector3(1.0f * Time.deltaTime * speed, 0 , 0.0f), Space.World);
                if (this.gameObject.transform.position.x >= endpoint)
                {
                    up = false;
                    down = true;
                }
            }
            if (down == true)
            {
                this.gameObject.transform.Translate(new Vector3(-1.0f * Time.deltaTime * speed, 0, 0.0f), Space.World);
                if (this.gameObject.transform.position.x <= startpoint)
                {
                    down = false;
                    up = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.parent = transform;
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.parent = null;
        }
    }


}
