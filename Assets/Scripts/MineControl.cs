using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MineControl : MonoBehaviour
{
    public GameManager gameMan;

    public SubDamageManager subMan;
    public float mineDamage;

    public GameObject mineMesh;
    public GameObject explosion;
    public GameObject explosionPoint;


    bool isActive;

    private void Start()
    {
        isActive = true;

        gameMan = GameManager.Instance;
        subMan = SubDamageManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "SubTag")
        {
            //explosion.SendEvent("Blow"); //Not Working
            subDamage();
        }
    }

    public void subDamage()
    {
        //explosion.SendEvent("OnPlay");
        Instantiate(explosion, explosionPoint.transform.position, Quaternion.identity);
        subMan.Hit();
        //mineMesh.SetActive(false);
        Destroy(gameObject);
    }
}
