using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewController : MonoBehaviour
{
    private CharacterController controller;

    public InputActionReference movementControl;
    public InputActionReference jumpControl;

    private Transform cameraMainTransform;

    private Vector3 playerVelocity;

    private bool groundedPlayer;

    private float playerSpeed = 10.0f;
    private float gravityValue = -9.81f;
    private float fallMultiplier = 2.0f;
    private float rotationSpeed = 8.0f;

    private float jump = 3.5f;

    public int jumpType = 0;

    // ---------- Enable / Disable -------------------------------------------------------------
    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }
    //------------ Start / Update -------------------------------------------------------------
    private void Start()
    {
        //establish controller / forward
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
    }

    void Update()
    {
        MoveControl();       
        JumpControl();
        isGroudned();
    }
// ------------------ Controls -----------------------------------------------------------------
    void MoveControl()
    {
        //Movement
        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
 
        if (move != Vector3.zero)
        {
            float targetAngel = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngel, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
           // gameObject.transform.forward = move;
        }
    }

    void PlayerRotation()
    {

    }

    void isGroudned()
    {
        // Grounded Check
        groundedPlayer = controller.isGrounded;
        bool isFalling = playerVelocity.y <= 0.0f;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        // Falling Check
        if (isFalling)
        {
            playerVelocity.y = playerVelocity.y + (gravityValue * fallMultiplier * Time.deltaTime);
        }
    }

    void JumpControl()
    {
        // Jump Control
        if (jumpControl.action.triggered && groundedPlayer && jumpType == 0)
        {
            playerVelocity.y += Mathf.Sqrt(jump * -3.0f * gravityValue);            
        }

        playerVelocity.y += gravityValue * fallMultiplier * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

