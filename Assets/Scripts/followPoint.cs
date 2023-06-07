using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPoint : MonoBehaviour
{
    public GameObject Eel1;
    public LargeEnemyBehavior LargeEnemyBehavior;
    void Start()
    {
        
    }


    public void ActivateEel()
    {
        Eel1.SetActive(true);
    }
    public void SpookEel()
    {
        Eel1.GetComponent<LargeEnemyBehavior>().currentState = LargeEnemyBehavior.State.Flee;
        StartCoroutine("DestroyEel");
    }

    IEnumerator DestroyEel()
    {
        yield return new WaitForSeconds(3f);
        Object.Destroy(Eel1);
    }
}
