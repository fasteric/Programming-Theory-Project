using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPush : Interactable
{
    [SerializeField] private float pushStrength = 1;

    private new Rigidbody rigidbody = null;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        Vector3 pushDirection = (transform.position - playerInteraction.transform.position).normalized;
        rigidbody.AddForce(pushDirection * pushStrength, ForceMode.Impulse);
    }

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }
}
