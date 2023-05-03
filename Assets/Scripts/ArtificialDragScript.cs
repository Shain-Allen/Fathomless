using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialDragScript : MonoBehaviour
{
    Rigidbody playerRb;
    Rigidbody subRb;
    playerScript2 playerScript;
    Vector3 targetVel;

    public float drag;
    void Start()
    {
        playerScript = GetComponent<playerScript2>();
        subRb = playerScript.sub.GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerScript.isGrounded)
        {
            if (playerScript.inSub)
            {//player is in sub
                targetVel = subRb.velocity;
            }
            else
            {//player in water
                targetVel = Vector3.zero;
            }
        }
        
        playerRb.velocity = Vector3.Lerp(playerRb.velocity, targetVel, drag);
    }
}
