using System.Collections;
using UnityEngine;

public class HatchInteractableToOutsub : MonoBehaviour, IInteractable
{
    PlayerScript playerScript;
    public GameObject teleporter;
    public GameObject player;

    //added for auto access to player in prefab
    public SubController controller;

    public bool canLeave;
    public bool animBlock;
    public float tooCloseDistance;
    public float exitDistance;
    public GameObject detectionPoint;
    public GameObject Ladder;
    public AnimationClip fadeClip;

    public static HatchInteractableToOutsub instance;
    public static HatchInteractableToOutsub Instance => instance;
    void Awake()
    {
        instance = this;
        player = controller.Player;
        playerScript = player.GetComponent<PlayerScript>();
        Ladder.SetActive(false);
    }

    public void Interact(GameObject player)
    {
        if (!GameManager.Instance.isFading && (!PilotPanelInteractable.Instance.controlSub && !TurretInteractable.Instance.controlTurret))
        {
            if (!animBlock)
            {
                    DetectGround();
                    if (canLeave)
                    {
                        playerScript.ResetMoveVector();
                        GameManager.Instance.isFading = true;
                        StartCoroutine("Teleport");
                        //TODO: add logic for moving player outside sub
                        //player.transform.position = tP.transform.position;
                        
                        playerScript.GetComponent<Transform>().SetParent(null);
                    }


            }
        }
    }

    public void DetectGround()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(detectionPoint.transform.position, detectionPoint.transform.TransformDirection(Vector3.down), out hit, exitDistance))
        {
            Debug.Log("Ground, Can Leave");
            canLeave = true;
        }
        else
        {
            CanvasController.Instance.DisplayText("I'm too far away from the seafloor.", true);
            canLeave = false;
        }

        if (Physics.Raycast(detectionPoint.transform.position, detectionPoint.transform.TransformDirection(Vector3.down), out hit, tooCloseDistance))
        {
            Debug.Log("Too Close");
            CanvasController.Instance.DisplayText("The sub is too close to the ground.", true);
            canLeave = false;
        }
    }

    public IEnumerator Teleport()
    {
        playerScript.frozen = true; //removes player control...
        CanvasController.Instance.PlayQuickFade();
        PlayerScript.instance.Feet.Stop();
        yield return new WaitForSeconds(fadeClip.length); //for this long

            playerScript.inSub = !playerScript.inSub;
            GlobalSoundsManager.instance.PlaySplash();
            GlobalSoundsManager.instance.StopSubAmbience();
            GlobalSoundsManager.instance.PlayWaterAmbience();
            Ladder.SetActive(true);
        PlayerScript.instance.HarpoonGun.SetActive(true);

        GlobalSoundsManager.instance.CutAmbientSounds();

            player.transform.localRotation = controller.gameObject.transform.rotation;

        playerScript.transform.position = teleporter.transform.position; //takes player to this position
        playerScript.frozen = false; //to restore player control
        yield return new WaitForSeconds(fadeClip.length); //waits this long...
        PlayerScript.instance.Feet.Stop();
        GameManager.Instance.isFading = false;
        //animBlock = false;
    }
}
