using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Cohesion")]
public class CohesionBehavior : FilteredSpeciesBehavior
{
    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //if no neighbors, return no adjustment
        if(context.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }

    
}
