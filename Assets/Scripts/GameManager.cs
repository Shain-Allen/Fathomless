using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gminstance;
    public int Scrap;
    public int ScrapMax;
    public uint currentTreasure;
    public uint newTreasure;
    public uint treasureMax = 9999999;
    public AnimationClip FadeToBlack;
    public int SubHealth;
    public bool canScissor;
    public float playerHealth;
    public float playerOxygen;
    public float O2DepletionRate;
    public float playerReloadPercentage;
    public float isReloading;


    //for hatch fade transitions
    public bool isFading;

    bool isGameEnding;
    public static GameManager Instance
    {
        get { return gminstance; }
    }
    private void Awake()
    {
        if (gminstance != null && gminstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gminstance = this;
        }
    }
    private void Start()
    {
        isGameEnding = false;
        playerHealth = 100;
        playerReloadPercentage = 100;
        playerOxygen = 100;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && canScissor)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    CanvasController.Instance.DisplayText("Rock.");
                    break;
                case 2:
                    CanvasController.Instance.DisplayText("Paper.");
                    break;
                case 3:
                    CanvasController.Instance.DisplayText("Scissiors.");
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tilde))
        {
            canScissor = true;
            CanvasController.Instance.DisplayText("Cheats activated: RPS Delux DLC Added.");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    private void FixedUpdate()
    {
        if (SubHealth <= 0)
        {
            EndGame();
        }
        HandleOxygen();
        CapBars();
    }

    private void HandleOxygen()
    {
        if (!PlayerScript.Instance.inSub)
        {
            playerOxygen -= O2DepletionRate * Time.deltaTime;
        }
        else
        {
            playerOxygen += O2DepletionRate * Time.deltaTime * 10;
        }
    }

    private void CapBars()
    {
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
        if (playerOxygen > 100)
        {
            playerOxygen = 100;
        }
        if (playerOxygen < 0)
        {
            playerOxygen = 0;
        }
    }

    public void EndGame()
    {
        //stops the animation from double procing in rare cases
        if (!isGameEnding)
        {
            isGameEnding = true;
            if (FadeToBlack != null)
                StartCoroutine(EndGameTimer());
            else
            {
                Debug.LogError("Gamemanager is missing FadeToBlack animation clip. Assign it in the inspector.");
            }
        }
    }
    //a timer so the animation can play before changing scenes
    public IEnumerator EndGameTimer()
    {
        CanvasController.Instance.PlayFadeToBlack();
        yield return new WaitForSeconds(FadeToBlack.length + 1);
        SceneManager.LoadScene("Credits");
    }
}
