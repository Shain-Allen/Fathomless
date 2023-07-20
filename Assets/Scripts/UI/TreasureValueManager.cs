using System;
using TMPro;
using UnityEngine;

public class TreasureValueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI treasureText;

    private void Update()
    {
        if (GameManager.gminstance.newTreasure > 0 && GameManager.gminstance.currentTreasure <= GameManager.gminstance.treasureMax)
        {
            AddTreasure();
        }
    }

    private void AddTreasure()
    {
        GameManager.gminstance.currentTreasure++;
        GameManager.gminstance.newTreasure--;

        treasureText.text = GameManager.gminstance.currentTreasure.ToString().PadLeft(7, '0');
    }
}
