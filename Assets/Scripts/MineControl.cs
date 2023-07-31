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
    public VisualEffect explosion;

    bool isActive;

    private void Start()
    {
        isActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "SubTag")
        {
            explosion.SendEvent("Blow"); //Not Working
            subDamage();
        }
    }

    public void subDamage()
    {
        subMan.Hit();
        mineMesh.SetActive(false);
    }
}
