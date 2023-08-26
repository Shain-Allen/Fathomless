using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CreditsIconSwaper : MonoBehaviour
{
    private PlayerInput playerInput;
    private Fathomless fathomlessInput;

    public Image buttonHintExit;
    
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
            buttonHintExit.sprite = ExitButtons[1];
        }
        else if (playerInput.currentControlScheme == fathomlessInput.PlaystationScheme.name)
        {
            buttonHintExit.sprite = ExitButtons[2];
        }
        else
        {
            buttonHintExit.sprite = ExitButtons[0];
        }
    }
}
