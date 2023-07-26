using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPoint : MonoBehaviour
{
    public GameObject Eel;
    public LargeEnemyBehavior LargeEnemyBehavior;
    void Start()
    {
        
    }


    public void ActivateEel()
    {
        Eel.SetActive(true);
    }
    public void SpookEel()
    {
        Eel.GetComponent<LargeEnemyBehavior>().eelFleeing = true;
        StartCoroutine("DeactivateEel");
    }
    public void EndOfRoad()
    {
        GameManager.gminstance.EndGame();
    }

    public void StartMusic()
    {
        GlobalSoundsManager.instance.PlayMusic();
    }
    public void StopMusic()
    {
        GlobalSoundsManager.instance.StopMusic();
    }
    public void ActivateAlarm()
    {
        Warning.instance.warnEffectOn();
    }
    public void DeactivateAlarm()
    {
        Warning.instance.warnEffectOff();
    }
    public void DamageSub()
    {
        SubDamageManager.instance.Hit();
    }
    public void AutopilotAlert()
    {
        //text removed
    }
    public void EelAlert()
    {
        //text removed
    }
    public void PostCombatDialogue()
    {
        //text removed
    }

    public void ReturnPilotControl()
    {
        SubController.instance.follow = false;
        SubController.instance.ResetRotation(); 
    }

    IEnumerator DeactivateEel()
    {
        yield return new WaitForSeconds(6f);
        Eel.gameObject.SetActive(false);
    }
}
