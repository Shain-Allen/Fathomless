using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDataHandler : MonoBehaviour
{
    public GameObject[] activeHarpoons;
    public static CheckpointDataHandler instance;
    public static CheckpointDataHandler Instance
    {
        get { return instance; }
    }
    // List to store all the checkpoints (presets).
    public List<CheckpointPreset> checkpointPresets = new List<CheckpointPreset>();

    public CheckpointPreset currentLatestCheckpoint;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SaveCheckpoint(SubController.Instance.transform.position, SubController.Instance.transform.rotation, GameManager.gminstance.currentTreasure);
    }

    //method to save a new checkpoint.
    public void SaveCheckpoint(Vector3 position, Quaternion rotation, uint treasure)
    {
        CheckpointPreset preset = new CheckpointPreset
        {
            subPosition = position,
            subRotation = rotation,
            treasureCount = treasure,
            // Set other data types as needed.
        };

        checkpointPresets.Add(preset);
        currentLatestCheckpoint = preset;
    }

    public void RemoveHarpoons()
    {
        foreach (GameObject harpoon in activeHarpoons)
        {
            if(harpoon != null)
            {
                Destroy(harpoon);
            }
        }
        activeHarpoons = new GameObject[1];
    }

    //method to load a checkpoint.
    public void LoadCheckpoint()
    {
        StartCoroutine(ArtificialLoadTime());
        SubController.Instance.transform.position = currentLatestCheckpoint.subPosition;
        SubController.Instance.transform.rotation = currentLatestCheckpoint.subRotation;
        GameManager.Instance.currentTreasure = currentLatestCheckpoint.treasureCount;
        GameManager.Instance.playerHealth = 100;
        GameManager.Instance.isGameEnding = false;
        GameManager.Instance.playerOxygen = 100;
        HatchInteractableToInsub.instance.SendToInsub();
    }
    public IEnumerator ArtificialLoadTime()
    {
        GlobalSoundsManager.instance.CutAmbientSounds();
        GlobalSoundsManager.instance.StopWaterAmbience();
        SubDamageManager.instance.RepairAllHits();
        GameManager.Instance.SubHealth = SubDamageManager.Instance.damagePoint.Length;
        RemoveHarpoons();
        yield return new WaitForSeconds(3);
        GlobalSoundsManager.instance.PlaySubAmbience();
        CanvasController.Instance.ResetFadeIn();

    }
    public void AddToHarpoonArray(GameObject harpoon)
    {
        //kind of crazy there isn't an easy way to append gameobjects to arrays, huh?
            GameObject[] newArray = new GameObject[activeHarpoons.Length + 1];

            for (int i = 0; i < activeHarpoons.Length; i++)
            {
                newArray[i] = activeHarpoons[i];
            }

            newArray[newArray.Length - 1] = harpoon;

            activeHarpoons = newArray;
    }
}
