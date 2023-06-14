using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrackBlockwall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("SubTag"))
        {
            CanvasController.Instance.DisplayText("Not happening. not even with my sub.");
        }
    }
}
