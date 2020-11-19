using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    public int healthValue = 5;

    public override void Use()
    {
        base.Use();

        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.HealHealth(healthValue);
        RemoveFromInventory();
    }
}
