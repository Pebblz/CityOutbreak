using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //for the direction moving
    Vector3 moveDir;
    float Speed = 5;
    float JumpSpeed = 3;
    bool IsGrounded = false;
    float distToGround;
    public GameObject Bullet;
    float Rof = .5f;
    //just in case we would need to find out 
    //if he is grounded or not for later
    Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded == true || Input.GetKey(KeyCode.UpArrow) && IsGrounded == true
            || Input.GetKey(KeyCode.Z) && IsGrounded == true)
        {
            rb.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Speed * Time.deltaTime;
           // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.15F);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
          //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-Vector3.right), 0.15F);
        }
        if(Input.GetKey(KeyCode.Space) && Rof <= 0 || Input.GetKey(KeyCode.X) && Rof <= 0)
        {
            GameObject bulClone = Instantiate(Bullet,this.gameObject.transform.position, Quaternion.identity);
            bulClone.GetComponent<Rigidbody>().velocity = transform.right * 5;
            Rof = .5f;
        }

        Rof -= Time.deltaTime;
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "floor")
        {
            IsGrounded = true;
        }
    }


    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "floor")
        {
            IsGrounded = false;
        }
    }
}
