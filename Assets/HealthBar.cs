using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    GameObject Player;
    public Image ColorImg;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.value = Player.GetComponent<Player>().Hp;
        if(healthBar.value > 14)
        {
            ColorImg.color = Color.green;
        }
        if(healthBar.value <= 14 && healthBar.value >= 8)
        {
            ColorImg.color = Color.yellow;
        }
        if(healthBar.value < 8)
        {
            ColorImg.color = Color.red;
        }
    }
}
