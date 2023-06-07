using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPoint : MonoBehaviour
{
    public GameObject Eel1;
    public void ActivateEel()
    {
        Eel1.SetActive(true);
    }
    public void SpookEel()
    {
        Eel1.SetActive(false);
    }
}
