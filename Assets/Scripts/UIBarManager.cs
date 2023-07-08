using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarManager : MonoBehaviour
{
    public GameObject playerHealthHandle;
    public GameObject playerOxygenHandle;

    private void FixedUpdate()
    {
        Vector3 newHandleScale = new Vector3(GameManager.Instance.playerHealth/100, playerHealthHandle.transform.localScale.y, 0);
        playerHealthHandle.transform.localScale = newHandleScale;
    }
}
