using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("TreasureGET");
            GlobalSoundsManager.instance.PlayTreasureCollect();
            Destroy(gameObject);

        }
    }
}
