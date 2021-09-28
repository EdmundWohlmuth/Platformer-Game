using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterController : MonoBehaviour
{
    PlayerController playerController;
    CharacterController charController;

    public float moveSpeed = 5.0f;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    bool isMovementPressed;
    bool isRunning;

    float rotationFactorPerFrame = 15.0f;

    private void Awake()
    {
        playerController = new PlayerController();
        charController = GetComponent<CharacterController>();

        playerController.keyboard.MoveKeys.started += MovementInput;
        playerController.keyboard.MoveKeys.performed += MovementInput;
        playerController.keyboard.MoveKeys.canceled += MovementInput;
        playerController.keyboard.Run.started += OnRun;
        playerController.keyboard.Run.canceled += OnRun;

    }


    // Update is called once per frame
    private void Update()
    {
        charController.Move(currentMovement * Time.deltaTime);
        HandleRotation();
        HandleGravity();

        if (isRunning)
        {
            charController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            charController.Move(currentMovement * Time.deltaTime);
        }
    }

    void OnRun (InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void MovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * moveSpeed;
        currentMovement.z = currentMovementInput.y * moveSpeed;

        currentRunMovement.x = currentMovementInput.x * 10.0f;
        currentRunMovement.z = currentMovementInput.y * 10.0f;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void HandleGravity()
    {
        if (charController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity * Time.deltaTime;
            currentRunMovement.y = groundedGravity * Time.deltaTime;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        playerController.keyboard.Enable();
    }
    private void OnDisable()
    {
        playerController.keyboard.Disable();
    }
}
