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

    void Awake()
    {
        player = controller.Player;
        playerScript2 = player.GetComponent<playerScript2>();
        playerRb = player.GetComponent<Rigidbody>();
       
    }

    public void Interact(GameObject player)
    {
        playerScript2.inSub = !playerScript2.inSub;
        playerRb.velocity = Vector3.zero;
        StartCoroutine("Teleport");
        //TODO: add logic for moving player outside sub
        //player.transform.position = tP.transform.position;
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
