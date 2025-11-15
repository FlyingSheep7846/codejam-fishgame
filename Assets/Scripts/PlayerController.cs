using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 150f;

    private Rigidbody rb;
    private float xRotation = 0f;    // pitch
    private float baseY;             // constant height

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;   // prevent physics from tipping us over
        Cursor.lockState = CursorLockMode.Locked;

        baseY = transform.position.y;  // lock height
    }

    void Update()
    {
        Look();
    }

    void FixedUpdate()
    {
        Move();
        LockHeight();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // pitch (vertical rotation)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        // Apply rotation
        transform.rotation = Quaternion.Euler(
            xRotation,
            transform.eulerAngles.y + mouseX,
            0f
        );
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // camera-relative horizontal movement
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (right * x + forward * z);

        // set velocity directly (no vertical movement)
        Vector3 newVelocity = moveDirection * moveSpeed;
        newVelocity.y = rb.linearVelocity.y;   // keep any RB Y (if gravity on) or set to 0

        rb.linearVelocity = newVelocity;
    }

    void LockHeight()
    {
        // force height to remain constant
        Vector3 pos = transform.position;
        pos.y = baseY;
        transform.position = pos;
    }
}
