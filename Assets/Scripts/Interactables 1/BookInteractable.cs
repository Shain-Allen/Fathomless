using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookInteractable : MonoBehaviour, IInteractable
{
    private void Start()
    {
        CanvasController.Instance.isBooking = false;
    }
    string[] manualLines = new string[]
    {
    "It's a manual for operating the submarine",
    "Page 1: E key to Interact, Esc to dismount station.",
    "Page 2: W key to increase  thrust, S key for breaks",
    "Page 3: A and D keys turn the sub left and right.",
    "Page 4: Shift key to ascend, Ctrl key to descend.",
    "The rest of the pages contain dense blueprints. Probably not important."
    };
    public void Interact(GameObject player)
    {
        if (!CanvasController.Instance.isBooking)
        {
            CanvasController.Instance.isBooking = true;
            CanvasController.Instance.DisplayMoreText(manualLines, 3f);
        }
    }

}
