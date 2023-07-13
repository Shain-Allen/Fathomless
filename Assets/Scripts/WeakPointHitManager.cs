using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointHitManager : MonoBehaviour
{
    public LargeEnemyBehavior enemy;
    public GameObject EelHeadLight;
    bool Cooldown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("harpoon") && !Cooldown)
        {
            Cooldown = true;
            EelHeadLight.SetActive(false);
            enemy.currentState = LargeEnemyBehavior.State.Stun;
            enemy.HarpoonHit(other.gameObject.GetComponent<TurretProjectile>().harpoonDamage);
            StartCoroutine(StartStunCooldown());
        }
    }
    private IEnumerator StartStunCooldown()
    {
        yield return new WaitForSeconds(enemy.StunCooldown);
        Cooldown = false;
        EelHeadLight.SetActive(true);

    }

}
