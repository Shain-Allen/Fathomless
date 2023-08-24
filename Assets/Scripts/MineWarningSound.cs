using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineWarningSound : MonoBehaviour
{
    public float interval = 1.0f; // Time between beeps in seconds
    private float timer = 0.0f;
    public bool beeping = false;
    public bool inner;
    public MineWarningSound otherSphere;

    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubTag"))
        {
            beeping = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SubTag"))
        {
            beeping = false;
            if (inner)
            {
                otherSphere.beeping = true;
            }
        }
        
    }

    private void Update()
    {
        if (beeping == true)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                PlayBeep();
                timer = 0.0f;
            }
        }
    }

    private void PlayBeep()
    {
        GlobalSoundsManager.instance.PlayBeep();
    }
}
