using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInteractable : MonoBehaviour, IInteractable
{
    GameObject playerChar;
    public TurretSystem turretScript;
    public GameObject turretCam;
    public GameObject sub;
    public GameObject FakeTurret;

    public bool controlTurret;


    private void Start()
    {
        playerChar = sub.GetComponent<SubController>().Player;

    }

    public void Interact(GameObject player)
    {
        FakeTurret.SetActive(false);
        controlTurret = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && controlTurret == true)
        {
            FakeTurret.SetActive(true);
            controlTurret = false;
        }

        
    }
    private void FixedUpdate()
    {
        if (controlTurret)
        {
            playerChar.SetActive(false);
            turretCam.SetActive(true);
            turretScript.isTurret = true;
        }

        if (!controlTurret)
        {
            playerChar.SetActive(true);
            turretCam.SetActive(false);
            turretScript.isTurret = false;
        }
    }
}
