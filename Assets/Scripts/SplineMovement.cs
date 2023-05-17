using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;
    [SerializeField] private Transform PointC;
    [SerializeField] private Transform PointAB;
    [SerializeField] private Transform PointBC;
    [SerializeField] private Transform PointAB_BC;

    private float interpolateAmount;

    void Update()
    {
        interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;
        PointAB.position = Vector3.Lerp(PointA.position, PointB.position, interpolateAmount);
        PointBC.position = Vector3.Lerp(PointB.position, PointC.position, interpolateAmount);
        PointAB_BC.position = Vector3.Lerp(PointAB.position, PointBC.position, interpolateAmount);
    }
}
