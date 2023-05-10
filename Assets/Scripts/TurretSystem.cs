using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSystem : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Camera camera;
    float x_rotation = 0f;
    float y_rotation = 0f;
    public GameObject turretProjectilePrefab;
    public Transform turretBarrel;
    public float fireDelay = 1.0f;
    float nextShot = 0.01f;
    public bool isTurret;

    void FixedUpdate()//this will use simple keycodes for now, but we can use this for the unity input system if we want. This is just to see the best way to control the sub
    {
        if (isTurret)//checks to see if the player has pressed e on the control pannel
        {
            TurretControl();
        }
    }

    // Update is called once per frame
    void TurretControl()
    {
        if (Input.GetMouseButton(0) && Time.time > nextShot)
        {
            FireTurret();
            nextShot = Time.time + fireDelay;
        }
        UpdateCamera(); 
    }

    void UpdateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        x_rotation += mouseX;
        y_rotation += mouseY;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);
        y_rotation = Mathf.Clamp(y_rotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(y_rotation, x_rotation, 0);
    }

    void FireTurret()   
    {
        GameObject turretProjectile = Instantiate(turretProjectilePrefab, turretBarrel.position, turretBarrel.rotation);
    }
}
