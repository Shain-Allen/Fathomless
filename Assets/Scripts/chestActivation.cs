using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestActivation : MonoBehaviour
{
    public Animator anim;
    public bool openChest;

    private void Update()
    {
        if (openChest)
        {
            anim.SetBool("isOpen", true);

            //do extra chest stuff here when you want the chest to open
        }
        else
        {
            anim.SetBool("isOpen", false);
            //will close chest when bool is set to false.
        }
    }
}
