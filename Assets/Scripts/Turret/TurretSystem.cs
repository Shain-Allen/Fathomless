using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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
    public Rigidbody subRb;
    public GameObject VisHarpoon;
    public Animator turretAnimator;

    void Update()
    {
        if (isTurret)
        {
            VisualHarpoon();
            TurretControl();
            UpdateCamera();
        }
    }


    void VisualHarpoon()
    {
        if (Time.time > nextShot)
        {
            VisHarpoon.SetActive(true);
        }
        else
        {
            VisHarpoon.SetActive(false);
        }
    }
    void TurretControl()
    {
        if (Input.GetMouseButton(0) && Time.time > nextShot)
        {
            FireTurret();
            turretAnimator.SetTrigger("Fire");
            StartCoroutine(ReloadTimer());
            nextShot = Time.time + fireDelay;
        }
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
        GlobalSoundsManager.instance.PlayHarpoon();
        GameObject turretProjectile = Instantiate(turretProjectilePrefab, turretBarrel.position, turretBarrel.rotation);
        TurretProjectile proj = turretProjectile.transform.GetChild(0).gameObject.GetComponent<TurretProjectile>();
        proj.subRb = subRb;
    }

    public IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(fireDelay - 0.2f);
        GlobalSoundsManager.instance.PlayReload();
    }
}
