using UnityEngine;
using UnityEngine.InputSystem;

public class interactControls : MonoBehaviour
{
    public Camera MainCamera;
    private RaycastHit raycast;
    private float raycastRange = 5f;
    public GameObject InteractFob;

    private void Start()
    {
        InteractFob.SetActive(false);
    }

    private void OnInteract(InputValue inputValue)
    {
        //Debug.Log("Button Press");
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out raycast, raycastRange))
        {
            raycast.collider.gameObject.GetComponent<IInteractable>()?.Interact(gameObject);
        }
    }

    private void OnLook(InputValue inputValue)
    {
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out raycast, raycastRange))
        {
            if (raycast.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                InteractFob.SetActive(true);
            }
            else
            {
                InteractFob.SetActive(false);
            }
        }
        else
        {
            InteractFob.SetActive(false);
        }
    }
}
