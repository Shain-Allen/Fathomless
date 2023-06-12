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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartAlarm();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StopAlarm();
        }
    }
}
