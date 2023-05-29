using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactControls : MonoBehaviour
{
    public Camera MainCamera;
    private RaycastHit raycast;
    private float raycastRange = 1000f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Button Press");
            if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out raycast, raycastRange))
            {
                raycast.collider.gameObject.GetComponent<IInteractable>()?.Interact(gameObject);
                Debug.Log(raycast.collider.gameObject);
            }
        }
    }
}
