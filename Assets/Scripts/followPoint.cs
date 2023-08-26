using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPoint : MonoBehaviour
{
    public GameObject Eel;
    public GameObject EelSpawnPoint;
    public LargeEnemyBehavior LargeEnemyBehavior;
    public Animator FallingRocks;
    public float startingHealth;
    void Start()
    {
        startingHealth = LargeEnemyBehavior.enemyHealth;
    }


    public void ActivateEel()
    {
        LargeEnemyBehavior.enemyHealth = startingHealth;
        LargeEnemyBehavior.eelFleeing = false;
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
    public void DropRocks()
    {
        FallingRocks.SetTrigger("RockFall");
    }
    public void ResetRocks()
    {
        FallingRocks.SetBool("Reset", true);
        StartCoroutine(ResetCancel());
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
        PilotPanelInteractable.Instance.canControl = true;
        HatchInteractableToOutsub.instance.animBlock = false;
    }

    IEnumerator DeactivateEel()
    {
        yield return new WaitForSeconds(4f);
        Eel.transform.position = EelSpawnPoint.transform.position;
        Eel.transform.rotation = EelSpawnPoint.transform.rotation;
        LargeEnemyBehavior.currentState = LargeEnemyBehavior.State.Pursue;
        Eel.gameObject.SetActive(false);
    }
    IEnumerator ResetCancel()
    {
        yield return new WaitForSeconds(0.1f);
        FallingRocks.SetBool("Reset", false);
    }
}
