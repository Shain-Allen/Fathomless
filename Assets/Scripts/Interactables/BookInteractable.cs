using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class BookInteractable : MonoBehaviour, IInteractable
{
    private PlayerInput playerInput;
    private Fathomless fathomlessInput;
    private void Start()
    {
        CanvasController.Instance.isBooking = false;
    }
    private void Awake()
    {
        fathomlessInput = new Fathomless();
        playerInput = FindObjectOfType<PlayerInput>();
    }
    string[] manualLinesPC = new string[]
    {
    "It's a manual for operating the submarine",
    "Page 1: E key to Interact, Esc to dismount station.",
    "Page 2: W key to increase  thrust, S key for brakes",
    "Page 3: Move mouse to turn the sub left and right.",
    "Page 4: Shift key to ascend, Ctrl key to descend.",
    "The rest of the pages contain dense blueprints. Probably not important."
    }; 
    string[] manualLinesXbox = new string[]
    {
    "It's a manual for operating the submarine",
    "Page 1: B button to Interact, X to dismount station.",
    "Page 2: Left Stick to increase thrust/reverse.",
    "Page 3: Right Stick turns the sub left and right.",
    "Page 4: Left bumber to ascend, trigger to descend.",
    "The rest of the pages contain dense blueprints. Probably not important."
    };
    string[] manualLinesPS = new string[]
    {
    "It's a manual for operating the submarine",
    "Page 1: Circle button to Interact, Square to dismount station.",
    "Page 2: Left Stick to increase thrust/reverse.",
    "Page 3: Right Stick turns the sub left and right.",
    "Page 4: Left bumber to ascend, trigger to descend.",
    "The rest of the pages contain dense blueprints. Probably not important."
    };
    public void Interact(GameObject player)
    {
        if (!CanvasController.Instance.isBooking)
        {
            CanvasController.Instance.isBooking = true;
            if (playerInput.currentControlScheme == fathomlessInput.KeyboardMouseScheme.name)
            {
                CanvasController.Instance.DisplayMoreText(manualLinesPC, 3f, false);
            }
            if (playerInput.currentControlScheme == fathomlessInput.XboxControllerScheme.name)
            {
                CanvasController.Instance.DisplayMoreText(manualLinesXbox, 3f, false);
            }
            if (playerInput.currentControlScheme == fathomlessInput.PlaystationScheme.name)
            {
                CanvasController.Instance.DisplayMoreText(manualLinesPS, 3f, false);
            }
        }
    }

}
