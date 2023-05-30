using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInteractable : MonoBehaviour, IInteractable
{
    public GameObject playerChar;
    public TurretSystem turretScript;
    public GameObject turretCam;

    public bool controlTurret;

    public void Interact(GameObject player)
    {
        controlTurret = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && controlTurret == true)
        {
            controlTurret = false;
        }

        
    }
    private void FixedUpdate()
    {
        if (controlTurret)
        {
            playerChar.transform.GetChild(1).gameObject.SetActive(false);
            turretCam.SetActive(true);
            turretScript.isTurret = true;
        }

        if (!controlTurret)
        {
            playerChar.transform.GetChild(1).gameObject.SetActive(true);
            turretCam.SetActive(false);
            turretScript.isTurret = false;
        }
    }
}
