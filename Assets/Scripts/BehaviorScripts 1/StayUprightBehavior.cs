using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/StayUpright")]
public class StayUprightBehavior : FilteredSpeciesBehavior
{
    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        if (context != null)
        {
            Vector3 UpRightAdjustment = Vector3.zero;
            //if (agent.transform.rotation.x > 25)
            //{
                UpRightAdjustment.x -= 1;
            //}
            //if (agent.transform.rotation.x < -25)
            //{
                UpRightAdjustment.x += 1;
            //}
            //if (agent.transform.rotation.z > 25 || agent.transform.rotation.z < -25)
            //{
                UpRightAdjustment.z = 0;
            //}
            //UpRightAdjustment.y = 0;
            return UpRightAdjustment;
        }
        return Vector3.zero;
    }
}
