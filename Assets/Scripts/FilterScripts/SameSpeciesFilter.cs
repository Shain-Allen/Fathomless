using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Species Filter/Same Species")]
public class SameSpeciesFilter : ContextFilter
{
    public override List<Transform> Filter(BoidAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            BoidAgent itemAgent = item.GetComponent<BoidAgent>();
            if (itemAgent != null && itemAgent.AgentSpecies == agent.AgentSpecies)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
