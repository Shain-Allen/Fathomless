using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueDialogue : MonoBehaviour
{
    string[] script;
    public AudioClip intro;
    void Start()
    {
        script = new string[]{
            "So... You're really doing this?",
            "Yeah. Why wouldn't I be?",
            "It's just... you know. It's dangerous.",
            "This isn't about those \"local legends\" you heard about, is it?",
            "They aren't just legends! The data doesn't lie! Real people have gone missing in that trench!",
            "That may be true. But I have to do my job. I've been doing this for years.",
            "I know... just be careful. You looked over the sub this time, right?",
            "Yeah, yeah. a sub's a sub. can't be that complicated.",
            "Right. Well, if you hit your head on the way down, theres a manual on the table.",
            "Just my luck. Let's get this train sinking."
        };
        CanvasController.Instance.DisplayMoreText(script, 3);
    }
    private IEnumerator IntroTimer()
    {
        yield return new WaitForSeconds(intro.length);
        SceneManager.LoadScene("MainScene");
    }
}
