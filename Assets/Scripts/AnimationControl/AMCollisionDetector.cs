using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMCollisionDetector : MonoBehaviour
{
    public GameObject AnimationManager;
    public bool HasCollided = false;
    public string AnimationName = "";
    public bool EndAnimation = false;
    void OnTriggerEnter(Collider collision)
    {
        if (HasCollided == false)
        {
            Debug.Log(collision.gameObject);
            if (EndAnimation == false)
            {
                AnimationManager.GetComponent<AnimationToggleManager>().StartAnimation(AnimationName);
                print("Animation Started");
                HasCollided = true;
            }
            else if (EndAnimation == true)
            {
                AnimationManager.GetComponent<AnimationToggleManager>().EndAnimation();
                print("Animation Ended");
                HasCollided = true;

            }
        }
    }
}
