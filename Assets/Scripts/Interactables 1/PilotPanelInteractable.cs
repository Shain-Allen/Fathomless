using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotPanelInteractable : MonoBehaviour, IInteractable
{
    public GameObject playerLockLocation;
    public GameObject player;
    public SubController subScript;
    public GameObject subCam;

    public bool controlSub;


    public void Interact(GameObject player)
    {
        print("Engaged pilot controls");
        //TODO: add logic for changing to sub pilot controls
        controlSub = true; //turns on sub control when player presses e on control pannel
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            controlSub = false;
        }

        if(controlSub)//enables sub control
        {
            player.SetActive(false);
            player.transform.position = playerLockLocation.transform.position;
            subCam.SetActive(true);
            subScript.isSub = true;
        }

        if(!controlSub) //lets the player leave sub control
        {
            //currently stops movement of sub. needs to be fixed so that player will move within sub
            player.SetActive(true);  
            subCam.SetActive(false);
            subScript.isSub = false;
        }
    }
}
