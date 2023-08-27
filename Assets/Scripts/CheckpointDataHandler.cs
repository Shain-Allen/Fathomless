using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDataHandler : MonoBehaviour
{
    public GameObject[] activeHarpoons;
    Transform[] urchinPositions;
    public EnemyDataManager[] urchinScripts;
    public GameObject[] urchinEnemies;
    public GameObject[] Scrap;
    public GameObject[] Mines;
    Transform[] minePositions;
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
        SaveMines();
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
    public void SaveMines()
    {
        Mines = GameObject.FindGameObjectsWithTag("Mine");
        minePositions = new Transform[Mines.Length];
        for (int i = 0; i < Mines.Length; i++)
        {
            minePositions[i] = Mines[i].transform;
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
        CheckStations();
        LoadGM();
        LoadSub();
        RemoveHarpoons();
        LoadEnemies();
        LoadMines();
        LoadTunnelReset();
        yield return new WaitForSeconds(1);
        GlobalSoundsManager.instance.PlaySubAmbience();
        LoadCanvas();

    }
    void CheckStations()
    {
        if (PilotPanelInteractable.Instance.controlSub)
        {
            PilotPanelInteractable.Instance.RemoveControl();
        }
        if (TurretInteractable.Instance.controlTurret)
        {
            TurretInteractable.Instance.RemoveControl();
        }
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
    void LoadTunnelReset()
    {
        if (SubController.instance.follow)
        {
            SubController.instance.follow = false;
            followPoint currentTunnelScript = SubController.instance.followPoint.GetComponent<followPoint>();
            Animation followPointAnim = SubController.instance.followPoint.GetComponent<Animation>();
            followPointAnim.Stop();
            followPointAnim.Rewind();
            currentTunnelScript.SpookEel();
            currentTunnelScript.ReturnPilotControl();
            currentTunnelScript.DeactivateAlarm();
        }
    }
    void LoadEnemies()
    {
        for (int i = 0; i < urchinEnemies.Length; i++)
        {
            if (urchinEnemies[i] != null)
            {
                urchinScripts[i].enemyHealth = urchinScripts[i].enemyMaxHealth;
                urchinEnemies[i].SetActive(true);
                urchinEnemies[i].transform.position = urchinPositions[i].position;
                urchinPositions[i].rotation = urchinPositions[i].rotation;
            }
        }

    }
    void LoadMines()
    {
        for (int i = 0; i < Mines.Length; i++)
        {
            if (Mines[i] != null)
            {
                Mines[i].SetActive(true);
                Mines[i].transform.position = minePositions[i].position;
                minePositions[i].rotation = minePositions[i].rotation;
            }
        }

    }
}
