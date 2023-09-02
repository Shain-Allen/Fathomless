using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 3.0f;
    public float amplitude = 0.5f;
    public float frequency = .3f;

    public GameObject jellyParticles;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
    private void OnTriggerEnter(Collider other)
    {
            GameObject particle = Instantiate(jellyParticles, null, true);
            particle.transform.position = transform.position;
            Destroy(this.gameObject);
    }
}
