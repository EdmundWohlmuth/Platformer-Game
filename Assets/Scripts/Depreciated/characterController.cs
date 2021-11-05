using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterController : MonoBehaviour
{

    //declerations
    PlayerController playerController;
    CharacterController charController;

    public Transform cam;

    public float moveSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float inititalJumpVelocity;
    public float maxJumpHeight = 1.0f;
    public float maxJumpTime = 0.5f;
    public float fallMultiplier = 2.0f;

    int jumpCount = 0;

    float gravity = -9.8f;
    float groundedGravity = -.05f;
    float rotationFactorPerFrame = 15.0f;

    Vector2 currentMovementInput;

    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    Vector3 movement;

    public Transform cameraTransform;

    bool isMovementPressed;
    bool isRunning;
    bool isJumpPressed = false;
    bool isJumping = false;
    bool isFalling;



    private void Awake()
    {
        playerController = new PlayerController();
        charController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;


        // paths to the input system and determins if it is pressed or not, then runs a method
        playerController.keyboard.MoveKeys.started += MovementInput;
        playerController.keyboard.MoveKeys.performed += MovementInput; //for controllers
        playerController.keyboard.MoveKeys.canceled += MovementInput;

        playerController.keyboard.Run.started += OnRun;
        playerController.keyboard.Run.canceled += OnRun;

        playerController.keyboard.Jump.started += OnJump;
        playerController.keyboard.Jump.canceled += OnJump;

        JumpVariables();
    }

    // Update is called once per frame
    private void Update()
    {

        charController.Move(currentMovement * Time.deltaTime);
        HandleRotation();

        if (isRunning)
        {
            currentRunMovement.x = currentMovement.x;
            currentRunMovement.z = currentMovement.z;

        }
        else
        {
            appliedMovement.x = currentMovement.x;
            appliedMovement.z = currentMovement.z;
        }

        charController.Move(appliedMovement * Time.deltaTime);

        HandleGravity();
        HandleJump();
    }

    void JumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        inititalJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void HandleJump ()
    {
        if (!isJumping && charController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = inititalJumpVelocity;
            appliedMovement.y = inititalJumpVelocity;
        }

        else if (!isJumpPressed && isJumping && charController.isGrounded)
        {
            isJumping = false;
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
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
        //takes input system "up", "down", "left", "right" and applies it to character then * moveSpeed
        currentMovementInput = context.ReadValue<Vector2>();

        currentMovement.x = currentMovementInput.x * moveSpeed;
        currentMovement.z = currentMovementInput.y * moveSpeed;

        //Need diffrent variables for the same value

        // isgrounded bool
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void HandleGravity()
    {
        isFalling = currentMovement.y <= 0.0f || isJumpPressed;

        if (charController.isGrounded)
        {           
            currentMovement.y = groundedGravity * Time.deltaTime; // '=' == set speed "+=" == acceleration
            currentRunMovement.y = groundedGravity * Time.deltaTime;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * 0.5f, -20.0f);
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
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
