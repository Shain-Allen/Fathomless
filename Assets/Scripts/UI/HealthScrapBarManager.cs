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
        // if(GameManager.Instance.SubHealth <= 0)
        // {
        //     newScale = new Vector3(0, 1, 1);
        //     gameObject.transform.localScale = newScale;
        // }
        // else
        // {
        //     float scale = (-1f / (float)(SubDamageManager.instance.damagePoint.Length)) * (float)GameManager.Instance.SubHealth;
        //     newScale = new Vector3(scale, 1, 1);
        //     gameObject.transform.localScale = newScale;
        // }

        healthBar.value = Convert.ToSingle(GameManager.Instance.SubHealth / SubDamageManager.Instance.damagePoint.Length);

        scrapText.text = GameManager.Instance.Scrap.ToString(); ;
    }

    public void blank()
    {
        
    }
}
