using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretSystem : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float x_rotation;
    float y_rotation;
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
    
    private void OnLook(InputValue inputValue)
    {
        Vector2 mouse = inputValue.Get<Vector2>() * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        x_rotation += mouse.x;
        y_rotation += mouse.y;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);
        y_rotation = Mathf.Clamp(y_rotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(y_rotation, x_rotation, 0);
    }
    
    private void OnFire(InputValue inputValue)
    {
        if (Time.time > nextShot)
        {
            FireTurret();
            turretAnimator.SetTrigger("Fire");
            StartCoroutine(ReloadTimer());
            nextShot = Time.time + fireDelay;
        }
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
