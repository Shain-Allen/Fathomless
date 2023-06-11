using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    private void Start()
    {
        string[] credits = new string[]
        {
            "Thank you for playing our Fathomless demo."
        };

        CanvasController.Instance.DisplayMoreText(credits, 2.5f);
    }
}
