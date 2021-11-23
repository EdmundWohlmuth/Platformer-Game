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
    private Vector3 move;
    private Vector2 movement;

    public bool isGrounded;
    public bool canWallJump;
    private bool isJumping;
    public bool hasWallJumped = false;

    private float playerSpeed = 10.0f;
    private float gravityValue = -9.81f;
    private float fallMultiplier = 2.0f;
    private float rotationSpeed = 8.0f;
    private float wallJump = 5f;

    private float wallJumpTimer = 0.5f;
    private float wallJumpTimerMax = 0.5f;

    public float jumpResetTime = 0.5f;
    private float jumpResetMax = 0.5f;

    private float[] jump = new float[3];

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

        //Jump arrays
        jump[0] = 3.5f;
        jump[1] = 5f;
        jump[2] = 7.5f;
    }

    void Update()
    {
        MoveControl();       
        JumpControl();
        WallJumpTimer();
        TimerStarted();
        GravityControl();
    }
// ------------------ Controls -----------------------------------------------------------------
    void MoveControl()
    {
        //Movement
        movement = movementControl.action.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
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
// ----------------- Gravity -------------------------------------------------------------------
    void GravityControl()
    {
        // Grounded Check
        isGrounded = controller.isGrounded;
        bool isFalling = playerVelocity.y <= 0.0f;

        if (isGrounded && playerVelocity.y < 0) 
        {
            isJumping = true;
            playerVelocity.y = -0.5f;           
        }
        // Falling Check
        if (isFalling)
        {
            playerVelocity.y = playerVelocity.y + (gravityValue * fallMultiplier * Time.deltaTime);
        }
    }
// ---------------- Jump Code -------------------------------------------------------
    void JumpControl()
    {
        // Jump Control
        if (jumpControl.action.triggered && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jump[jumpType] * -3.0f * gravityValue);
            jumpType++;
            jumpResetTime = jumpResetMax;
        }
        if (jumpType == 3)
        {
            jumpType = 0;
        }

        playerVelocity.y += gravityValue * fallMultiplier * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // ---------------- Wall Jump -----------------------------------------------------------
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isGrounded && hit.normal.y < 0.1f)
        {
            if (jumpControl.action.triggered)
            {
                wallJumpTimer = wallJumpTimerMax;

                playerVelocity.y += Mathf.Sqrt(wallJump * -3.0f * gravityValue);                
                playerVelocity += hit.normal * 15f;  
                
                hasWallJumped = true;

                Debug.Log("Would've wall jumped");
                WallJumpTimer();                
            }
        }    
    }

    // ---------------- Jump Reset ------------------------------------------------

    void TimerStarted()
    {
        if (isGrounded && isJumping)
        {
            jumpResetTime -= Time.deltaTime;
        }
        else
        {
            return;
        }
        
        if (jumpResetTime <= 0.0f)
        {
            jumpResetTime = jumpResetMax;
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        jumpType = 0;
    }

    // ----------- wall jump timer ------------------------------------------------------------
  
    void WallJumpTimer()
    {
        if (hasWallJumped) // velocity timer
        {
            wallJumpTimer -= Time.deltaTime;        
        }
        else
        {
            return;
        }

        if (wallJumpTimer <= 0.0f)
        {
            playerVelocity = Vector3.zero;           
            hasWallJumped = false;
        }
    }

}

