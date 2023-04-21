using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotPanelInteractable : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        print("Engaged pilot controls");
        //TODO: add logic for changing to sub pilot controls
    }
}
