using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable
{
    public GameObject destroyedVersion;

    public override void Interact()
    {
        base.Interact();
        
        if (Inventory.instance.items.FindAll(item => item.name == "Key").Count == 3)
            Open();
    }

    public void Open()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
