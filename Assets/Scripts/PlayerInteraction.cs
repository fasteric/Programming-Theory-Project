using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private HashSet<Collider> colliders = new HashSet<Collider>();

    private void OnTriggerEnter(Collider other) => colliders.Add(other);
    private void OnTriggerExit(Collider other) => colliders.Remove(other);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ABSTRACTION
            Interact();
        }
    }

    private void Interact()
    {
        // find nearest collider
        Interactable interactableNearest = null;
        float colliderNearestDistSqr = float.PositiveInfinity;
        foreach (Collider collider in colliders)
        {
            float distSqr = (collider.transform.position - transform.position).sqrMagnitude;
            if (distSqr >= colliderNearestDistSqr) continue;

            Interactable interactable = collider.GetComponent<Interactable>();
            if (!interactable) continue;

            interactableNearest = interactable;
            colliderNearestDistSqr = distSqr;
        }

        if (!interactableNearest)
        {
            Debug.Log(name + " have nothing to interact with");
            return;
        }

        Debug.Log(name + " interact with " + interactableNearest.name);
        interactableNearest.Interact(this);
    }
}
