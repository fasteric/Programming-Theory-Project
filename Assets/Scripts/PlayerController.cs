using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private Vector3 inputToWorldHorizontal = (Vector3.right - Vector3.forward).normalized;
    [SerializeField] private Vector3 inputToWorldVertical = (Vector3.right + Vector3.forward).normalized;

    private CharacterController characterController = null;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        int inputHorizontal = 0;
        if (Input.GetKey(KeyCode.D)) ++inputHorizontal;
        if (Input.GetKey(KeyCode.A)) --inputHorizontal;

        int inputVertical = 0;
        if (Input.GetKey(KeyCode.W)) ++inputVertical;
        if (Input.GetKey(KeyCode.S)) --inputVertical;

        Vector3 moveDirection = Vector3.zero;
        moveDirection += inputHorizontal * inputToWorldHorizontal;
        moveDirection += inputVertical * inputToWorldVertical;

        Vector3 motion = moveDirection * speed;
        characterController.SimpleMove(motion);
    }
}
