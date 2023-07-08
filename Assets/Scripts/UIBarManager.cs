using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarManager : MonoBehaviour
{
    public GameObject playerHealthHandle;
    public GameObject playerOxygenHandle;
    public GameObject playerReloadHandle;

    private void FixedUpdate()
    {
        Vector3 newHandleScale = new Vector3(GameManager.Instance.playerHealth/100, playerHealthHandle.transform.localScale.y, 0);
        playerHealthHandle.transform.localScale = newHandleScale;
        newHandleScale = new Vector3(GameManager.Instance.playerOxygen / 100, playerOxygenHandle.transform.localScale.y, 0);
        playerOxygenHandle.transform.localScale = newHandleScale;
        newHandleScale = new Vector3(GameManager.Instance.playerReloadPercentage / 100, playerReloadHandle.transform.localScale.y, 0);
        playerReloadHandle.transform.localScale = newHandleScale;
    }
}
