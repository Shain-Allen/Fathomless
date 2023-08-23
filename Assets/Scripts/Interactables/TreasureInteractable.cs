using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
public class TreasureInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private uint nominalValue;
    [SerializeField] private uint valueRange;
    [Range(1f, 15f)]
    public float floatSpeed;
    public float shrinkSpeed;
    bool pickedUp;

    public void Interact(GameObject player)
    {
        if (GameManager.gminstance.newTreasure < GameManager.gminstance.treasureMax)
        {
            pickedUp = true;
        }
        else
        {
            CanvasController.Instance.DisplayText("I can't carry any more.");
        }
        float lowBound = Convert.ToSingle(nominalValue - valueRange);
        float upperBound = (Convert.ToSingle(nominalValue + valueRange));

        GameManager.gminstance.newTreasure += Convert.ToUInt32(Random.Range(lowBound, upperBound));
    }
    /* Incase we decide make the treasure pick ups use this code instead of interact.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.gminstance.newTreasure < GameManager.gminstance.treasureMax)
            {
                pickedUp = true;

                float lowBound = Convert.ToSingle(nominalValue - valueRange);
                float upperBound = Convert.ToSingle(nominalValue + valueRange);
                GameManager.gminstance.newTreasure += Convert.ToUInt32(Random.Range(lowBound, upperBound));
            }
            else
            {
                CanvasController.Instance.DisplayText("I can't carry any more.");
            }
        }
    }
    */
    private void Start()
    {
        pickedUp = false;
    }
    private void Update()
    {
        if (pickedUp)
        {
            Vector3 direction = PlayerScript.instance.transform.position - transform.position;
            transform.position += direction * floatSpeed * Time.deltaTime;
            transform.Rotate(0, 250 * Time.deltaTime, 0);

            transform.localScale = transform.localScale / shrinkSpeed;

            if (direction.magnitude < 0.3)
            {
                Destroy(gameObject);
            }
        }
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
