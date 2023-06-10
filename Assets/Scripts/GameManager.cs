using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gminstance;
    public GameObject player;
    public int Scrap;
    public int ScrapMax;

    public static GameManager Instance
    {
        get { return gminstance; }
    }
    private void Awake()
    {
        if (gminstance != null && gminstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gminstance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    CanvasController.Instance.DisplayText("Rock.");
                    break;
                case 2:
                    CanvasController.Instance.DisplayText("Paper.");
                    break;
                case 3:
                    CanvasController.Instance.DisplayText("Scissiors.");
                    break;
            }
        }
    }
    public void EndGame()
    {
        CanvasController.Instance.DisplayText("Game Over");
    }
}
