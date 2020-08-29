


using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyState
    {
        FOLLOW,
        IDLE,
        DEAD,
        SHOOT,
        MELEE,
        NOTHING
    }


    public float speed = 1f;
    public float randomWalkDistance = 2f;
    public float distanceFromPlayerIgnore = 10f;
    public float distanceToShootPlayer = 5f;
    public float walkingCoolDown = 4f;
    public float initWalkingCoolDown;
    public float targetIdlePositionX;
    public GameObject bullet;
    public GameObject audioObject;
    public AudioClip clip;

    GameObject player;
    public EnemyState state = EnemyState.NOTHING;
    float Rof = .5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initWalkingCoolDown = walkingCoolDown;
 

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0,0);
        Vector3 enemyPositon = new Vector3(transform.position.x, 0, 0);


        float distToPlayer = Vector3.Distance(enemyPositon, playerPosition);
        if (this.state != EnemyState.DEAD)
        {
            //int layerMask = 1 << 8;
            //layerMask = ~layerMask;
            //RaycastHit hit;
            //if(!Physics.Raycast(transform.position, transform.TransformDirection(playerPosition), out hit, Mathf.Infinity, layerMask))
            //{
            //   if(hit.transform.gameObject.tag == "Player")
            //    {
            //        this.state = EnemyState.FOLLOW;
            //    }
            //}
            if(distToPlayer - Mathf.Abs(this.transform.position.x) < distanceToShootPlayer)
            {
                this.state = EnemyState.SHOOT;
            }
            //else if (distToPlayer - this.transform.position.x > distanceFromPlayerIgnore)
            //{
            //    this.state = EnemyState.IDLE;
            //}
            else
            {
                this.state = EnemyState.NOTHING;
            }
        }

        switch (this.state)
        {
            //case EnemyState.FOLLOW:
            //    followPlayer(distToPlayer, enemyPositon.x, playerPosition.x);
            //    break;
            //case EnemyState.IDLE:
            //    Idle();
            //    break;
            case EnemyState.DEAD:
                Dead();
                break;
            case EnemyState.SHOOT:
               // followPlayer(distToPlayer, enemyPositon.x, playerPosition.x);
                Shoot(enemyPositon.x, playerPosition.x);
                break;
            case EnemyState.NOTHING:
                break;
         }
        walkingCoolDown -= Time.deltaTime;
        Rof -= Time.deltaTime;

    }


    #region StateBehaviors
    void followPlayer(float distToPlayer, float enemyX, float playerX)
    {
        float distToMove = Time.deltaTime * distToPlayer * speed;


        //Enemy to the right is true
        if(leftOrRightOfPlayer(enemyX, playerX))
        {
            this.transform.Translate(new Vector3(-distToMove, 0, 0));
        } else
        {
            this.transform.Translate(new Vector3(distToMove, 0, 0));
        }
        
        
    }

    void Idle()
    {

        if (this.transform.position.x == targetIdlePositionX)
            return;


        float distanceToMove = 0f;
        if (walkingCoolDown <= 0f)
        {
            float lowerBound = -1 * this.randomWalkDistance;
            float upperBound = this.randomWalkDistance;

            float walkDistance = Random.Range(lowerBound, upperBound);
            distanceToMove = walkDistance * Time.deltaTime;
            this.targetIdlePositionX = walkDistance;
            walkingCoolDown = initWalkingCoolDown;
        }
        else
        {
            distanceToMove = targetIdlePositionX * Time.deltaTime;
            
        }
    

        transform.Translate(new Vector3(distanceToMove, 0, 0));
    }


    void Dead()
    {
        var audio = Instantiate(audioObject);
        audio.GetComponent<AudioLoader>().clip = this.clip;
        audio.GetComponent<AudioLoader>().Load();
        audio.GetComponent<AudioLoader>().Play();
        this.gameObject.SetActive(false);
    }

   

    void Shoot(float enemyX, float playerX)
    {
        if(Rof <= 0)
        {
            GameObject bulClone = Instantiate(bullet, this.gameObject.transform.position, Quaternion.identity);
            bulClone.GetComponent<Bullet>().isEnemyBullet = true;
            if (leftOrRightOfPlayer(enemyX, playerX))
            {
                bulClone.GetComponent<Rigidbody>().velocity = transform.right * -10f;
            }
            else
            {
                bulClone.GetComponent<Rigidbody>().velocity = transform.right * 10f;
            }
            
            Rof = 1f;
        }

    }

    #endregion
    bool leftOrRightOfPlayer(float enemyX, float playerX)
    {
        return enemyX >= playerX;
    }



}
