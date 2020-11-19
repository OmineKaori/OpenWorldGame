using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private PlayerManager playerManager;
    private CharacterStats myStats;

    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    
    public override void Interact()
    {
        base.Interact();
        EnemyCombat playerCombat = playerManager.player.GetComponent<EnemyCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}