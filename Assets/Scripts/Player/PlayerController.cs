using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Thruster Settings")]
    public float thrusterForce = 10f;
    public float jumpForce = 15f;

    [Header("Fuel Settings")]
    public float maxFuel = 100f;
    public float currentFuel;
    public float fuelConsumptionRate = 10f; // per second while thrusting
    public float fuelRechargeRate = 5f;     // per second when grounded

    [Header("Dash Settings")]
    [SerializeField] private float dashForce = 500f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private float dashFuelCost = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.3f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool canDash = true;
    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotation during collisions
        currentFuel = maxFuel;
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        float downThrust = Mathf.Min(vInput, 0f); // Only downward when pressing S or stick down

        Vector3 thrustDirection = new Vector3(hInput, downThrust, 0f);
        bool isThrusting = thrustDirection.magnitude > 0.01f;

        // Dash input with fuel check
        if (Input.GetKeyDown(dashKey) && canDash && !isDashing)
        {
            if (currentFuel >= dashFuelCost)
            {
                currentFuel -= dashFuelCost;
                StartCoroutine(Dash());
            }
            else
            {
                Debug.Log("Not enough fuel to dash!");
            }
        }

        // Apply thruster force in air
        if (!isGrounded && isThrusting && currentFuel > 0f)
        {
            rb.AddForce(thrustDirection * thrusterForce);
            currentFuel -= fuelConsumptionRate * Time.fixedDeltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
        }

        // Fuel recharge and jump when grounded
        if (isGrounded)
        {
            currentFuel += fuelRechargeRate * Time.fixedDeltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector3 dashDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;

        if (dashDirection == Vector3.zero)
            dashDirection = transform.forward;

        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}


/*using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    [SerializeField]
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        Vector3 move = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * input;
        controller.Move(move.normalized * moveSpeed * Time.deltaTime);
        Debug.Log($"i/p {input} Move {move.normalized * moveSpeed * Time.deltaTime}");

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}*/