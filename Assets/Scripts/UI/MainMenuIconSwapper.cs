using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuIconSwapper : MonoBehaviour
{
    private PlayerInput playerInput;
    private Fathomless fathomlessInput;

    public Image buttonHintStart;
    public Image buttonHintExit;

    public List<Sprite> startButtons;
    public List<Sprite> ExitButtons;

    private void Awake()
    {
        fathomlessInput = new Fathomless();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == fathomlessInput.XboxControllerScheme.name)
        {
            buttonHintStart.sprite = startButtons[1];
            buttonHintExit.sprite = ExitButtons[1];
        }
        else if (playerInput.currentControlScheme == fathomlessInput.PlaystationScheme.name)
        {
            buttonHintStart.sprite = startButtons[2];
            buttonHintExit.sprite = ExitButtons[2];
        }
        else
        {
            buttonHintStart.sprite = startButtons[0];
            buttonHintExit.sprite = ExitButtons[0];
        }
    }
}
