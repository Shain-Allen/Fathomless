using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerScript : MonoBehaviour
{
    public GameObject followPoint;
    public GameObject exitBlock;
    public TunnelExitScript exitScript;
    public Animation followPointAnim;

    private void Start()
    {
        exitScript = exitBlock.GetComponent<TunnelExitScript>();
        followPointAnim = followPoint.GetComponent<Animation>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SubTag")
        {
            StartCombatSequence();
        }
    }
    public void StartCombatSequence()
    {
        exitScript.col.isTrigger = true;
        SubController.instance.followPoint = followPoint;
        PilotPanelInteractable.Instance.kickPlayerOut();

        followPointAnim.Play();
    }

    public void EndCombatSequence()
    {
        SubController.instance.follow = false;
        SubController.instance.resetSubRot = true;
        PilotPanelInteractable.Instance.canControl = true;
        HatchInteractableToOutsub.instance.animBlock = false;
        
    }
}
