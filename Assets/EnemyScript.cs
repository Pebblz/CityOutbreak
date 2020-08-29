
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyState
    {
        FOLLOW,
        IDLE,
        DEAD
    }


    public float speed = 1f;
    public float randomWalkDistance = 2f;
    public float distanceFromPlayerIgnore = 10f;
    public float walkingCoolDown = 4f;
    public float initWalkingCoolDown;
    public float targetIdlePositionX;
    GameObject player;
    public EnemyState state = EnemyState.IDLE;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initWalkingCoolDown = walkingCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        float playerPosition = player.transform.position.x;
        float enemyPositon = this.gameObject.transform.position.x;


        float distToPlayer = Mathf.Sqrt(Mathf.Pow(playerPosition, 2) + (Mathf.Pow(enemyPositon, 2)));
        if (this.state != EnemyState.DEAD)
        { 

            if (distToPlayer - this.transform.position.x > distanceFromPlayerIgnore)
            {
                this.state = EnemyState.IDLE;
            }
            else
            {
                this.state = EnemyState.FOLLOW;
            }
        }

        switch (this.state)
        {
            case EnemyState.FOLLOW:
                followPlayer(distToPlayer, enemyPositon, playerPosition);
                break;
            case EnemyState.IDLE:
                Idle();
                break;
            case EnemyState.DEAD:
                Dead();
                break;
         }
        walkingCoolDown -= Time.deltaTime;

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
        Destroy(this.gameObject);
    }


    #endregion
    bool leftOrRightOfPlayer(float enemyX, float playerX)
    {
        return enemyX >= playerX;
    }

    
}
