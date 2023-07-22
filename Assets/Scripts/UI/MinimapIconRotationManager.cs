using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapIconRotationManager : MonoBehaviour
{
    public LayerMask targetLayer;
    [SerializeField] private int textureWidth = 512;
    [SerializeField] private int textureHight = 512;
    [SerializeField] private RenderTextureFormat textureFormat = RenderTextureFormat.ARGB32;
    [SerializeField] private FilterMode filterMode = FilterMode.Bilinear;

    [SerializeField] private RawImage mapUI;

    private RenderTexture renderTexture;

    private List<Transform> objectsWithLayer = new List<Transform>();
    private void Awake()
    {
        FindIcons();
        GenerateRenderTexture();
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

    private void GenerateRenderTexture()
    {
        // Create a new RenderTexture with the specified Properties
        renderTexture = new RenderTexture(textureWidth, textureHight, 0, textureFormat);
        
        // Set the Filter mode to the specified mode
        renderTexture.filterMode = filterMode;

        if (TryGetComponent(out Camera mapCam))
        {
            mapCam.targetTexture = renderTexture;
            mapUI.texture = renderTexture;
        }
    }
}
