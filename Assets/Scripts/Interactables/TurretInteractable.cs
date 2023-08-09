using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretInteractable : MonoBehaviour, IInteractable
{
    private GameObject playerChar;
    public PilotPanelInteractable otherStation;
    public TurretSystem turretScript;
    public GameObject turretCam;
    public GameObject sub;
    public GameObject FakeTurret;
    public GameObject playerTurretDropPoint;

    public bool controlTurret;

    interactControls playerInteractController;

    private static TurretInteractable instance;
    
    public static TurretInteractable Instance => instance;

    private Fathomless fathomlessInputActions;

    private void Awake()
    {
        instance = this;
        
        fathomlessInputActions = new Fathomless();
        fathomlessInputActions.Player_AMap.Enable();
    }

    private void Start()
    {
        playerChar = sub.GetComponent<SubController>().Player.gameObject;
        playerInteractController = playerChar.GetComponent<interactControls>();
    }

    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerScript>().frozen = true;
        FakeTurret.SetActive(false);
        controlTurret = true;
    }
    
    private void FixedUpdate()
    {
        if (controlTurret || otherStation.controlSub)
        {
            playerInteractController.InteractFob.SetActive(false);
            if (controlTurret)
            {
                turretCam.SetActive(true);
                turretScript.isTurret = true;
            }
        }

        if (!controlTurret && !otherStation.controlSub)
        {
            turretCam.SetActive(false);
            turretScript.isTurret = false;
        }
    }

    public void OnLeavePost()
    {
        if (controlTurret)
        {
            playerChar.GetComponent<PlayerScript>().ResetMoveVector();
            playerChar.transform.position = playerTurretDropPoint.transform.position;
            FakeTurret.SetActive(true);
            controlTurret = false;
        }
    }
}
