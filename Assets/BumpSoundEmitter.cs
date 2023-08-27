using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpSoundEmitter : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject emitter;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            PlayCollisionSound(collision.GetContact(0).point);
    }

    private void PlayCollisionSound(Vector3 collisionPoint)
    {
        emitter.transform.position = collisionPoint;
        float randomPitch = 1f + Random.Range(-1, 1);
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }
}
