using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    public Rigidbody projectileRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody.AddForce(transform.forward * 1000);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}