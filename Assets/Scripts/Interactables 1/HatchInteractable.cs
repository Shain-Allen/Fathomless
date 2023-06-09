using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteractable : MonoBehaviour, IInteractable
{
    playerScript2 playerScript2;
    public Rigidbody playerRb;
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

    void Awake()
    {
        player = controller.Player;
        playerScript2 = player.GetComponent<playerScript2>();
        playerRb = player.GetComponent<Rigidbody>();
        Ladder.SetActive(false);
    }

    public void Interact(GameObject player)
    {
        if(!animBlock)
        {
            if (innerHatch)
            {
                DetectGround();
                if(canLeave)
                {
                    playerScript2.inSub = !playerScript2.inSub;
                    playerRb.velocity = Vector3.zero;
                    Ladder.SetActive(true);
                    StartCoroutine("Teleport");
                    //TODO: add logic for moving player outside sub
                    //player.transform.position = tP.transform.position;
                }
            
            }
        
            if (outerHatch)
            {
                playerScript2.inSub = !playerScript2.inSub;
                playerRb.velocity = Vector3.zero;
                Ladder.SetActive(false);
                StartCoroutine("Teleport");
                //TODO: add logic for moving player outside sub
                //player.transform.position = tP.transform.position;
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

    IEnumerator Teleport()
    {
        playerScript2.Frozen = true; //removes player control...
        yield return new WaitForSeconds(0.01f); //for this long
        playerScript2.transform.position = teleporter.transform.position; //takes player to this position
        yield return new WaitForSeconds(0.01f); //waits this long...
        if (playerScript2.inSub)
            player.transform.localRotation = controller.gameObject.transform.rotation;
        else
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerScript2.Frozen = false; //to restore player control
    }
}
