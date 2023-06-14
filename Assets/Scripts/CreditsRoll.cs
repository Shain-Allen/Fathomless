using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsRoll : MonoBehaviour
{
    string[] credits;
    private void Start()
    {
        credits = new string[]
        {
            "Thank you for playing our Fathomless demo.",
            "This couldn't be possible without our fantastic team:",
            "Producers:",
            "Maverick Wilkinson     Sean Spencer     Emily Adams     Ethan Berning.",
            "Modeling and Texture artists:",
            "Carlos Flores-Torres     Ita Cummins    Howard Chan        Micheal MCGOWAN",
            "William Collins          An Ta           Daniel Agcaoili    Anna Wylie",
            "Ethan Berning",
            "Particle effects:",
            "Tyler Bader Ramos",
            "Lighting and Volumetrics:",
            "Tyler Charnholm",
            "Programmers:",
            "Maverick Wilkinson     Joshua Abens     Sean Spencer     Hugo Autio",
            "Animation and Rigging:",
            "Erin Hiliker    Ita Cummins",
            "Environment and Level Design",
            "Emily Adams     Alex Turner",
            "UI",
            "Maverick Wilkinson    Tyler Charnholm",
            "Implementation",
            "Maverick Wilkinson    Joshua Abens    Sean Spencer",
            "Moral support (and special thanks)",
            "Larry Paolicelli",
            "and the folks who made all these sounds.",
            "Metal Breaking: Sound Finder on Youtube",
            "Sonar Ping: Pixabay",
            "Distress Signal 2: Pixabay",
            "Moving Low Frequencies: Pixabay",
            "Baleines: Pixabay",
            "Sea and Seagull, wave: Pixabay",
            "WalthamstowResevoire: Pixabay",
            "muvibeat1_130BPM: yokim Pixabay",
            "Reload: UNIVERSFIELD Pixabay",
            "Deep Sea Ambience: Pixabay",
            "Tank reloading: iwanPlays on Youtube",
            "Under water sounds while scuba diving: Pixabay",
            "SPLASH (by blaukreuz): Pixabay",
            "underwater-crab-like-monster-growl: Pixabay",
            "Additional sound effects from https://www.zapsplat.com",
            "And thank you for playing.",
            "We really hope you enjoyed the experience."
        };

        CanvasController.Instance.DisplayMoreText(credits, 2.5f);
        StartCoroutine(CreditsTimer());
    }
    public IEnumerator CreditsTimer()
    {
        yield return new WaitForSeconds(credits.Length * 2.5f + 3);
        SceneManager.LoadScene("MainMenu");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
