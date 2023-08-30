using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public uint DeathPenalty;

    public float pressTimeThreshold = 0.5f;    // Time threshold for quick succession
    public int requiredPressCount = 5;         // Number of presses required

    private int pressCount = 0;
    private float lastPressTime = 0f;
    private Fathomless fathomlessInputActions;

    //for hatch fade transitions
    public bool isFading;

    public bool isGameEnding;
    public bool toCredits;
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

        fathomlessInputActions = new Fathomless();
        fathomlessInputActions.Player_AMap.Enable();
    }
    private void OnEnable()
    {
        fathomlessInputActions.Player_AMap.StartGame.performed += OnStartGame;
        fathomlessInputActions.Player_AMap.StartGame.canceled += OnStartGame;
    }
    private void Start()
    {
        isGameEnding = false;
        toCredits = false;
        playerHealth = 100;
        playerReloadPercentage = 100;
        playerOxygen = 100;


        CanvasController.Instance.DisplayMoreText(introText, 2.5f, true);
    }
    private void Update()
    {
        

    }
    private void OnStartGame(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        float currentTime = Time.time;

        if (currentTime - lastPressTime > pressTimeThreshold)
        {
            pressCount = 1;
        }
        else
        {
            pressCount++;
            if (pressCount >= requiredPressCount)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        lastPressTime = currentTime;
    }
    private void FixedUpdate()
    {
        if (SubHealth <= 0 || playerHealth <= 0)
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
            if(playerOxygen <= 0)
            {
                playerHealth -= O2DepletionRate * Time.deltaTime;
            }
        }
        else
        {
            playerOxygen += O2DepletionRate * Time.deltaTime * 10;
            playerHealth += O2DepletionRate * Time.deltaTime * 4.5f;
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
                StartCoroutine(CheckPointTimer());
            else
            {
                Debug.LogError("Gamemanager is missing FadeToBlack animation clip. Assign it in the inspector.");
            }
        }
    }
    //a timer so the animation can play before changing scenes
    public IEnumerator CheckPointTimer()
    {
        CanvasController.Instance.PlayFadeToBlack();
        yield return new WaitForSeconds(FadeToBlack.length + 1);
        if (!toCredits)
        {
            CheckpointDataHandler.Instance.LoadCheckpoint();
        }
        else
        {
            SceneManager.LoadScene("Credits");
        }
    }

    public string[] introText = { "I made it into the trench! But the sub took a hit on the way down.", "I saw some scrap on the seafloor.", "Maybe I can use them to patch the hole?" };
}
