using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.TerrainAPI;

public class Gun : MonoBehaviour
{

    public int damage = 10;
    public float range = 100f;
    public float impactForce = 30f;
    private bool isReloading = false;

    public int maxAmmo = 10;
    private int currentAmmo = -1;
    public float reloadTime = 1f;

    public AudioClip shootingSound;
    public AudioClip gunReloading;
    private AudioSource audioSource;

    public Animator animator;
    
    public Transform originObject;
    public ParticleSystem muzzleFlash;
    private static readonly int Reloading = Animator.StringToHash("Reloading");

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
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        
        animator.SetBool(Reloading, true);
        
        yield return new WaitForSeconds(reloadTime);
        
        audioSource.PlayOneShot(shootingSound);
        animator.SetBool(Reloading, false);
        
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    
    public void Shoot()
    {
        if (isReloading)
            return;
        
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
        
        audioSource.PlayOneShot(shootingSound);
    }
}
