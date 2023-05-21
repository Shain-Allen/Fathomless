 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredSpeciesBehavior
{
    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        if (context != null)
        {
            //if no neighbors, return no adjustment
            if (context.Count == 0)
            {
                return Vector3.zero;
            }
            Vector3 avoidanceMove = Vector3.zero;
            int nAvoid = 0;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                Vector3 closestPoint = item.gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position);
                if (Vector3.SqrMagnitude(item.position - agent.transform.position) < boid.SquareAvoidanceRadius)
                {
                    nAvoid++;
                    avoidanceMove += agent.transform.position - closestPoint;
                }
            }
            if (nAvoid > 0)
                avoidanceMove /= nAvoid;
            return avoidanceMove;
        }
        return Vector3.zero;
    }
}
