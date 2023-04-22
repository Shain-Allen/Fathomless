 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Avoidance")]
public class AvoidanceBehavior : BoidBehavior
{
    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < boid.SquareAvoidanceRadius)
            {
                nAvoid++;
            avoidanceMove += agent.transform.position - item.position;
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;
        return avoidanceMove;
    }
}
