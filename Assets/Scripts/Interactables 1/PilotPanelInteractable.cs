using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotPanelInteractable : MonoBehaviour, IInteractable
{
    public GameObject playerLockLocation;
    public GameObject player;
    public SubController subScript;
    public GameObject subCam;
         

    public void Interact(GameObject player)
    {
        print("Engaged pilot controls");
        //TODO: add logic for changing to sub pilot controls

        player.SetActive(false);
        player.transform.position = playerLockLocation.transform.position;
        subCam.SetActive(true);
        subScript.isSub = true;


    }
}
