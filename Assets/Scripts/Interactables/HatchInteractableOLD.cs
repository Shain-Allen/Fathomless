using System.Collections;
using UnityEngine;

public class HatchInteractableOLD : MonoBehaviour, IInteractable
{
    PlayerScript playerScript;
    public LayerMask seaMask, subMask;
    public GameObject teleporter;
    public GameObject player;

    //added for auto access to player in prefab
    public SubController controller;

    public bool canLeave;
    public bool innerHatch;
    public bool outerHatch;
    public bool animBlock;
    public float tooCloseDistance;
    public float exitDistance;
    public GameObject detectionPoint;
    public GameObject Ladder;
    public AnimationClip fadeClip;

    void Awake()
    {
        player = controller.Player;
        playerScript = player.GetComponent<PlayerScript>();
        Ladder.SetActive(false);
    }

    public void Interact(GameObject player)
    {
        if (!GameManager.Instance.isFading)
        {
            if (!animBlock)
            {
                if (innerHatch)
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

                if (outerHatch)
                {
                    playerScript.ResetMoveVector();
                    GameManager.Instance.isFading = true;
                    StartCoroutine("Teleport");
                    //TODO: add logic for moving player outside sub
                    //player.transform.position = tP.transform.position;
                    playerScript.GetComponent<Transform>().SetParent(SubController.instance.GetComponent<Transform>());
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
            Debug.Log(hit.distance);
            canLeave = true;
        }
        else
        {
            CanvasController.Instance.DisplayText("I'm too far away from the seafloor.");
            canLeave = false;
        }

        if (Physics.Raycast(detectionPoint.transform.position, detectionPoint.transform.TransformDirection(Vector3.down), out hit, tooCloseDistance))
        {
            Debug.Log("Too Close");
            Debug.Log(hit.distance);
            CanvasController.Instance.DisplayText("The sub is too close to the ground.");
            canLeave = false;
        }
    }

    public IEnumerator Teleport()
    {
        playerScript.frozen = true; //removes player control...
        CanvasController.Instance.PlayQuickFade();
        yield return new WaitForSeconds(fadeClip.length); //for this long
        if (innerHatch)
        {
            playerScript.inSub = !playerScript.inSub;
            GlobalSoundsManager.instance.PlaySplash();
            GlobalSoundsManager.instance.StopSubAmbience();
            GlobalSoundsManager.instance.PlayWaterAmbience();
            Ladder.SetActive(true);
        }
        if (outerHatch)
        {
            playerScript.inSub = !playerScript.inSub;
            GlobalSoundsManager.instance.PlaySubAmbience();
            GlobalSoundsManager.instance.StopWaterAmbience();
            Ladder.SetActive(false);
        }
        GlobalSoundsManager.instance.CutAmbientSounds();
        if (playerScript.inSub)
            player.transform.localRotation = controller.gameObject.transform.rotation;
        else
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerScript.transform.position = teleporter.transform.position; //takes player to this position
        playerScript.frozen = false; //to restore player control
        yield return new WaitForSeconds(fadeClip.length); //waits this long...
        GameManager.Instance.isFading = false;
        //animBlock = false;
    }
}
