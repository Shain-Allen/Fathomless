using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisHarpoonAwake : MonoBehaviour
{
    private void OnEnable()
    {
        GlobalSoundsManager.instance.PlayReload();
    }
}
