using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Bounds bounds;

    [Header("Example")] 
    [SerializeField] private Transform worldTransform;
    [SerializeField] private RectTransform imageTransform;
    
    private RectTransform MapTransform => transform as RectTransform;

    private void Update()
    {
        if (!worldTransform)
        {
            Debug.LogError("you don't have the Player set in the Map's Mapmanager scrip in the inspector");
            return;
        }

        if (!bounds)
        {
            Debug.LogError("you don't have the MiniMapWorldBounds prefab in your scene, this is required for the map to work");
            return;
        }
        
        imageTransform.anchoredPosition = FindInterfacePoint();
    }

    private Vector2 FindInterfacePoint()
    {
        Vector2 normalizedPosition = bounds.FindNormalizedPosition(worldTransform.position);
        return Rect.NormalizedToPoint(MapTransform.rect, normalizedPosition);
    }
}
