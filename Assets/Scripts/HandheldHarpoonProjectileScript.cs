using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandheldHarpoonProjectileScript : MonoBehaviour
{
    public float projectileSpeed;
    Rigidbody rb;
    public int harpoonDamage;
    private void Start()
    {
        CheckpointDataHandler.instance.AddToHarpoonArray(this.gameObject);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * projectileSpeed, ForceMode.Impulse);
    }
    private void FixedUpdate()
    {
        Vector3 distToPlayer = transform.position - PlayerScript.instance.transform.position;
        if (distToPlayer.magnitude > 1000)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            gameObject.transform.parent = other.transform;
            rb.isKinematic = true;
        }
    }
}
