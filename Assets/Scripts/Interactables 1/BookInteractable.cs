using UnityEngine;

public class BookInteractable : MonoBehaviour, IInteractable
{
    string[] manualLines = new string[]
    {
    "Value 1",
    "Value 2",
    "Value 3"
    };
    public void Interact(GameObject player)
    {
        CanvasController.Instance.DisplayMoreText(manualLines, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
