using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Cohesion")]
public class CohesionBehavior : BoidBehavior
{
    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //if no neighbors, return no adjustment
        if(context.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform item in context)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }

    
}
