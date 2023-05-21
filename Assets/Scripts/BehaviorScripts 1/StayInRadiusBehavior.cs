using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : BoidBehavior
{
    public Vector3 center;
    public float radius;

    public override Vector3 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector3.zero;
        }
        //t^2 for "quadratic effect". I dont know what that means.
        return centerOffset * t * t;
    }
}
