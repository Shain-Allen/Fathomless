using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDataHandler : MonoBehaviour
{
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
        yield return new WaitForSeconds(3);
        GlobalSoundsManager.instance.PlaySubAmbience();
        CanvasController.Instance.ResetFadeIn();

    }
}
