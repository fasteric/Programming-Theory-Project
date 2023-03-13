using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOrbit : Interactable
{
    [SerializeField] private float orbitDuration = 4f;
    [SerializeField] private float orbitRadius = 4f;
    [SerializeField] private float orbitAngularVelocity = 2f * Mathf.PI;

    private bool isOrbiting => orbitFramesRemaining > 0;
    private int orbitFramesMax => Mathf.RoundToInt(orbitDuration * GameManager.targetFrameRate);

    private new Rigidbody rigidbody = null;

    private Transform orbitTargetTransform = null;
    private float orbitAngleInitial = 0f;
    private int orbitFramesRemaining = 0;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (isOrbiting) return;

        orbitTargetTransform = playerInteraction.transform;

        Vector3 displacement = transform.position - orbitTargetTransform.position;
        orbitAngleInitial = Mathf.Atan2(displacement.z, displacement.x);
        orbitAngleInitial = Mathf.Repeat(orbitAngleInitial, GameManager.TAU);

        orbitFramesRemaining = orbitFramesMax;
    }

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (isOrbiting) Orbit();
    }

    private void Orbit()
    {
        // unity is not unreal, you don't have to be overly cautious
        //if (!orbitTargetTransform) { orbitFramesRemaining = 0; return; }

        --orbitFramesRemaining;

        // calculate current orbit angle
        int orbitFrameNumber = orbitFramesMax - orbitFramesRemaining;
        float orbitAngleAdditional = orbitAngularVelocity * orbitFrameNumber / GameManager.targetFrameRateFloat;
        float orbitAngle = orbitAngleInitial + orbitAngleAdditional;

        // calculate expected orbit position
        Vector3 orbitPosition = orbitTargetTransform.position;
        orbitPosition.x += Mathf.Cos(orbitAngle) * orbitRadius;
        orbitPosition.z += Mathf.Sin(orbitAngle) * orbitRadius;

        // calculate orbit velocity
        Vector3 orbitDeltaPosition = orbitPosition - rigidbody.position;
        Vector3 velocity = orbitDeltaPosition * GameManager.targetFrameRate;
        rigidbody.velocity = velocity;

        // let physics update handle the rest
    }
}
