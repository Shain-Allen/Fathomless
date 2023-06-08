using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToggleManager : MonoBehaviour
{
    public GameObject Submarine;
    public bool Animation_Started = false;
    public bool Animation_Completed = false;
    public PilotPanelInteractable controlPannelScript;
    //public HatchInteractable hatchScript;
    public GameObject innerHatch;

    public void StartAnimation(string AnimName)
    {
        Submarine.GetComponent<SubController>().follow = true;
        Submarine.GetComponent<SubController>().AnimationName = AnimName;
        Debug.Log("Starting Animation");

        controlPannelScript.controlSub = false;
        controlPannelScript.canControl = false;
        innerHatch.GetComponent<HatchInteractable>().animBlock = true;
    }

    public void EndAnimation()
    {
        //hatchScript.animBlock = false;
        innerHatch.GetComponent<HatchInteractable>().animBlock = false;
        Submarine.GetComponent<SubController>().follow = false;
        Submarine.GetComponent<SubController>().resetSubRot = true;
        controlPannelScript.canControl = true;
        
    }
}
