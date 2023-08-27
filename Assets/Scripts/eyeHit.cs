using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeHit : MonoBehaviour //this script goes on the eye of the enemy
{

    public patrolScript patrolScript;

    public EnemyDataManager enemyDat;

/*    private void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }*/


    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(other);
    }
    public void TakeDamage(Collider other)
    {
        if (other.tag == "harpoon")
        {
            /*patrolScript.patrolCase = 5;*/
            patrolScript.eyeDamage = true;

            enemyDat.MakeHurtSound();
            if (other.GetComponent<HandheldHarpoonProjectileScript>() != null)
            {
                enemyDat.enemyHealth -= other.GetComponent<HandheldHarpoonProjectileScript>().harpoonDamage;
            }
            if (other.GetComponent<TurretProjectile>() != null)
            {
                enemyDat.enemyHealth -= (int)other.GetComponent<TurretProjectile>().harpoonDamage;
            }
        }
    }
    //I've chosen to write an overload method, incase we need to have it take damage through otherm means
    public void TakeDamage(int damage)
    {
        enemyDat.enemyHealth -= damage;
    }
}
