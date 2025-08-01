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
    [SerializeField]  private bool canDash = true;
    [SerializeField]  private bool isDashing = false;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField]  private bool isGrounded;

    private Rigidbody rb;


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
        //float downThrust = Mathf.Min(vInput, 0f); // Only downward when pressing S or stick down

        Vector3 thrustDirection = new Vector3(hInput, vInput, 0f);
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
    public void RefillFuel(float amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
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
    public Vector3 oldInput = Vector3.zero;

    public float fireRate = 0.1f;
    public float shootRange = 10000f;

    public bool showDebugRay = true;
    public float debugRayDuration = 0.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float nextFireTime = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {

    }

    void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 rayDirection = (mousePosition - transform.position).normalized;

        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, rayDirection, out hit, 300, -1);
        if (didHit)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red, debugRayDuration);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + rayDirection * 300, Color.red, debugRayDuration);
        }
        OnShoot(); // Recoil, screen shake
    }

    void OnHitTarget(RaycastHit2D hit)
    {
        Debug.Log($"Hit: {hit.collider.name} at {hit.point}");


    }

    void OnShoot()
    {
        Debug.Log("Shot fired!");
    }
}*/