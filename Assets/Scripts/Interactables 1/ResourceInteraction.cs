using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInteraction : MonoBehaviour, IInteractable
{
    public GameManager GameManager;
    bool pickedUp;
    [Range(1f, 15f)]
    public float floatSpeed;
    public float shrinkSpeed;

    public void Interact(GameObject player)
    {
        if (GameManager.Scrap < GameManager.ScrapMax)
        {
            pickedUp = true;
        }
        else
        {
            CanvasController.Instance.DisplayText("I can't carry any more.");
        }
    }

    private void Start()
    {
        pickedUp = false;
    }

    private void Update()
    {
        if (pickedUp)
        {
            Vector3 direction = GameManager.player.transform.position - transform.position;
            transform.position += direction * floatSpeed * Time.deltaTime;
            transform.Rotate(0,250 * Time.deltaTime, 0);

            transform.localScale = transform.localScale/shrinkSpeed;

            if (direction.magnitude < 0.3)
            {
                GameManager.Scrap += 1;
                Destroy(gameObject);
            }
        }
    }
}
