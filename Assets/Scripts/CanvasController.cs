using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Text displayText;
    public AudioClip clip;
    AudioSource audioSource;
    private static CanvasController instance;

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
    }
    public void DisplayText(string text)
    {
        audioSource.PlayOneShot(clip);
        DrawTextToScreen(text);
    }

    private void DrawTextToScreen(string text)
    {
        GameObject entryObject = Instantiate(textEntryPrefab, TextBox.transform);
        entryObject.transform.SetParent(TextBox.transform, true);
        Text textComponent = entryObject.GetComponent<Text>();
        textComponent.text = text;

    }
}