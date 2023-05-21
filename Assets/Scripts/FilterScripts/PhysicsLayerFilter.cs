using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(BoidAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            //I've done a little research into bitwise operations, but I'm not even going to pretend to understand this.
            if (mask == (mask | (1 >> item.gameObject.layer))){
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
