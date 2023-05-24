using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    public GameObject obj;
    public Rigidbody projectileRigidbody;
    public float velocityProj = 1000;
    public bool test = false;
    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody.AddForce(transform.forward * velocityProj);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (test == false)
        {
            GameObject collidedObject = collision.gameObject;
            obj.transform.parent = collidedObject.transform;
            projectileRigidbody.isKinematic = true;
            projectileRigidbody.detectCollisions = false;
        }
    }
}