using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactControls : MonoBehaviour
{

    public LayerMask interactables;
    public Camera MainCamera;
    private RaycastHit raycast;
    private float raycastRange = 3f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out raycast, raycastRange, interactables))
            {
                switch (raycast.collider.gameObject.tag)
                {
                    case "hatchTrigger":
                        Debug.Log("This is the hatch trigger.");
                        break;
                    case "resources":
                        Debug.Log("This is a resource.");
                        break;
                    case "subPilot":
                        Debug.Log("This is the submarine controller.");
                        break;
                    case "damageRepair":
                        Debug.Log("This is a damaged part of the ship.");
                        break;
                    case "turrets":
                        Debug.Log("This is a turret.");
                        break;
                    default:
                        Debug.Log("This object has no tag.");
                        break;
                }
            }
        }
    }
}
