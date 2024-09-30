using UnityEngine;
using UnityEngine.UI;  // THIS CODE IS TO CONTROL THE CUE BALL - ALL OF THE CODE IS LABELED AS OF THIER FUNCTIONS - ITS.SHAHEEN'S CODE //

public class CueBallController : MonoBehaviour
{
    // Public Variables
    [Header("Ball Physics Settings")]
    public float strikeForce = 500f;             // Default force applied when the ball is struck
    public float frictionCoefficient = 0.05f;    // Friction to slow the ball
    public float minimumSpeed = 0.01f;           // Speed threshold for stopping the ball
    public PhysicMaterial ballPhysicsMaterial;   // Physics material for bounces and friction

    [Header("Aiming Settings")]
    public Transform cueBall;                    // Reference to the cue ball's transform
    public float moveSpeed = 5f;                 // Speed for moving the cue ball
    public float rotationSpeed = 100f;           // Speed for rotating aim direction
    public LineRenderer lineRenderer;            // Line renderer to visualize shot direction

    [Header("UI Settings")]
    public Slider forceSlider;                   // Reference to the force slider UI
    public Text forceValueText;                  // Text to display the force value (optional)

    // Private Variables
    private Rigidbody rb;                        // Rigidbody of the cue ball
    private Vector3 aimDirection = Vector3.forward;  // Direction to shoot the ball

    void Start()
    {
        // Get the Rigidbody of the ball and assign the physics material
        rb = GetComponent<Rigidbody>();
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.material = ballPhysicsMaterial;

        // Set Rigidbody drag properties for realistic movement
        rb.angularDrag = 0.1f;
        rb.drag = 0.05f;

        // Set initial force from the slider
        strikeForce = forceSlider.value;

        // Update the force value text, if it exists
        if (forceValueText != null)
        {
            forceValueText.text = "Force: " + strikeForce.ToString("F0");
        }

        // Add listener to update force as the slider changes
        forceSlider.onValueChanged.AddListener(UpdateForce);
    }

    void Update()
    {
        // Adjust the position and aim direction of the cue ball before shooting
        HandlePositioningAndAiming();

        // Shoot the ball when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StrikeBall(aimDirection);
        }
    }

    // Handle moving the cue ball and adjusting its aim direction
    void HandlePositioningAndAiming()
    {
        // Move the cue ball left or right (horizontal movement)
        float moveInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        cueBall.Translate(Vector3.right * moveInput);

        // Rotate the aim direction based on input
        float rotateInput = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        aimDirection = Quaternion.Euler(0, rotateInput, 0) * aimDirection;

        // Update the Line Renderer to show the aiming direction
        UpdateLineRenderer();
    }

    // Update the Line Renderer to visualize the direction of the shot
    void UpdateLineRenderer()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, cueBall.position);
            lineRenderer.SetPosition(1, cueBall.position + aimDirection * 30f);  // GARRIS
        }
    }

    // Method to shoot the ball
    public void StrikeBall(Vector3 direction)
    {
        rb.AddForce(direction.normalized * strikeForce, ForceMode.Impulse);
    }

    // Method to update force when slider changes
    public void UpdateForce(float value)
    {
        strikeForce = value;

        // Update the force value text
        if (forceValueText != null)
        {
            forceValueText.text = "Force: " + strikeForce.ToString("F0");
        }
    }

    void FixedUpdate()
    {
        // Apply friction to the ball as it moves
        ApplyFriction();
    }

    // Slow down the ball over time using friction
    private void ApplyFriction()
    {
        float speed = rb.velocity.magnitude;

        if (speed > minimumSpeed)
        {
            Vector3 friction = -rb.velocity.normalized * frictionCoefficient;
            rb.AddForce(friction, ForceMode.Acceleration);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
