using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToggleManager : MonoBehaviour
{
    public GameObject Submarine;
    public bool Animation_Started = false;
    public bool Animation_Completed = false;

    public void StartAnimation()
    {
        Submarine.GetComponent<SubController>().isSub = false;
        Submarine.GetComponent<Animator>().SetBool("Animate?", true);
        Debug.Log("Starting Animation");
    }

    public void EndAnimation()
    {
        Submarine.GetComponent<SubController>().isSub = false;
    }
}
