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
}
