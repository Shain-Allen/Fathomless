using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField] private Transform lower;
    [SerializeField] private Transform upper;

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
}
