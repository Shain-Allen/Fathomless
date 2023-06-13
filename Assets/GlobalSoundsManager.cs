using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundsManager : MonoBehaviour
{
    public playerScript2 playerScript;
    public SubController subController;
    AudioSource[] audiosources;
    playerstate state;
    bool alarmRunning;
    public AudioSource toBeFaded;
    public int indexToFade;

    public static GlobalSoundsManager instance;
    public static GlobalSoundsManager Instance
    {
        get { return instance; }
    }
    enum playerstate
    {
        //stipulating whether or not sounds should play depending on if the player is in the sub or not.
        inSub, Outsub
    }

    private void Awake()
    {
        audiosources = GetComponents<AudioSource>();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        //This will handle random ambient sounds, as well as constant ambience.
        if (state == playerstate.inSub)
        {
            //sounds that will play when the player is in the sub

        }
        if (state == playerstate.Outsub)
        {
            //sounds that will play when the player is outside the sub

        }
        if (state == playerstate.Outsub || state == playerstate.inSub)
        {
            //sounds that always have the ability to play

        }
    }
    public void PlayHammer()
    {
        audiosources[0].pitch = Random.Range(0.85f, 1.25f);
        audiosources[0].Play();
    }

    public void StartAlarm()
    {
        if (!alarmRunning)
        {
            alarmRunning = true;
            audiosources[2].loop = true;
            audiosources[2].Play();
        }
    }

    public void StopAlarm()
    {
        if (alarmRunning)
        {
            alarmRunning = false;
            audiosources[2].loop = false;
        }
    }
    public void PlayMusic()
    {
        indexToFade = 9;
        FadeIn();
    }
    public void StopMusic()
    {
        indexToFade = 9;
        FadeOut();
    }

    public void PlayHarpoon()
    {
        audiosources[3].pitch = Random.Range(0.85f, 1.25f);
        audiosources[3].Play();
    }
    public void PlayReload()
    {
        audiosources[4].Play();
    }

    public void PlayMetalSnap()
    {
        audiosources[1].pitch = Random.Range(0.85f, 1.25f);
        audiosources[1].Play();
    }
    private void Update()
    {
        HandleFade();
        //making a lot of buttons to try sounds for volume testing.
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    PlayHammer();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    PlayHarpoon();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    PlayMetalSnap();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    PlayReload();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    PlayMusic();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    SubDamageManager.instance.Hit();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //}
    }
    public float fadeDuration = 1f;
    public float targetVolume;

    private float initialVolume;
    private float currentFadeTime;
    private bool isFading;
    void HandleFade()
    {
        if (isFading)
        {
            currentFadeTime += Time.deltaTime;

            float t = Mathf.Clamp01(currentFadeTime / fadeDuration);
            audiosources[indexToFade].volume = Mathf.Lerp(initialVolume, targetVolume, t);

            if (t >= 1f)
            {
                isFading = false;
            }
        }
    }
    public void FadeIn()
    {
        initialVolume = audiosources[indexToFade].volume;
        targetVolume = 0.3f;
        currentFadeTime = 0f;
        isFading = true;
    }

    public void FadeOut()
    {
        initialVolume = audiosources[indexToFade].volume;
        targetVolume = 0f;
        currentFadeTime = 0f;
        isFading = true;
    }
}
