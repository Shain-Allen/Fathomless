using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineControl : MonoBehaviour
{
    public GameManager gameMan;

    public SubDamageManager subMan;
    public float mineDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerDamge(mineDamage);
        }

        if(collision.gameObject.tag == "SubTag")
        {
            subDamage();
        }
    }

    public void subDamage()
    {
        subMan.Hit();
        Destroy(gameObject);
    }

    public void playerDamge(float damage) //does not currently work with player object becuase player does not have rigibody/collider
    {
        gameMan.playerHealth -= damage;
    }
}
