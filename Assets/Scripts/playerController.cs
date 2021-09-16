using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerController : MonoBehaviour
{
    private Rigidbody rb;

    private int count;

    private float movementX;
    private float movementY;

    public bool isGrounded;
    public bool canWallJump;

    public float playerSpeed = 10.0f;
    public float jumpPadHeght = 15.0f;
    public float playerJump = 5.0f;
    public float playerBoost = 15.0f;
    public float playerDown = 20.0f;
    public float maxSpeed = 15.0f;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Basic Movement
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * playerSpeed);

        //Speed limiter while on the ground
        if (rb.velocity.magnitude > maxSpeed && isGrounded)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // UI
    void SetCountText()
    {
        countText.text = "Points: " + count.ToString();
    }
    // Game Interactions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        if (other.gameObject.CompareTag("FinnishLine"))
        {
            winTextObject.SetActive(true);
        }

        if (other.gameObject.CompareTag("JumpPad"))
        {
            rb.AddForce(Vector3.up * jumpPadHeght, ForceMode.Impulse);
        }          
    }

    //Collision detection
    private void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (!collision.gameObject.CompareTag("Ground") && collision.gameObject.CompareTag("Wall"))
        {
            canWallJump = true;
            Debug.Log("Can wallJump");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            canWallJump = false;
            Debug.Log("Cannot wallJump");
        }
    }

    //Jumping
    void OnJump(InputValue movementValue)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * playerJump, ForceMode.Impulse);
        }

        if (canWallJump)
        {

        }
    }

    //Extra Schmovement
    void OnBoost(InputValue movementValue)
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        if (isGrounded)
        {
            rb.AddForce(movement * playerBoost, ForceMode.Impulse);
        }
        
    }

    void OnDownBurst(InputValue movementValue)
    {
        if (isGrounded == false)
        {
            rb.AddForce(Vector3.down * playerDown, ForceMode.VelocityChange);
        }
        
    }
}
