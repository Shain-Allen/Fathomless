using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
public class TreasureInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private uint nominalValue;
    [SerializeField] private uint valueRange;

    public void Interact(GameObject player)
    {
        float lowBound = Convert.ToSingle(nominalValue - valueRange);
        float upperBound = (Convert.ToSingle(nominalValue + valueRange));
        
        GameManager.gminstance.newTreasure += Convert.ToUInt32(Random.Range(lowBound, upperBound));
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (TryGetComponent(out Collider tiCollider) && !tiCollider.isTrigger)
        {
            tiCollider.isTrigger = true;
            Debug.LogWarning($"The Collider for {gameObject.name} has been changed to a trigger to allow the TreasureInteractable script to work");
        }
    }
}
