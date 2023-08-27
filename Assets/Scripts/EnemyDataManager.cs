using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyDataManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyHealth;

    public patrolScript patrolScript;
    private Animator animator;
    public AnimationClip deathAnim;
    public GameObject bloodParticles;
    private AudioSource damageSound;
    private void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            StartCoroutine(killEnemy());
        }
    }

    private void Start()
    {
        //incase I forget why I did this, the checkpoint loader uses max health to heal enemies on load.
        enemyHealth = enemyMaxHealth;
        animator = GetComponent<Animator>();
        damageSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(other);
    }
    public void TakeDamage(Collider other)
    {
        if (other.tag == "harpoon")
        {
            /*patrolScript.patrolCase = 2;*/
            patrolScript.takenDamage = true;

            MakeHurtSound();
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
    public void MakeHurtSound()
    {
        float randomPitch = 1f + Random.Range(-2, 1f);
        damageSound.pitch = randomPitch;
        damageSound.Play();
    }
    //I've chosen to write an overload method, incase we need to have it take damage through other means
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
    }
    public void SpawnBlood()
    {
        GameObject particle = Instantiate(bloodParticles, null, true);
        particle.transform.position = transform.position;
    }
    public IEnumerator killEnemy()
    {
        animator.SetTrigger("Die");
        patrolScript.canMove = false;
        yield return new WaitForSeconds(deathAnim.length + 0.05f);
        patrolScript.canMove = true;
        gameObject.transform.parent.gameObject.SetActive(false);

    }
}
