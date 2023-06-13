using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsRoll : MonoBehaviour
{
    string[] credits;
    private void Start()
    {
        credits = new string[]
        {
            "Thank you for playing our Fathomless demo."
        };

        CanvasController.Instance.DisplayMoreText(credits, 2.5f);
        StartCoroutine(CreditsTimer());
    }
    public IEnumerator CreditsTimer()
    {
        yield return new WaitForSeconds(credits.Length * 2.5f + 3);
        SceneManager.LoadScene("MainMenu");
    }
}
