using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInteraction : MonoBehaviour, IInteractable
{
    public GameManager GameManager;
    bool pickedUp;
    [Range(1f, 15f)]
    public float floatSpeed;

    public void Interact(GameObject player)
    {
        print("picked up a resource");
        pickedUp = true;
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

            //transform.localScale = transform.localScale/2;

            if (direction.magnitude < 0.3)
            {
                GameManager.Scrap += 1;
                Destroy(gameObject);
            }
        }
    }
}