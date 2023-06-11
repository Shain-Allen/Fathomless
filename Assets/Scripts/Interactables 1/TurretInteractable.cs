using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInteractable : MonoBehaviour, IInteractable
{
    GameObject playerChar;
    playerScript2 playerScript;
    public PilotPanelInteractable otherStation;
    public TurretSystem turretScript;
    public GameObject turretCam;
    public GameObject sub;
    public GameObject FakeTurret;
    public GameObject playerTurretDropPoint;

    public bool controlTurret;

    interactControls playerInteractController;

    private void Start()
    {
        playerChar = sub.GetComponent<SubController>().Player.gameObject;
        playerScript = playerChar.GetComponent<playerScript2>();
        playerInteractController = playerChar.GetComponent<interactControls>();
    }

    public void Interact(GameObject player)
    {
        FakeTurret.SetActive(false);
        controlTurret = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && controlTurret)
        {
            playerChar.transform.position = playerTurretDropPoint.transform.position;
            FakeTurret.SetActive(true);
            controlTurret = false;
        }

        
    }
    private void FixedUpdate()
    {
        if (controlTurret || otherStation.controlSub)
        {
            playerInteractController.InteractFob.SetActive(false);
            playerChar.SetActive(false);
            turretCam.SetActive(true);
            turretScript.isTurret = true;
        }

        if (!controlTurret && !otherStation.controlSub)
        {
            playerChar.SetActive(true);
            turretCam.SetActive(false);
            turretScript.isTurret = false;
        }
    }
}
