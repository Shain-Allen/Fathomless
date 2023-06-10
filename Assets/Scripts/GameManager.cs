using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int Scrap;
    public int ScrapMax;

    private void Start()
    {
        //Scrap = 0;
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
}
