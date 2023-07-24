using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SubDepthManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI depthText;
    [SerializeField] private Transform depthPoint;
    [SerializeField] private HatchInteractableToOutsub exitHatch;


    private void Update()
    {
        float subDepth = 0;
        
        if (Physics.Raycast(depthPoint.position, Vector3.down, out RaycastHit raycastHit, 100f))
        {
            subDepth = Mathf.Round(Vector3.Distance(raycastHit.point, depthPoint.position));
            depthText.text = subDepth.ToString(CultureInfo.CurrentCulture);
        }

        if (exitHatch.tooCloseDistance > subDepth|| subDepth > exitHatch.exitDistance)
        {
            depthText.color = Color.red;
        }
        else
        {
            depthText.color = Color.black;
        }
    }
}
