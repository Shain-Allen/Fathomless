using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactControls : MonoBehaviour
{
    public Camera MainCamera;
    private RaycastHit raycast;
    private float raycastRange = 5f;
    public GameObject InteractFob;

    private Fathomless fathomlessInputActions;

    private void Awake()
    {
        fathomlessInputActions = new Fathomless();
        fathomlessInputActions.Player_AMap.Enable();
    }

    private void OnEnable()
    {
        fathomlessInputActions.Player_AMap.Look.performed += OnLook;
        fathomlessInputActions.Player_AMap.Look.canceled += OnLook;
        fathomlessInputActions.Player_AMap.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        fathomlessInputActions.Player_AMap.Look.performed -= OnLook;
        fathomlessInputActions.Player_AMap.Look.canceled -= OnLook;
        fathomlessInputActions.Player_AMap.Interact.performed -= OnInteract;
    }

    private void Start()
    {
        InteractFob = CanvasController.Instance.pointer;
        InteractFob.SetActive(false);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out raycast, raycastRange))
        {
            raycast.collider.gameObject.GetComponent<IInteractable>()?.Interact(gameObject);

            PlayerScript.instance.Feet.Stop();
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        
        
    }
    private void FixedUpdate()
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
