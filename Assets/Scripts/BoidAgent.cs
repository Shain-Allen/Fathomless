using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class BoidAgent : MonoBehaviour
{
    public GameObject player;
    public BoidSpawner boidSpawner;
    Boid agentSpecies;
    public Boid AgentSpecies { get { return agentSpecies; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Boid species)
    {
        agentSpecies = species;
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Vector3 distToPlayer = transform.position - player.transform.position;
        if (distToPlayer.magnitude > boidSpawner.exitRange)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SubTag")
        {
            Destroy(gameObject);
        }

    }
}
