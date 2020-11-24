using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    public float attackDelay = .6f;

    private CharacterStats myStats;

    public int damage = 10;
    public float range = 100f;
    public float impactForce = 30f;
    public Transform originObject;

    private AudioSource audioSource;
    public AudioClip shootingSound;
    public ParticleSystem muzzleFlash;
    
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Shoot();
        stats.TakeDamage(myStats.damage.GetValue());
    }
    
    public void Shoot()
    {
        muzzleFlash.Play();
        
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
