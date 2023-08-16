using UnityEngine;
using UnityEngine.InputSystem;

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

    public GameObject sub;
    public float tooCloseDistance;
    public float exitDistance;
    public GameObject detectionPoint;

    public GameObject resetPoint;
    GameObject InteractFob;

    private static PilotPanelInteractable instance;
    public static PilotPanelInteractable Instance => instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        playerInteractController = player.GetComponent<interactControls>();
        InteractFob = playerInteractController.InteractFob;
        player = PlayerScript.instance.gameObject;
    }

    public void Interact(GameObject player)
    {
        print("Engaged pilot controls");
        //TODO: add logic for changing to sub pilot controls

        if (canControl)
        {
            controlSub = true; //turns on sub control when player presses e on control pannel
            player.GetComponent<PlayerScript>().frozen = true;
        }
    }

    private void Update()
    {
        if (controlSub || otherStation.controlTurret)//enables sub control
        {
            InteractFob.SetActive(false);
            player.transform.position = playerSubLockLocation.transform.position;
            if (controlSub)
            {
                subCam.SetActive(true);
                subScript.isSub = true;
            }
        }

        if (!controlSub && !otherStation.controlTurret) //lets the player leave sub control
        {
            //currently stops movement of sub. needs to be fixed so that player will move within sub
            subCam.SetActive(false);
            subScript.isSub = false;
        }
    }

    public void OnLeavePost ()
    {
        if (controlSub)
        {
            player.GetComponent<PlayerScript>().ResetMoveVector();
            controlSub = false;
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    public void kickPlayerOut() 
    {
        player.GetComponent<PlayerScript>().ResetMoveVector();
        controlSub = false;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        SubController.instance.follow = true;
        canControl = false;
        HatchInteractableToOutsub.instance.animBlock = true;
    }

}
