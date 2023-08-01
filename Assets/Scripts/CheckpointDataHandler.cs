using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDataHandler : MonoBehaviour
{
    public GameObject[] activeHarpoons;
    Transform[] urchinPositions;
    EnemyDataManager[] urchinScripts;
    public GameObject[] urchinEnemies;
    public static CheckpointDataHandler instance;
    GameObject Submarine;
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
        SaveEnemies();
        Submarine = SubController.instance.gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.Instance.EndGame();
        }
    }

    public void SaveEnemies()
    {
        urchinEnemies = GameObject.FindGameObjectsWithTag("urchinEnemy");
        urchinPositions = new Transform[urchinEnemies.Length];
        urchinScripts = new EnemyDataManager[urchinEnemies.Length];
        for (int i = 0; i < urchinEnemies.Length; i++)
        {
            urchinScripts[i] = urchinEnemies[i].GetComponentInChildren<EnemyDataManager>(true);
            urchinPositions[i] = urchinEnemies[i].transform;
        }
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

        //checkpointPresets.Add(preset);
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

    //method to load a checkpoint.
    public void LoadCheckpoint()
    {
        StartCoroutine(ArtificialLoadTime());
        HatchInteractableToInsub.instance.SendToInsub();
    }
    public IEnumerator ArtificialLoadTime()
    {
        GlobalSoundsManager.instance.CutAmbientSounds();
        GlobalSoundsManager.instance.StopWaterAmbience();
        GameManager.Instance.SubHealth = SubDamageManager.Instance.damagePoint.Length;
        LoadGM();
        LoadSub();
        RemoveHarpoons();
        LoadEnemies();
        yield return new WaitForSeconds(1);
        GlobalSoundsManager.instance.PlaySubAmbience();
        LoadCanvas();

    }
    void LoadSub()
    {
        SubController.instance.SetSubPosAndRot(currentLatestCheckpoint.subPosition, currentLatestCheckpoint.subRotation);
        SubDamageManager.instance.RepairAllHits();
    }
    void LoadCanvas()
    {
        CanvasController.Instance.ResetFadeIn();
    }
    void LoadGM()
    {
        GameManager.Instance.currentTreasure = currentLatestCheckpoint.treasureCount;
        GameManager.Instance.playerHealth = 100;
        GameManager.Instance.isGameEnding = false;
        GameManager.Instance.playerOxygen = 100;
    }
    void LoadEnemies()
    {
        for (int i = 0; i < urchinEnemies.Length; i++)
        {
            urchinScripts[i].enemyHealth = urchinScripts[i].enemyMaxHealth;
            urchinEnemies[i].SetActive(true);
            urchinEnemies[i].transform.position = urchinPositions[i].position;
            urchinPositions[i].rotation = urchinPositions[i].rotation;
        }

    }
}
