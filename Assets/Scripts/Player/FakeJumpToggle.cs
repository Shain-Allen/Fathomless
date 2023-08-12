using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeJumpToggle : MonoBehaviour
{
    public void FakeJumpReset()
    {
        PlayerScript.instance.canFakeJump = true;
    }
}
