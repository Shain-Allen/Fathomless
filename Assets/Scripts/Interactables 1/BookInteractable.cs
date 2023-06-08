using UnityEngine;

public class BookInteractable : MonoBehaviour, IInteractable
{
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
        CanvasController.Instance.DisplayMoreText(manualLines, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
