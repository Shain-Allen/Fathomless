using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickPlayScript : MonoBehaviour
{
    public AnimationClip fadeClip;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            animator.SetTrigger("FadeToBlack");
            StartCoroutine(WaitForFade());
        }
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(fadeClip.length);
        SceneManager.LoadScene("Prologue");
    }
}
