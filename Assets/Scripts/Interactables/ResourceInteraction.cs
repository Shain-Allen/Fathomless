using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInteraction : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        print("picked up a resource");
        //TODO: add logic for resource pickup
    }
}
