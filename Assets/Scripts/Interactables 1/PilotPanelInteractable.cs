using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotPanelInteractable : MonoBehaviour, IInteractable
{
    public GameObject playerSubLockLocation;
    public GameObject player;
    public SubController subScript;
    public GameObject subCam;
    public TurretInteractable otherStation;

    interactControls playerInteractController;

    public bool controlSub;
    public bool canControl;

    private void Start()
    {
        playerInteractController = player.GetComponent<interactControls>();
    }

    public void Interact(GameObject player)
    {
        print("Engaged pilot controls");
        //TODO: add logic for changing to sub pilot controls

        if (canControl)
        {
            controlSub = true; //turns on sub control when player presses e on control pannel
        }
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape) && controlSub)
        {
            controlSub = false;
            player.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }

        if(controlSub || otherStation.controlTurret)//enables sub control
        {
            playerInteractController.InteractFob.SetActive(false);
            player.SetActive(false);
            player.transform.position = playerSubLockLocation.transform.position;
            subCam.SetActive(true);
            subScript.isSub = true;
        }

        if(!controlSub && !otherStation.controlTurret) //lets the player leave sub control
        {
            //currently stops movement of sub. needs to be fixed so that player will move within sub
            player.SetActive(true);  
            subCam.SetActive(false);
            subScript.isSub = false;
        }
    }
}
