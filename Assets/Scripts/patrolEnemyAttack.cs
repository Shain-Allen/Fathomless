using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolEnemyAttack : MonoBehaviour
{
    public bool doDamage;

    public float timerTime;
    public float maxTime;
    public patrolScript patroler;

    public float damageAmount;

    public Animator anim;

    public void Start()
    {
        timerTime = maxTime;
    }

    public void Update()
    {
        if(doDamage)
        {
            timerTime -= Time.deltaTime;
            if(timerTime <= 0)
            {
                patroler.DamagePlayer(damageAmount);
                anim.SetBool("attack", true);
                timerTime = maxTime;
            }
            else
            {
                anim.SetBool("attack", false);
            }
            //StartCoroutine(DamageCountDown());
        }
        if(!doDamage)
        {
            timerTime = maxTime;
        }
    }
/*
    IEnumerator DamageCountDown()
    {
        yield return new WaitForSeconds(timerTime);
        patroler.DamagePlayer(damageAmount);
    }*/
}
