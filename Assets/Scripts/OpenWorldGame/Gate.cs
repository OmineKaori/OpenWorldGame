using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable
{
    public GameObject destroyedVersion;
    public AudioClip gateBreaking;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gateBreaking;
    }

    public override void Interact()
    {
        base.Interact();
        
        if (Inventory.instance.items.FindAll(item => item.name == "Key").Count == 3)
            Open();
    }

    public void Open()
    {
        audioSource.Play();
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
