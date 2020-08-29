using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //for the direction moving
    Vector3 moveDir;
    public float Speed = 6;
    public float JumpSpeed = 20;
    public GameObject Bullet;
    public GameObject FireBullet;
    float Rof = .5f;
    //just in case we would need to find out 
    //if he is grounded or not for later
    Rigidbody rb;
    //stuff for checkpoints
    GameObject[] CheckPoint;
    GameObject curCheckPoint;
    Vector3 CheckPos;
    public bool canDash = false;
    public bool hasFireBullets = false;
    bool IsGrounded;
    public int Hp = 20;
    public int Lives = 3;
    public AudioSource shoot;
    bool isrunning;
    private float dashTimeout = 5f;
    Animator anim;
    public GameObject AudioObject;
    public AudioClip hurt;
    public AudioClip die;
   
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        CheckPoint = GameObject.FindGameObjectsWithTag("CheckPoint");
        CheckPos = this.gameObject.transform.position;
        anim = GetComponent<Animator>();
        anim.Play("idle");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Movement

        if (rb.velocity.y == 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }


        if(canDash && Input.GetKey(KeyCode.LeftShift) && dashTimeout <= 0)
        {
            transform.Translate(Vector3.forward * 5f);
            dashTimeout = 5f;
        }

        dashTimeout -= Time.deltaTime;


        if (Input.GetKey(KeyCode.W) && IsGrounded == true || Input.GetKey(KeyCode.UpArrow) && IsGrounded == true
            || Input.GetKey(KeyCode.Z) && IsGrounded == true)
        {
                rb.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
                anim.SetBool("jump", true);
                anim.SetBool("idle", false);
                anim.SetBool("run", false);
                anim.SetBool("shootrun", false);
                anim.SetBool("shootjump", false);
                anim.SetBool("shootidle", false);
            // rb.velocity += Vector3.up * JumpSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (IsGrounded && isrunning == false)
            {
                anim.SetBool("run", true);
                anim.SetBool("jump", false);
                anim.SetBool("idle", false);
                anim.SetBool("shootrun", false);
                anim.SetBool("shootjump", false);
                anim.SetBool("shootidle", false);
            }
            transform.position += transform.forward * Speed * Time.deltaTime;
            //rb.AddForce(new Vector3(2, 0, 0), ForceMode.Acceleration);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (IsGrounded && isrunning == false)
            {
                anim.SetBool("run", true);
                anim.SetBool("jump", false);
                anim.SetBool("idle", false);
                anim.SetBool("shootrun", false);
                anim.SetBool("shootjump", false);
                anim.SetBool("shootidle", false);
            }
            transform.position += transform.forward * Speed * Time.deltaTime;
            //rb.AddForce(new Vector3(2, 0, 0), ForceMode.Acceleration);
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else
        {
            if (IsGrounded == true)
            {
                anim.SetBool("run", false);
                anim.SetBool("jump", false);
                anim.SetBool("idle",true);
                anim.SetBool("shootrun", false);
                anim.SetBool("shootjump", false);
                anim.SetBool("shootidle", false);
            }
        }
        #endregion
        #region BulletStuff
        if (Input.GetKey(KeyCode.Space) && Rof <= 0 || Input.GetKey(KeyCode.X) && Rof <= 0)
        {
            shoot.Play();
            if (this.GetComponent<PowerupScript>().spread == false)
            {
                GameObject bulClone = Instantiate( (hasFireBullets)?FireBullet:Bullet, this.gameObject.transform.position + new Vector3(0,1.2f,0), Quaternion.identity);
                bulClone.GetComponent<Rigidbody>().velocity = transform.forward * 10;
                

                if(anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && isrunning == false)
                {
                    anim.SetBool("run", false);
                    anim.SetBool("jump", false);
                    anim.SetBool("idle", false);
                    anim.SetBool("shootidle", true);
                    anim.SetBool("shootrun", false);
                    anim.SetBool("shootjump", false);
                }
                if (IsGrounded == false)
                {
                    anim.SetBool("run", false);
                    anim.SetBool("jump", false);
                    anim.SetBool("idle", false);
                    anim.SetBool("shootjump", true);
                    anim.SetBool("shootidle", false);
                    anim.SetBool("shootrun", false);
                    
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
                {
                    anim.SetBool("run", false);
                    anim.SetBool("jump", false);
                    anim.SetBool("idle", false);
                    anim.SetBool("shootrun", true);
                    anim.SetBool("shootidle", false);
                    anim.SetBool("shootjump", false);
                    isrunning = true;
                }
            }

            else if (this.GetComponent<PowerupScript>().spread == true)
            {
                GameObject bulClone = Instantiate((hasFireBullets) ? FireBullet : Bullet, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                bulClone.GetComponent<Rigidbody>().velocity = transform.forward * 10;

                GameObject bulClone2 = Instantiate((hasFireBullets) ? FireBullet : Bullet, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 3, this.gameObject.transform.position.z), Quaternion.identity);
                bulClone2.GetComponent<Rigidbody>().velocity = transform.forward * 10;

                GameObject bulClone3 = Instantiate((hasFireBullets) ? FireBullet : Bullet, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), Quaternion.identity);
                bulClone3.GetComponent<Rigidbody>().velocity = transform.forward * 10;
            } else
            {
                isrunning = false;
            }

            Rof = 1f;
        }

        Rof -= Time.deltaTime;
        #endregion


        for (int i = 0; i < CheckPoint.Length; i++)
        {
            if (CheckPoint[i].GetComponent<CheckPoint>().Active == true)
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

        }
        if (Lives <= 0)
        {
            GameOver();
        }
    }
    public void RespawnAtCheckPoint()
    {
        this.gameObject.transform.position = CheckPos;
        if (Hp > 0)
        {
            Hp -= 1;
        }

        if (Hp <= 0)
        {
            Lives -= 1;
            Hp += 20;
        }

        GetComponent<RespawnEnemies>().resetEnemies();

    }
    public void Take1Damage()
    {
        Hp -= 1;
        if (Hp <= 0)
        {
            playDeath();
        } else
        {
            playOuch();
        }
    }
    public void Take3Damage()
    {
        Hp -= 3;
        if (Hp <= 0)
        {
            playDeath();
        }
        else
        {
            playOuch();
        }
    }
    public void Take5Damage()
    {
        Hp -= 5;
        if (Hp <= 0)
        {
            playDeath();
        }
        else
        {
            playOuch();
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Lose");
    }

    public void SpringJump(int Height)
    {
        rb.AddForce(new Vector3(0, JumpSpeed + Height, 0), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spike")
        {
            Hp = 0;
        } else if(other.gameObject.tag == "Win")
        {
            if (this.isPlayerWinning())
            {
                SceneManager.LoadScene("Win");
            }
        }
    }

    #region SFX
    public void playOuch()
    {
        var sound = Instantiate(AudioObject);
        sound.GetComponent<AudioLoader>().clip = hurt;
        sound.GetComponent<AudioLoader>().Load();
        sound.GetComponent<AudioLoader>().Play();

    }

    public void playDeath()
    {
        var sound = Instantiate(AudioObject);
        sound.GetComponent<AudioLoader>().clip = die;
        sound.GetComponent<AudioLoader>().Load();
        sound.GetComponent<AudioLoader>().Play();
    }
    #endregion


    public bool isPlayerWinning()
    {
        return this.hasFireBullets && this.canDash && GetComponent<PowerupScript>().spread;
    }
}
