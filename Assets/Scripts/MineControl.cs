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

    public MineWarningSound warningSound;
    public MineWarningSound warningSound2;


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
            GlobalSoundsManager.instance.PlayMineExplode();
        }
    }

    public void subDamage()
    {
        //explosion.SendEvent("OnPlay");
        Instantiate(explosion, explosionPoint.transform.position, Quaternion.identity);
        subMan.Hit();
        warningSound.beeping = false;
        warningSound2.beeping = false;
        //mineMesh.SetActive(false);
        gameObject.SetActive(false);
    }
}
