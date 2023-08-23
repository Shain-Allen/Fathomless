using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class TreasureValueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI treasureText;
    [SerializeField] private uint transferSpeed = 1;
    private bool isUpdatingTreasure = false;

    private void Update()
    {
        if (GameManager.gminstance.newTreasure >= transferSpeed && GameManager.gminstance.currentTreasure <= GameManager.gminstance.treasureMax)
        {
            if (!isUpdatingTreasure)
            {
                StartUpdatingTreasure();
            }
        }
        else if (GameManager.gminstance.newTreasure > 0 && GameManager.gminstance.currentTreasure <= GameManager.gminstance.treasureMax)
        {
            if (!isUpdatingTreasure)
            {
                StartUpdatingTreasure();
            }
        }
        else
        {
            if (isUpdatingTreasure)
            {
                StopUpdatingTreasure();
            }
        }
    }

    private void StartUpdatingTreasure()
    {
        isUpdatingTreasure = true;
        StartCoroutine(UpdateTreasureCoroutine());
        GlobalSoundsManager.instance.PlayTreasureCollect();
    }

    private void StopUpdatingTreasure()
    {
        isUpdatingTreasure = false;
        GlobalSoundsManager.instance.StopTreasureCollect();
    }

    private IEnumerator UpdateTreasureCoroutine()
    {
        while (GameManager.gminstance.newTreasure >= transferSpeed && GameManager.gminstance.currentTreasure <= GameManager.gminstance.treasureMax)
        {
            AddTreasure(transferSpeed);
            yield return null;
        }

        while (GameManager.gminstance.newTreasure > 0 && GameManager.gminstance.currentTreasure <= GameManager.gminstance.treasureMax)
        {
            AddTreasure(1);
            yield return null;
        }
    }

    private void AddTreasure(uint transferValue)
    {
        GameManager.gminstance.currentTreasure += transferValue;
        GameManager.gminstance.newTreasure -= transferValue;
        treasureText.text = GameManager.gminstance.currentTreasure.ToString().PadLeft(7, '0');
    }
}
