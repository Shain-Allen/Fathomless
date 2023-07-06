using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Alignment")]
public class AlignmentBehavior : FilteredSpeciesBehavior
{
    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        if (context != null)
        {
            //if no neighbors, maintain current allignment
            if (context.Count == 0)
            {
                return agent.transform.forward;
            }
            Vector3 alignmentMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                alignmentMove += item.transform.forward;
            }
            alignmentMove /= filteredContext.Count;
            alignmentMove.Normalize();
            //if (filteredContext.Count != 0)
            //    alignmentMove /= filteredContext.Count; //context.Count;
            //else
            //    alignmentMove /= context.Count;
            //Debug.Log(alignmentMove);

            return alignmentMove;
        }
        return Vector3.zero;
    }

}
