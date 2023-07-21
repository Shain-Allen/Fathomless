using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float interpolationValue;

    void Update()
    {
        // Ensure the interpolation value stays between 0 and 1
        interpolationValue = Mathf.Clamp01(interpolationValue);

        // Calculate the new position based on the interpolation value
        Vector3 newPosition = Vector3.Lerp(pointA.position, pointB.position, interpolationValue);

        // Apply the new position to the object
        transform.position = newPosition;
    }
}
