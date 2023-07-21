using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public int enemyHealth;

    public patrolScript patrolScript;

    private void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(other);
    }
    public void TakeDamage(Collider other)
    {
        if (other.tag == "harpoon")
        {
            /*patrolScript.patrolCase = 5;*/
            patrolScript.takenDamage = true;

            if (other.GetComponent<HandheldHarpoonProjectileScript>() != null)
            {
                enemyHealth -= other.GetComponent<HandheldHarpoonProjectileScript>().harpoonDamage;
            }
            if (other.GetComponent<TurretProjectile>() != null)
            {
                enemyHealth -= (int)other.GetComponent<TurretProjectile>().harpoonDamage;
            }
        }
    }
    //I've chosen to write an overload method, incase we need to have it take damage through otherm means
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
    }
}
