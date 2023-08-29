using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelSpawner : MonoBehaviour
{
    public GameObject Eel;
    

    public void ActivateEel()
    {
        CanvasController.Instance.DisplayText("I sense a disturbance...", true);
        Eel.SetActive(true);
    }
}
