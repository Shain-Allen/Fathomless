using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitScrap : MonoBehaviour
{
    public GameObject scrap;
    public Rigidbody rigi;
    bool isHit;
    public Collider mineCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "harpoon")
        {
            rigi.isKinematic = false;
            mineCollider.isTrigger = false;
            Debug.Log("Hit Mine Trigger");
        }
    }

    public IEnumerator mineTimer()
    {
        yield return new WaitForSeconds(3f);
        rigi.isKinematic = true;
        mineCollider.isTrigger = true;
    }
}
