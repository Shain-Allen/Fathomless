using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteractable : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        print("Interacted with the Sub Hatch");
        //TODO: add logic for moving player outside sub
    }
}
