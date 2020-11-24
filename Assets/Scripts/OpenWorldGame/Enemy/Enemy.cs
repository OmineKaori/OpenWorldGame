using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }
    
    public override void Interact()
    {
        base.Interact();
        EnemyCombat playerCombat = Player.instance.GetComponent<EnemyCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}