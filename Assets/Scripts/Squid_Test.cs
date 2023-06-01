using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid_Test : MonoBehaviour
{
    //This just allows me to see how the animations look while the creature is in motion
    void Update()
    {
        //if using "Floppy" animation, looks best when translate Vector3.forward
        //if using "Striaght" animation, looks best when translate Vector3.back
        transform.Translate(Vector3.back * Time.deltaTime * 2);
    }
}
