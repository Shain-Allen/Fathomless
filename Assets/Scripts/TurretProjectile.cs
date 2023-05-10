using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    public GameObject obj;
    public Rigidbody projectileRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody.AddForce(transform.forward * 1000);
    }

    private void OnCollisionEnter(Collision collision)
    {
        projectileRigidbody.velocity = new Vector3(0, 0, 0);
        GameObject collidedObject = collision.gameObject;
        this.transform.parent = collidedObject.transform;
        projectileRigidbody.isKinematic = true;
        projectileRigidbody.detectCollisions = false;
        Destroy(obj);
    }
}