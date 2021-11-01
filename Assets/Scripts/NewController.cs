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

    public bool groundedPlayer;

    private float playerSpeed = 10.0f;
    private float gravityValue = -9.81f;
    private float fallMultiplier = 2.0f;
    private float rotationSpeed = 8.0f;
    private float waitTime = 0.5f;

    private float lowJump = 3.5f;
    private float midJump = 5f;
    private float highJump = 7.0f;

    private float[] jump = new float[3];

    public int jumpCount = 0;

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

        //Jump Array
        jump[0] = lowJump;
        jump[1] = midJump;
        jump[2] = highJump;
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

    void isGroudned()
    {
        // Grounded Check
        groundedPlayer = controller.isGrounded;
        bool isFalling = playerVelocity.y <= 0.0f;

        if (groundedPlayer && playerVelocity.y < 0)
        {           
            playerVelocity.y = 0;         
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
        if (jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jump[jumpCount] * -3.0f * gravityValue);
            jumpCount++;           
        }
        if (jumpCount == 3)
        {
            jumpCount = 0;
        }

        playerVelocity.y += gravityValue * fallMultiplier * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator jumpResetRoutine()
    {
        yield return new WaitForSeconds(waitTime);
        jumpCount = 0;
    }
}

