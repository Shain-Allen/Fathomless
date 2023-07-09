using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconRotationManager : MonoBehaviour
{
    public LayerMask targetLayer;

    private List<Transform> objectsWithLayer = new List<Transform>();
    private void Awake()
    {
        FindIcons();
    }

    private void FindIcons()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (((1 << obj.layer) & targetLayer) != 0)
            {
                objectsWithLayer.Add(obj.transform);
            }
        }
    }

    private void Update()
    {
        float minimapRotationY = -gameObject.transform.rotation.eulerAngles.y;
        foreach (Transform obj in objectsWithLayer)
        {
            Vector3 spin = new Vector3(90, 0, minimapRotationY);
            Quaternion rot = Quaternion.Euler(spin);
            obj.localRotation = rot;
        }
    }
}
