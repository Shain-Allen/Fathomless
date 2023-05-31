using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitManager : MonoBehaviour
{
    public LargeEnemyBehavior enemy;
    GameObject bloodEffect;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("Harpoon Hit");
    //    if (collision.gameObject.CompareTag("harpoon"))
    //    {
    //        enemy.HarpoonHit(collision.gameObject.GetComponent<TurretProjectile>().harpoonDamage);
    //    }
    //}
    private void Start()
    {
        bloodEffect = enemy.bloodEffect;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("harpoon"))
        {
            GameObject blood = Instantiate(bloodEffect, other.transform);
            blood.transform.SetParent(transform, true);
            enemy.HarpoonHit(other.gameObject.GetComponent<TurretProjectile>().harpoonDamage);
        }
    }
}
