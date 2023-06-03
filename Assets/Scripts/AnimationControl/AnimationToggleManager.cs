using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToggleManager : MonoBehaviour
{
    public GameObject Submarine;
    public bool Animation_Started = false;
    public bool Animation_Completed = false;

    public PilotPanelInteractable controlPannelScript;

    public void StartAnimation()
    {
        Submarine.GetComponent<SubController>().follow = true;
        Debug.Log("Starting Animation");

        controlPannelScript.controlSub = false;
        controlPannelScript.canControl = false;
    }

    public void EndAnimation()
    {
        Submarine.GetComponent<SubController>().follow = false;
        Submarine.GetComponent<SubController>().resetSubRot = true;
        controlPannelScript.canControl = true;
    }
}
