using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Boid/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredSpeciesBehavior
{
    public float agentSmoothTime;
    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //if no neighbors, return no adjustment
        if (context != null)
        {
            if (context.Count == 0)
            {
                return Vector3.zero;
            }
            Vector3 cohesionMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                cohesionMove += item.position;
            }
            cohesionMove /= filteredContext.Count;
            //cohesionMove.Normalize();
            //if(filteredContext.Count != 0)
            //    cohesionMove /= filteredContext.Count; //context.Count;
            // else
            //    cohesionMove /= context.Count;
            //Debug.Log(context.Count);

            //EditorApplication.isPaused = true;
            //create offset from agent position
            cohesionMove -= agent.transform.position;
            Debug.Log("before: " + cohesionMove);
            cohesionMove = Vector3.Slerp(agent.transform.forward, cohesionMove, agentSmoothTime);
            Debug.Log("after: " + cohesionMove);
            return cohesionMove;
            
        }
        return Vector3.zero;
    }
}
