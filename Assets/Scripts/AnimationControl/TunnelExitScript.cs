using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelExitScript : MonoBehaviour
{
    public BoxCollider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "SubTag")
        {
            //if sub exits cave, become a wall.
            col.isTrigger = false;
        }
    }
}
