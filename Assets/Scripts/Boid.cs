using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public BoidAgent agentPrefab;
    List<BoidAgent> agents = new List<BoidAgent>();
    public BoidBehavior behavior;

    [Range(1, 500)]
    public float startingCount = 250;

    const float agentDensity = 0.5f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(0.1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    public GameObject player;
    public BoidSpawner spawner;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    //just learned about these. They're called properties. Really cool.
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        //At runtime, create variables that are our base values, but squared. (squares and roots are expensive, so we store these ahead of time)
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            BoidAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * agentDensity + transform.position, Quaternion.Euler(new Vector3(1,1,1) * Random.Range(0f, 360f)),
                transform
                );
            newAgent.player = player;
            newAgent.boidSpawner = spawner;
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    private void Update()
    {
        foreach (BoidAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //this fun line visualizes boids by making them purple when they have more neighbors. to a maximum of 6. Highly unoptimized, for demo use only.
            //agent.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.cyan, Color.magenta, context.Count / 6f);

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            if(agent != null)
            {
                if (float.IsNaN(move.x) || float.IsNaN(move.y) || float.IsNaN(move.z))
                {
                    Debug.Log("Boid sucks. Obliterating.");
                    Destroy(agent);
                }
                else
                {
                    agent.Move(move);
                }
            } 
        }
    }
    private void FixedUpdate()
    {
        if(transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    List<Transform> GetNearbyObjects(BoidAgent agent)
    {
        if (agent != null)
        {
            List<Transform> context = new List<Transform>();
            Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
            foreach (Collider c in contextColliders)
            {
                if (c != agent.AgentCollider)
                {
                    context.Add(c.transform);
                }
            }
            return context;
        }
        return null;
        
    }

}
