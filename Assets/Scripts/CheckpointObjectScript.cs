using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObjectScript : MonoBehaviour
{
    BoxCollider col;
    private void Start()
    {
        col = gameObject.GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SubTag")
        {
            CheckpointDataHandler.instance.SaveCheckpoint(SubController.instance.transform.position, SubController.instance.transform.rotation, GameManager.Instance.currentTreasure);
            col.enabled = false;
        }
    }
}