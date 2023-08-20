using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float boundsSize = 25f;
    [SerializeField] private float SubBoundSize = 50f;
    
    [SerializeField] private Transform lower;
    [SerializeField] private Transform upper;


    private void OnValidate()
    {
        if (target) return;
        
        Debug.Log("Attempting to default to Player transform as minimap center target due to field being null");
        target = FindObjectOfType<PlayerScript>().GetComponent<Transform>();

        if (!target)
        {
            Debug.LogError("No player found in the scene. minimap doesn't have a center target assigned yet so please assign one");
            return;
        }
            
        Debug.Log("Player object found and set as minimap center target");
    }

    private void Awake()
    {
        SetBoundDist(boundsSize);
    }

    private void Update()
    {
        transform.parent.position = new Vector3(target.position.x, 0, target.position.z);

        if (PlayerScript.Instance.inSub)
        {
            SetBoundDist(SubBoundSize);
        }
        else
        {
            SetBoundDist(boundsSize);
        }
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

    private void SetBoundDist(float dist)
    {
        lower.localPosition = new Vector3(-dist, 0, -dist);
        upper.localPosition = new Vector3(dist, 0, dist);
    }
}
