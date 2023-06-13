using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public GameObject subLight1;
    public GameObject subLight2;

    public GameObject redLightHodler1;    
    public GameObject redLightHodler2;

    public Light redLight1;
    public Light redLight2;

    public float maxLightIntensity;
    float redLightIntensity;
    public float speed;
    int lightSwitch;

    public GameObject warningSound;

    public bool warn; 
    public static Warning instance;

    public static Warning Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        redLightIntensity = 0;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        redLight1.intensity = redLightIntensity;
        redLight2.intensity = redLightIntensity;

        if (warn)
        {
            if (redLightIntensity >= maxLightIntensity)
            {
                lightSwitch = 2;
            }

            if(redLightIntensity <= 0)
            {
                lightSwitch = 1;
            }

            switch (lightSwitch)
            {
                case 1:
                    redLightIntensity += speed;
                    break;
                case 2:
                    redLightIntensity -= speed;
                    break;
            }
        }
    }

    public void warnEffectOn()
    {
        warn = true;
        subLight1.SetActive(false);
        subLight2.SetActive(false);

        redLightHodler1.SetActive(true);
        redLightHodler2.SetActive(true);

        GlobalSoundsManager.instance.StartAlarm();
    }

    public void warnEffectOff()
    {
        warn = false;
        subLight1.SetActive(true);
        subLight2.SetActive(true);

        redLightHodler1.SetActive(false);
        redLightHodler2.SetActive(false);

        GlobalSoundsManager.instance.StopAlarm();
    }
}
