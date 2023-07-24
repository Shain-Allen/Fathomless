using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScrapBarManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scrapText;
    [SerializeField] private Slider healthBar;
    
    private void FixedUpdate()
    {
        healthBar.value = Convert.ToSingle(GameManager.Instance.SubHealth / SubDamageManager.Instance.damagePoint.Length);

        scrapText.text = GameManager.Instance.Scrap.ToString(); ;
    }

    public void blank()
    {
    }
}
