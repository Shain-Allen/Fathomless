using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerActivate : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubTag"))
        {
            Debug.Log("The fish is on the hunt!");
        }
    }
}
