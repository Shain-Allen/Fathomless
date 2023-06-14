using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundsManager : MonoBehaviour
{
    public playerScript2 playerScript;
    public SubController subController;
    public AudioSource[] audiosources;
    bool alarmRunning;
    public AudioSource toBeFaded;
    public int indexToFade;
    public float timer;
    public float averageInterval;


    public static GlobalSoundsManager instance;
    public static GlobalSoundsManager Instance
    {
        get { return instance; }
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
        if (playerScript2.instance.inSub)
        {
            //sounds that will play when the player is in the sub
        }
        if (!playerScript2.instance.inSub)
        {
            //sounds that will play when the player is outside the sub

        }
    }
    private void Start()
    {
        timer = Random.Range(averageInterval - 30f, averageInterval + 30f);
    }
    public void PlaySplash()
    {
        audiosources[11].pitch = Random.Range(0.85f, 1.25f);
        audiosources[11].Play();
    }
    public void PlayAmbience()
    {
        audiosources[5].Play();
        audiosources[10].Stop();
    }
    public void StopAmbience()
    {
        audiosources[5].Stop();
        audiosources[10].Play();
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
        RandomTimer();
    }

    private void RandomTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PlayRandomSound();
            timer = Random.Range(averageInterval - 30f, averageInterval + 30f);
        }
    }
    public void CutAmbientSounds()
    {
        audiosources[6].Stop();
        audiosources[7].Stop();
        audiosources[8].Stop();
        audiosources[12].Stop();
    }
    private void PlayRandomSound()
    {
        int randomIndex = Random.Range(1, 5);
        switch (randomIndex)
        {
            case 1:
                randomIndex = 6;
                break;
            case 2:
                randomIndex = 7;
                break;
            case 3:
                randomIndex = 8;
                break;
            case 4:
                randomIndex = 12;
                break;
        }
        AudioSource randomClip = audiosources[randomIndex];
        if ((randomIndex == 7 || randomIndex == 8) && playerScript2.instance.inSub)
        {
            randomClip.Play();
        }
        else if (randomIndex == 6 || randomIndex == 12 && !playerScript2.instance.inSub)
        {
            randomClip.Play();
        }
        else if ((randomIndex == 7 || randomIndex == 8) && !playerScript2.instance.inSub)
        {
            PlayRandomSound();
        }
        else if (randomIndex == 6 || randomIndex == 12 && playerScript2.instance.inSub)
        {
            PlayRandomSound();
        }
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
