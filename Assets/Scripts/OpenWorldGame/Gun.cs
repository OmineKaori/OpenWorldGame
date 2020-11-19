using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    private bool isReloading = false;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;

    public AudioClip shootingSound;
    private AudioSource audioSource;
    
    public Transform originObject;
    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = shootingSound;
        
        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
    }
    
    void Update()
    {
        if (isReloading)
            return;
        
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        
        yield return new WaitForSeconds(reloadTime);
        
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    
    public void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;
        
        RaycastHit hit;
        if (Physics.Raycast(originObject.transform.position, originObject.transform.forward, out hit, range))
        {
            CharacterStats targetStats = hit.transform.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                targetStats.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
        
        if (Input.GetButtonDown("Fire1"))
            audioSource.Play();
        if (Input.GetButtonUp("Fire1"))
            audioSource.Stop();
    }
}
