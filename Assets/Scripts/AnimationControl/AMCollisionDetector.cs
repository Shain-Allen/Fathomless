using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMCollisionDetector : MonoBehaviour
{
    public GameObject AnimationManager;
    public bool HasCollided = false;
    public bool ToggleType = false;
    void OnCollisionEnter(Collision collision)
    {
        if (HasCollided == false)
        {
            Debug.Log(collision.gameObject);
            if (ToggleType == false)
            {
                AnimationManager.GetComponent<AnimationToggleManager>().StartAnimation();
                print("Animation Started");
                HasCollided = true;
            }
            else if (ToggleType == true)
            {
                AnimationManager.GetComponent<AnimationToggleManager>().EndAnimation();
                print("Animation Ended");
                HasCollided = true;
            }
        }
    }
}
