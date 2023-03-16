using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionReset : Interactable
{
    private struct PositionAndRotation { public Vector3 position; public Quaternion rotation; }

    private int interactTimeoutFramesMax => Mathf.RoundToInt(interactTimeoutSeconds * GameManager.targetFrameRate);

    [SerializeField] private float interactTimeoutSeconds = 1f;

    private Dictionary<Interactable, PositionAndRotation> interactableInitialPositionsAndRotations = new Dictionary<Interactable, PositionAndRotation>();
    private int interactTimeoutFrames = 0;
    private Color meshRendererMaterialDefaultColor = Color.gray;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (interactTimeoutFrames > 0) return;
        interactTimeoutFrames = interactTimeoutFramesMax;
        ResetAllInteractablePositions();
        // set color
        transform.root.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }

    // populate interactableInitialPositionsAndRotations
    private void Start()
    {
        meshRendererMaterialDefaultColor = transform.root.GetComponentInChildren<MeshRenderer>().material.color;

        Interactable[] interactables = FindObjectsOfType<Interactable>();
        foreach (Interactable e in interactables)
        {
            if (e == this) continue;

            Rigidbody interactableRigidbody = e.transform.root.GetComponent<Rigidbody>();
            PositionAndRotation initialPositionAndRotation = new PositionAndRotation { position = interactableRigidbody.position, rotation = interactableRigidbody.rotation };
            interactableInitialPositionsAndRotations.Add(e, initialPositionAndRotation);
        }
    }

    private void Update()
    {
        if (interactTimeoutFrames <= 0) return;
        --interactTimeoutFrames;
        if (interactTimeoutFrames > 0) return;
        // reset color
        transform.root.GetComponentInChildren<MeshRenderer>().material.color = meshRendererMaterialDefaultColor;
    }

    private void ResetAllInteractablePositions()
    {
        Interactable[] interactables = FindObjectsOfType<Interactable>();
        foreach (Interactable e in interactables)
        {
            if (e == this) continue;
            Rigidbody interactableRigidbody = e.transform.root.GetComponent<Rigidbody>();
            if (!interactableInitialPositionsAndRotations.ContainsKey(e)) continue;
            PositionAndRotation initialPositionAndRotation = interactableInitialPositionsAndRotations[e];
            interactableRigidbody.velocity = Vector3.zero;
            interactableRigidbody.angularVelocity = Vector3.zero;
            interactableRigidbody.MovePosition(initialPositionAndRotation.position);
            interactableRigidbody.MoveRotation(initialPositionAndRotation.rotation);
        }
    }
}
