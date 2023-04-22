using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRepairInteractable : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        print("Interacted with damaged part of sub");
        //TODO: add logic for fixing damage
    }
}
