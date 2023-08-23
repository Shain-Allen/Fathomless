using TMPro;
using UnityEngine;

public class PlayerUIScrapCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerUIScrapValue;

    private void Start()
    {
        playerUIScrapValue = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        playerUIScrapValue.text = GameManager.Instance.Scrap.ToString();
    }
}
