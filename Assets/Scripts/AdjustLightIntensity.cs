using UnityEngine;

public class AdjustLightIntensity : MonoBehaviour
{
    public Light targetLight;
    public Transform player;
    public float distanceThreshold = 50f; // Adjust this value to set the distance threshold
    [Range(0f,1f)] public float minIntensityPercentage = 0.5f; // Adjust this value to set the minimum intensity percentage

    private float initialIntensity;

    private void Start()
    {
        player = PlayerScript.instance.transform;
        targetLight = GetComponent<Light>();
        if (targetLight == null)
        {
            Debug.LogError("Target Light not assigned!");
            enabled = false;
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player Transform not assigned!");
            enabled = false;
            return;
        }

        initialIntensity = targetLight.intensity;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(targetLight.transform.position, player.position);
        if (distanceToPlayer < distanceThreshold)
        {
            float intensityFactor = Mathf.Clamp01(1f - (distanceToPlayer / distanceThreshold));

            float minIntensity = initialIntensity * minIntensityPercentage;

            float adjustedIntensity = Mathf.Lerp(initialIntensity, minIntensity, intensityFactor);

            targetLight.intensity = adjustedIntensity;
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log(targetLight.intensity);
            }
        }
        else
        {
            targetLight.intensity = initialIntensity;
        }
    }
}
