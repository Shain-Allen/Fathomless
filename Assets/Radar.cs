using UnityEngine;

public class Radar : MonoBehaviour
{
    public Transform target;
    public AudioSource audioSource;
    public float maxVolume = 1f;
    public float minVolume = 0f;
    private void Start()
    {
        InvokeRepeating("AdjustVolume", 0f, audioSource.clip.length);
    }
    private void AdjustVolume()
    {
        Vector3 targetDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        float volume = Mathf.InverseLerp(0f, 180f, angle);
        float pitch = Mathf.InverseLerp(0f, 180f, angle);
        volume = Mathf.Lerp(minVolume, maxVolume, volume);
        pitch = Mathf.Lerp(minVolume, maxVolume, pitch);

        audioSource.volume = volume;
        audioSource.pitch = pitch;
        if (playerScript2.instance.inSub)
            audioSource.Play();
    }
}