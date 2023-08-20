using System;
using UnityEngine;

public enum MapPOITypes
{
    Treasure,
    Tunnel,
    Sub
}

public class MapPOI : MonoBehaviour
{
    public MapPOITypes poiType;
    private RectTransform poiRectTransform;
    private float poiIconSize;
    public float GetPoiRectSize() => poiIconSize;

    public Action<Transform, RectTransform> POIDeleted;

    public void RegisterPOIIcon(RectTransform rectTransform) => poiRectTransform = rectTransform;

    private void OnDestroy()
    {
        POIDeleted(transform, poiRectTransform);
        if (poiRectTransform)
            poiRectTransform = null;
    }

    
}
