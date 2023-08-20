using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Bounds bounds;

    [Header("Points of interest")] 
    [SerializeField] private GameObject mapIconPrefab;
    [SerializeField] private List<MapPOI> pointsOfIntrestWT;
    [SerializeField] private List<RectTransform> pointsOfIntrestsRT;

    [SerializeField] private Sprite[] mapIcons;
    
    private RectTransform MapTransform => transform as RectTransform;
    
    private void OnValidate()
    {
        pointsOfIntrestWT = new List<MapPOI>();
        
        foreach (MapPOI icon in FindObjectsOfType<MapPOI>())
        {
            pointsOfIntrestWT.Add(icon);
        }
    }

    private void Awake()
    {
        if (pointsOfIntrestWT == null)
        {
            Debug.LogWarning("There are no Points of interests registered on the map");
            return;
        }

        if (bounds == null)
        {
            Debug.LogError("your missing the Bounds Object in the scene, please add one");
        }

        foreach (MapPOI mapPoi in pointsOfIntrestWT)
        {
            GameObject newMapIcon = Instantiate(mapIconPrefab, transform);
            newMapIcon.GetComponent<Image>().sprite = mapIcons[(int)mapPoi.poiType];
            mapPoi.RegisterPOIIcon(newMapIcon.transform as RectTransform);
            pointsOfIntrestsRT.Add(newMapIcon.transform as RectTransform);
            mapPoi.POIDeleted += POIDeleted;
        }

        pointsOfIntrestsRT[pointsOfIntrestWT.IndexOf(pointsOfIntrestWT.Find(poi => poi.poiType == MapPOITypes.Sub))].transform.SetAsLastSibling();
    }

    private void Update()
    {
        if (pointsOfIntrestWT.Count == 0 || pointsOfIntrestsRT.Count == 0)
        {
            return;
        }    
        
        if (pointsOfIntrestsRT.Count != pointsOfIntrestWT.Count)
        {
            Debug.LogWarning("Error: for some reason there is not the same amount of POI's as map icons");
            return;
        }
        
        for (int i = 0; i < pointsOfIntrestWT.Count; i++)
        {
            pointsOfIntrestsRT[i].anchoredPosition = FindInterfacePoint(pointsOfIntrestWT[i].transform);
        }
    }

    private Vector2 FindInterfacePoint(Transform objectTransform)
    {
        Vector2 normalizedPosition = bounds.FindNormalizedPosition(objectTransform.position);
        
        return Rect.NormalizedToPoint(MapTransform.rect, normalizedPosition);
    }
    
    private void POIDeleted(Transform poiTransform, RectTransform poiRT)
    {
        pointsOfIntrestWT.Remove(poiTransform.GetComponent<MapPOI>());
        pointsOfIntrestsRT.Remove(poiRT);
        poiTransform.GetComponent<MapPOI>().POIDeleted -= POIDeleted;
        
        if (poiRT)
            Destroy(poiRT.gameObject);
    }
}
