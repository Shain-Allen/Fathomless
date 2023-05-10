using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInteractable : MonoBehaviour, IInteractable
{
    public GameObject playerLockLocation;
    public GameObject playerChar;
    public TurretSystem turretScript;
    public GameObject turretCam;

    public bool controlTurret;

    public void Interact(GameObject player)
    {
        print("Turret Activated");
        //TODO: add logic for changing to sub pilot controls
        controlTurret = true; //turns on sub control when player presses e on control pannel
        turretCam.SetActive(true);
        playerChar = player;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && controlTurret == true)
        {
            controlTurret = false;
            turretCam.SetActive(false);
            playerChar.transform.position = playerLockLocation.transform.position;
        }

        if (controlTurret)//enables sub control
        {
            playerChar.SetActive(false);
            turretCam.SetActive(true);
            turretScript.isTurret = true;
        }

        if (!controlTurret) //lets the player leave sub control
        {
            //currently stops movement of sub. needs to be fixed so that player will move within sub
            playerChar.SetActive(true);
            turretCam.SetActive(false);
            turretScript.isTurret = false;
        }
    }
}
