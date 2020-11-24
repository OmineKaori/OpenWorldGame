using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject explosion;
    
    public override void Die()
    {
        base.Die();
        
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 10);
        Destroy(gameObject);
    }
}
