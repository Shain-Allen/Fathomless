using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteractable : MonoBehaviour, IInteractable
{
    playerScript2 playerScript2;
    public LayerMask seaMask, subMask;
    public GameObject teleporter;
    public GameObject player;

    void Awake()
    {
        playerScript2 = player.GetComponent<playerScript2>();
    }

    public void Interact(GameObject player)
    {
        playerScript2.inSub = !playerScript2.inSub;
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
        playerScript2.Frozen = false; //to restore player control
    }
}
