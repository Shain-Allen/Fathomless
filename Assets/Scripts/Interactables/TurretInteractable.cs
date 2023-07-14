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
    private PlayerInput subInput;

    interactControls playerInteractController;

    private void Start()
    {
        playerChar = sub.GetComponent<SubController>().Player.gameObject;
        playerInteractController = playerChar.GetComponent<interactControls>();
        subInput = sub.GetComponent<PlayerInput>();
    }

    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerInput>().enabled = false;
        subInput.enabled = true;
        FakeTurret.SetActive(false);
        controlTurret = true;
    }
    
    private void FixedUpdate()
    {
        if (controlTurret || otherStation.controlSub)
        {
            playerInteractController.InteractFob.SetActive(false);
            playerChar.SetActive(false);
            if (controlTurret)
            {
                turretCam.SetActive(true);
                turretScript.isTurret = true;
            }
        }

        if (!controlTurret && !otherStation.controlSub)
        {
            playerChar.SetActive(true);
            turretCam.SetActive(false);
            turretScript.isTurret = false;
        }
    }

    private void OnLeavePost(InputValue inputValue)
    {
        if (controlTurret)
        {
            subInput.enabled = false;
            playerChar.GetComponent<PlayerInput>().enabled = true;
            playerChar.GetComponent<PlayerScript>().ResetMoveVector();
            playerChar.transform.position = playerTurretDropPoint.transform.position;
            FakeTurret.SetActive(true);
            controlTurret = false;
        }
    }
}
