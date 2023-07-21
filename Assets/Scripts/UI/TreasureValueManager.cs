using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TreasureValueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI treasureText;
    [SerializeField] private uint transferSpeed = 1;

    private void Update()
    {
        if (GameManager.gminstance.newTreasure >= transferSpeed && GameManager.gminstance.currentTreasure <= GameManager.gminstance.treasureMax)
        {
            AddTreasure(transferSpeed);
        }
        else if (GameManager.gminstance.newTreasure > 0 && GameManager.gminstance.currentTreasure <= GameManager.gminstance.treasureMax)
        {
            AddTreasure(1);
        }
    }

    private void AddTreasure(uint transferValue)
    {
        GameManager.gminstance.currentTreasure += transferValue;
        GameManager.gminstance.newTreasure -= transferValue;

        treasureText.text = GameManager.gminstance.currentTreasure.ToString().PadLeft(7, '0');
    }
}
