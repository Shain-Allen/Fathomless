using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretProjectile : MonoBehaviour
{
    public GameObject obj;
    public Rigidbody projectileRigidbody;
    public float velocityProj;
    public bool test = false;
    public float harpoonDamage;
    public Rigidbody subRb;
    BoxCollider thisCol;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent.SetParent(subRb.transform, true);
        projectileRigidbody.velocity = subRb.velocity;
        projectileRigidbody.AddForce(transform.forward * velocityProj, ForceMode.Impulse);
        //EditorApplication.isPaused = true;

        thisCol = gameObject.GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
       Vector3 distToSub = projectileRigidbody.transform.position - subRb.transform.position;
        if (distToSub.magnitude > 1000)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (test == false)
    //    {
    //        GameObject collidedObject = collision.gameObject;
    //        obj.transform.parent = collidedObject.transform;
    //        projectileRigidbody.isKinematic = true;
    //        thisCol.enabled = false;
    //        projectileRigidbody.detectCollisions = false;

    //        Destroy(projectileRigidbody);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (test == false)
        {
            GameObject collidedObject = other.gameObject;
            obj.transform.parent = collidedObject.transform;
            projectileRigidbody.isKinematic = true;
            projectileRigidbody.detectCollisions = false;
        }
    }
}