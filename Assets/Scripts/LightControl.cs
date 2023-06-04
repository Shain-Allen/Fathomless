using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public GameObject cam;
    public float lagSpeed;
    private Quaternion targetRotation;

    // Update is called once per frame
    void Update()
    {
        targetRotation = Quaternion.Slerp(targetRotation,  cam.transform.rotation, lagSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}
