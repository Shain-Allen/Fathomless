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
    AudioSource waterSound;
    float initialVolume;
    public void Interact(GameObject player)
    {
        if (Manager.Scrap >= 1)
        {
            GlobalSoundsManager.instance.PlayHammer();
            repaired = true;
            Manager.Scrap--;
        }
        else
        {
            if (Random.value < 0.5)
            {
                CanvasController.Instance.DisplayText("I hope I can find something to patch this up...");
            }
            else
            {
                CanvasController.Instance.DisplayText("I've got nothing.");
            }
        }
    }
    private void FixedUpdate()
    {
        if (PlayerScript.instance.inSub)
        {
            waterSound.volume = initialVolume;
        }
        else
        {
            waterSound.volume = 0;
        }
    }
    private void Start()
    {
        initialVolume = waterSound.volume;
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
