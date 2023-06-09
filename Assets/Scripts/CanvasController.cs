using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CanvasController : MonoBehaviour
{
    public Text displayText;
    public AudioClip clip;
    AudioSource audioSource;
    private static CanvasController instance;
    private IEnumerator coroutine;
    private Animator animator;

    public GameObject textEntryPrefab;
    public GameObject TextBox;

    public static CanvasController Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        animator = GetComponent<Animator>();
        PlayFadeFromBlack();
    }
    public void DisplayText(string text)
    {
        audioSource.PlayOneShot(clip);
        DrawTextToScreen(text);
    }
    public void DisplayMoreText(string[] lines, float delay)
    {
        coroutine = TextWriter(lines, delay);
        StartCoroutine(coroutine);
    }

    public void PlayFadeToBlack()
    {
        animator.SetTrigger("FadeToBlack");
    }

    public void PlayFadeFromBlack()
    {
        animator.SetTrigger("FadeFromBlack");
    }

    private void DrawTextToScreen(string text)
    {
        GameObject entryObject = Instantiate(textEntryPrefab, TextBox.transform);
        GameObject textContainer = entryObject.transform.GetChild(0).gameObject;
        entryObject.transform.SetParent(TextBox.transform, true);
        Vector2 ShiftedDisplacePos = new Vector2(0, textContainer.GetComponent<RectTransform>().anchoredPosition.y);
        Vector2 ShiftedTextPos = new Vector2(0, textContainer.GetComponent<RectTransform>().anchoredPosition.y + 50 * TextBox.transform.childCount);
        entryObject.GetComponent<RectTransform>().localPosition = ShiftedTextPos;
        textContainer.GetComponent<RectTransform>().localPosition = ShiftedTextPos;
        Text textComponent = textContainer.GetComponent<Text>();
        textComponent.text = text;
    }
    
    private IEnumerator TextWriter(string[] lines, float delay)
    {
        foreach (string line in lines)
        {
            DisplayText(line);
            yield return new WaitForSeconds(delay);
        }
    }
}