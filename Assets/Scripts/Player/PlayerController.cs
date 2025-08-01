using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Thruster Settings")]
    public float thrusterForce = 10f;
    public float jumpForce = 15f;
    [SerializeField] private float airDrag = 1.5f;
    [SerializeField] private float groundedDrag = 5f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Acceleration Settings")]
    [SerializeField] private AnimationCurve accelerationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float accelerationMultiplier = 1f;
    private float currentThrustLerp = 0f;


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
    private bool dashKeyCheck = false, jumpKeyCheck = false;

    float hInput, vInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotation during collisions
        currentFuel = maxFuel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
            dashKeyCheck = true;
        if (Input.GetKeyUp(dashKey))
            dashKeyCheck = false;

        if (Input.GetButtonDown("Jump"))
            jumpKeyCheck = true;
        if (Input.GetButtonUp("Jump"))
            jumpKeyCheck = false;

        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        Vector3 thrustDirection = new Vector3(hInput, vInput, 0f);
        bool isThrusting = thrustDirection.magnitude > 0.01f;

        rb.drag = isGrounded ? groundedDrag : airDrag;
        Debug.Log($"thrust direction {thrustDirection} is thrustin {isThrusting}");

        if (!isGrounded && isThrusting && currentFuel > 0f)
        {
            // Increase lerp factor when thrusting
            currentThrustLerp += Time.fixedDeltaTime * accelerationMultiplier;
            currentThrustLerp = Mathf.Clamp01(currentThrustLerp);
        }
        else
        {
            // Decay lerp factor when not thrusting
            currentThrustLerp -= Time.fixedDeltaTime * accelerationMultiplier;
            currentThrustLerp = Mathf.Clamp01(currentThrustLerp);
        }

        float thrustFactor = accelerationCurve.Evaluate(currentThrustLerp);
        rb.AddForce(thrustDirection * thrusterForce * thrustFactor);
        if (isThrusting)
        {
            currentFuel -= fuelConsumptionRate * Time.fixedDeltaTime * thrustFactor;
        }


        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (dashKeyCheck && canDash && !isDashing)
        {
            if (currentFuel >= dashFuelCost)
            {
                currentFuel -= dashFuelCost;
                StartCoroutine(Dash());
            }
        }

        if (isGrounded)
        {
            currentFuel += fuelRechargeRate * Time.fixedDeltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

            if (jumpKeyCheck)
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