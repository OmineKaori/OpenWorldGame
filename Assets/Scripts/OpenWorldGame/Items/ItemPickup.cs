using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    
    AudioSource audioSource;
    public AudioClip gettingItem;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            audioSource.PlayOneShot(gettingItem);
            Destroy(gameObject);
        }
    }
}
