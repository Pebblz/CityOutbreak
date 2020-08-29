using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobberEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lobPrefab;
    private GameObject player;
    public float ROF = 4f;
    private float initROF;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initROF = ROF;
    }

    // Update is called once per frame
    void Update()
    {
        ROF -= Time.deltaTime;
        if(ROF <= 0f)
        {
            shoot();
            ROF = initROF;
        }
    }

    public void shoot()
    {
        var bullet = Instantiate(lobPrefab, this.transform);
        bullet.GetComponent<LobScript>().direction = getDirectionMagnitudeTowardsPlayer();

    }

    public float getDirectionMagnitudeTowardsPlayer()
    {
        return Mathf.Sign(player.transform.position.x - this.transform.position.x);
    }
}
