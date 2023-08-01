using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Animation ScreenShakeAnim;

    public static CameraManager instance;
    public static CameraManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ScreenShakeAnim = GetComponent<Animation>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            HurtPlayerEffect();
        }
    }

    public void HurtPlayerEffect()
    {
        ScreenShakeAnim.Play();
        CanvasController.Instance.PlayDamageVignette();
    }
    public void MoveCam(float yrot)
    {
        transform.localRotation = Quaternion.Euler(yrot, 0, 0);
    }
}
