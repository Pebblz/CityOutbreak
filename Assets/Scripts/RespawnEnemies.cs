using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] Enemies;
    public Vector3[] initialPositions;
    public float[] shootDistances;
    public float[] followDistances;
    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        initialPositions = new Vector3[Enemies.Length];
        shootDistances = new float[Enemies.Length];
        followDistances = new float[Enemies.Length];

        for(int i = 0; i < Enemies.Length; i++)
        {
            initialPositions[i] = Enemies[i].transform.position;
            if (Enemies[i].GetComponent<EnemyScript>() != null)
            {
                shootDistances[i] = Enemies[i].GetComponent<EnemyScript>().distanceToShootPlayer;
                followDistances[i] = Enemies[i].GetComponent<EnemyScript>().distanceFromPlayerIgnore;
            }
        }
    }

    public void resetEnemies()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].SetActive(true);
            Enemies[i].transform.position = initialPositions[i];


            if (Enemies[i].GetComponent<EnemyScript>() != null)
            {
                Enemies[i].GetComponent<EnemyScript>().state = EnemyScript.EnemyState.IDLE;
                Enemies[i].GetComponent<EnemyScript>().distanceToShootPlayer = shootDistances[i];
                Enemies[i].GetComponent<EnemyScript>().distanceFromPlayerIgnore = followDistances[i];
            }
            
        }
    }

}
