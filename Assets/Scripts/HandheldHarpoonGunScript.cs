using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandheldHarpoonGunScript : MonoBehaviour
{
    public GameObject HarpoonProjectile;
    public GameObject ProjectileSpawnPoint;
    public GameObject HarpoonVisual;
    public float projectileSpeed;
    public bool reloading;
    public float reloadTime;

    private void Start()
    {
        reloading = false;
        HarpoonVisual.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !reloading)
        {
            HarpoonVisual.SetActive(false);
            reloading = true;
            GameObject harpoon = Instantiate(HarpoonProjectile, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
            harpoon.GetComponent<HandheldHarpoonProjectileScript>().projectileSpeed = projectileSpeed;
            GlobalSoundsManager.instance.PlayHandheldHarpoon();
            StartCoroutine(ReloadTimer(reloadTime));
        }
    }
    private IEnumerator ReloadTimer(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        HarpoonVisual.SetActive(true);
    }
}
