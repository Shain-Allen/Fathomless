using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScrapBarManager : MonoBehaviour
{
    public Text text;
    Vector3 newScale;
    private void FixedUpdate()
    {
        if(GameManager.Instance.SubHealth <= 0)
        {
            newScale = new Vector3(0, 1, 1);
            gameObject.transform.localScale = newScale;
        }
        else
        {
            float scale = (-1f / (float)(SubDamageManager.instance.damagePoint.Length)) * (float)GameManager.Instance.SubHealth;
            newScale = new Vector3(scale, 1, 1);
            gameObject.transform.localScale = newScale;
        }

        text.text = GameManager.Instance.Scrap.ToString(); ;
    }
}
