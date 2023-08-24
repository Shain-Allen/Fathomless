using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ClickPlayScript : MonoBehaviour
{
    public AnimationClip fadeClip;
    Animator animator;
    private Fathomless fathomlessInput;

    private void Awake()
    {
        fathomlessInput = new Fathomless();
        fathomlessInput.Player_AMap.Enable();
    }

    private void OnEnable()
    {
        fathomlessInput.Player_AMap.StartGame.performed += OnStartGame;
        fathomlessInput.Player_AMap.ExitGame.performed += OnExitGame;
    }

    private void OnDisable()
    {
        fathomlessInput.Player_AMap.StartGame.performed -= OnStartGame;
        fathomlessInput.Player_AMap.ExitGame.performed -= OnExitGame;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnStartGame(InputAction.CallbackContext context)
    {
        animator.SetTrigger("FadeToBlack");
        StartCoroutine(WaitForFade());
    }
    
    private void OnExitGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(fadeClip.length);
        SceneManager.LoadScene(1);
    }
}
