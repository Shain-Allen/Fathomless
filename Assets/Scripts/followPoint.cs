using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPoint : MonoBehaviour
{
    public GameObject Eel1;
    public LargeEnemyBehavior LargeEnemyBehavior;
    public GameObject Eel2;
    void Start()
    {
        
    }


    public void ActivateEel()
    {
        Eel1.SetActive(true);
    }
    public void ActivateEel2()
    {
        Eel2.SetActive(true);
    }
    public void SpookEel()
    {
        Eel1.GetComponent<LargeEnemyBehavior>().eelFleeing = true;
        StartCoroutine("DestroyEel");
    }
    public void SpookEel2()
    {
        Eel2.GetComponent<LargeEnemyBehavior>().eelFleeing = true;
        StartCoroutine("DestroyEel2");
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
        CanvasController.Instance.DisplayText("(Hull damage revieved. Initiating autopilot)");
    }
    public void EelAlert()
    {
        CanvasController.Instance.DisplayText("(Abnormal heat signature detected. Defensive action advised.)");
    }
    public void PostCombatDialogue()
    {
        CanvasController.Instance.DisplayMoreText(new string[]
        {
            "(Conditions normalized. Returning pilot control.)",
            "That doesn't look good... I better find something to repair those holes.",
            "Maybe I could find something out on the seafloor?"
        }, 3);
    }

    public void ReturnPilotControl()
    {
        SubController.instance.follow = false;
        SubController.instance.ResetRotation(); 
    }

    IEnumerator DestroyEel()
    {
        yield return new WaitForSeconds(6f);
        Object.Destroy(Eel1);
    }
    IEnumerator DestroyEel2()
    {
        yield return new WaitForSeconds(6f);
        Object.Destroy(Eel2);
    }
}
