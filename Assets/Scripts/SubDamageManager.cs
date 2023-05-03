using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDamageManager : MonoBehaviour, IHit
{
    public GameManager Manager;
    public GameObject damageProtrusionObj;
    public GameObject damageProtrusionInstance;
    public GameObject[] damagePoint = new GameObject[5];
    public bool[] DamagedSpots = new bool[5];

    int currentlyDamaged;
    public void Hit()
    {
        while (true)
        {
            for (int i = 0; i < DamagedSpots.Length; i++)
            {
                if (DamagedSpots[i] == true)
                {
                    currentlyDamaged++;
                }
            }
            if (currentlyDamaged >= DamagedSpots.Length)
            {
                print("Sub destroyed, game over.");
                break;
            }
            else
            {
                currentlyDamaged = 0;
            }
            int damaged = Random.Range(0, damagePoint.Length);
            if (DamagedSpots[damaged] == false)
            {
                damageProtrusionInstance = Instantiate(damageProtrusionObj, damagePoint[damaged].transform.position, damagePoint[damaged].transform.rotation);
                DamageRepairInteractable inst = damageProtrusionInstance.GetComponent<DamageRepairInteractable>();
                inst.DamageProtrusionIndex = damaged;
                inst.Manager = Manager;
                inst.transform.SetParent(damagePoint[damaged].transform);
                inst.SubDamageManager = this;
                DamagedSpots[damaged] = true;
                break;
            }
        }
    }


    void Start()
    {
        for (int i = 0; i < damagePoint.Length; i++)
        {
            DamagedSpots[i] = false;
        }
    }


    void Update()
    {
        print(" ");
        for (int i = 0; i < damagePoint.Length; i++)
        {
            print(DamagedSpots[i]);
        }
        print(currentlyDamaged);
    }
}
