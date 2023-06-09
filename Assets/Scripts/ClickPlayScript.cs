using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickPlayScript : MonoBehaviour
{
    public AnimationClip fadeClip;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            StartCoroutine(WaitForFade());
        }
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(fadeClip.length);
        SceneManager.LoadScene("MainScene");
    }
}
