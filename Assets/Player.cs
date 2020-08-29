using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //for the direction moving
    Vector3 moveDir;
    float Speed = 6;
    float JumpSpeed = 5;
    public GameObject Bullet;
    float Rof = .5f;
    //just in case we would need to find out 
    //if he is grounded or not for later
    Rigidbody rb;
    //stuff for checkpoints
    GameObject[] CheckPoint;
    GameObject curCheckPoint;
    Vector3 CheckPos;
    int Hp = 5;
    int Lives = 3;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        CheckPoint = GameObject.FindGameObjectsWithTag("CheckPoint");
        CheckPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Movement
        float DistanceToTheGround = GetComponent<Collider>().bounds.extents.y;

        bool IsGrounded = Physics.Raycast(transform.position, Vector3.down, DistanceToTheGround + 0.1f);



        if (Input.GetKey(KeyCode.W) && IsGrounded == true || Input.GetKey(KeyCode.UpArrow) && IsGrounded == true
            || Input.GetKey(KeyCode.Z) && IsGrounded == true)
        {
             rb.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);

           // rb.velocity += Vector3.up * JumpSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.right * Speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        #endregion
        #region BulletStuff
        if (Input.GetKey(KeyCode.Space) && Rof <= 0 || Input.GetKey(KeyCode.X) && Rof <= 0)
        {
            GameObject bulClone = Instantiate(Bullet,this.gameObject.transform.position, Quaternion.identity);
            bulClone.GetComponent<Rigidbody>().velocity = transform.right * 10;
            Rof = 1f;
        }
        
        Rof -= Time.deltaTime;
        #endregion


        for(int i = 0; i < CheckPoint.Length;i++)
        {
            if(CheckPoint[i].GetComponent<CheckPoint>().Active == true)
            {
                curCheckPoint = CheckPoint[i];
                CheckPos = curCheckPoint.transform.position;
                print(curCheckPoint);
                CheckPoint[i].GetComponent<CheckPoint>().Active = false;
            }
        }

        if (Hp <= 0)
        {
            RespawnAtCheckPoint();
            Hp += 20;
        }
        if (Lives <= 0)
        {
            GameOver();
        }
    }
    public void RespawnAtCheckPoint()
    {
        this.gameObject.transform.position = CheckPos;
        Lives -= 1;
    }
    public void Take1Damage()
    {
        Hp -= 1;
    }
    public void Take3Damage()
    {
        Hp -= 3;
    }
    public void Take5Damage()
    {
        Hp -= 5;
    }
    public void GameOver()
    {
        //for later
    }
}
