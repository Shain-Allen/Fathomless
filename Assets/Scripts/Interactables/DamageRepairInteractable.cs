using UnityEngine;

public class DamageRepairInteractable : MonoBehaviour, IInteractable
{
    public GameManager Manager;
    public bool repaired;
    public float shrinkSpeed;
    public SubDamageManager SubDamageManager;
    public int DamageProtrusionIndex = 0;
    public GameObject repairPlate;
    AudioSource waterSound;
    float initialVolume;
    bool makePlate;
    public void Interact(GameObject player)
    {
        if (GameManager.Instance.Scrap >= 1)
        {
            GlobalSoundsManager.instance.PlayHammer();
            repaired = true;
            makePlate = true;
            GameManager.Instance.Scrap--;
        }
        else
        {
            if (Random.value < 0.5)
            {
                CanvasController.Instance.DisplayText("I hope I can find something to patch this up...");
            }
            else
            {
                CanvasController.Instance.DisplayText("I've got nothing.");
            }
        }
    }
    private void FixedUpdate()
    {
        if (PlayerScript.instance.inSub)
        {
            waterSound.volume = initialVolume;
        }
        else
        {
            waterSound.volume = 0;
        }
    }
    private void Start()
    {
        waterSound = GetComponent<AudioSource>();
        initialVolume = waterSound.volume;
    }

    public void Update()
    {
        if (repaired)
        {
            transform.localScale = transform.localScale / shrinkSpeed;
            if (transform.localScale.x < 0.05f)
            {
                if (makePlate)
                    Instantiate(repairPlate, transform.position, transform.rotation, transform.parent);
                SubDamageManager.DamagedSpots[DamageProtrusionIndex] = false;
                SubDamageManager.instance.UpdateSubHealth();
                Destroy(this.gameObject);
            }
        }
    }
}
