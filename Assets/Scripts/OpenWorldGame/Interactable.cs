using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    private Transform player;
    
    public virtual void Interact()
    {
        // this method is meant to be overwritten.
    }

    void Update()
    {
        player = Player.instance.transform;
        float distance = Vector3.Distance(player.position, interactionTransform.position);
        
        if (distance <= radius)
        {
            Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
