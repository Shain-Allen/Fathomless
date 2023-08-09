using System;
using UnityEngine;

public enum MapPOITypes
{
    Treasure,
    Tunnel
}

public class MapPOI : MonoBehaviour
{
    public MapPOITypes poiType;
    private RectTransform poiRectTransform;

    public Action<Transform, RectTransform> POIDeleted;

    public void RegisterPOIIcon(RectTransform rectTransform) => poiRectTransform = rectTransform;

    private void OnDestroy()
    {
        POIDeleted(transform, poiRectTransform);
    }
}
