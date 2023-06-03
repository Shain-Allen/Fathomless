using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRepairInteractable : MonoBehaviour, IInteractable
{
    public GameManager Manager;
    public bool repaired;
    public float shrinkSpeed;
    public SubDamageManager SubDamageManager;
    public int DamageProtrusionIndex = 0;
    public void Interact(GameObject player)
    {
        Debug.Log("INTERACTED");
        if (Manager.Scrap >= 1)
        {
            repaired = true;
            Manager.Scrap--;
        }
    }

    public void Update()
    {
        if (repaired)
        {
            transform.localScale = transform.localScale / shrinkSpeed;
            if (transform.localScale.x < 0.05f)
            {
                print("destroying");
                SubDamageManager.DamagedSpots[DamageProtrusionIndex] = false;
                Destroy(this.gameObject);
            }
        }
    }
}
