using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretSystem : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private Vector2 rotation;
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
        rotation += mouse;
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
    }
    
    private void OnFire(InputValue inputValue)
    {
        
        if (!isTurret) return;
        
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
