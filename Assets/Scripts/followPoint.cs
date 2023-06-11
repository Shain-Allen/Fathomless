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
        Eel1.GetComponent<LargeEnemyBehavior>().currentState = LargeEnemyBehavior.State.Flee;
        StartCoroutine("DestroyEel");
    }
    public void SpookEel2()
    {
        Eel2.GetComponent<LargeEnemyBehavior>().currentState = LargeEnemyBehavior.State.Flee;
        StartCoroutine("DestroyEel2");
    }
    public void EndOfRoad()
    {
        GameManager.gminstance.EndGame();
    }

    IEnumerator DestroyEel()
    {
        yield return new WaitForSeconds(3f);
        Object.Destroy(Eel1);
    }
    IEnumerator DestroyEel2()
    {
        yield return new WaitForSeconds(3f);
        Object.Destroy(Eel2);
    }
}
