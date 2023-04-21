using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    Vector3 direction;
    [Range(0, 100)]
    public float swimSpeed;
    public Rigidbody rb;
    [Range(0, 100)]
    public float noiseSpeed;
    [Range(0, 100)]
    public float noiseScale;
    public enum State
    {
        Idle, Pursue, Attack
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 toPlayer = Player.transform.position - transform.position;
        swimSpeed = toPlayer.magnitude;
        //adds noise for more organic movement
        Vector3 noise = new Vector3(Mathf.PerlinNoise(Time.time * noiseSpeed, 0), Mathf.PerlinNoise(0, Time.time * noiseSpeed), Mathf.PerlinNoise(Time.time * noiseSpeed, Time.time * noiseSpeed));
        noise -= Vector3.one * 0.5f;
        noise *= noiseScale;
        noise *= toPlayer.magnitude;
        toPlayer.Normalize();
        direction = toPlayer * swimSpeed + noise;
        rb.velocity = direction;
        rb.rotation = Quaternion.LookRotation(direction);
    }
}
