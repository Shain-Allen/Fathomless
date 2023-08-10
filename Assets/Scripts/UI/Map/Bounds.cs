using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float boundsSize = 25f;
    
    [SerializeField] private Transform lower;
    [SerializeField] private Transform upper;


    private void Awake()
    {
        SetBoundDist(boundsSize);
    }

    private void Update()
    {
        transform.parent.position = new Vector3(target.position.x, 0, target.position.z);
    }

    public Vector2 FindNormalizedPosition(Vector3 position)
    {
        if (!lower || !upper)
        {
            Debug.LogError("you don't have the Upper and/or lower Bound set in the inspector");
            return Vector2.zero;
        }
        
        float xPosition = Mathf.InverseLerp(lower.position.x, upper.position.x, position.x);
        float zPosition = Mathf.InverseLerp(lower.position.z, upper.position.z, position.z);

        return new Vector2(xPosition, zPosition);
    }

    public void SetBoundDist(float dist)
    {
        lower.localPosition = new Vector3(-dist, 0, -dist);
        upper.localPosition = new Vector3(dist, 0, dist);
    }
}
