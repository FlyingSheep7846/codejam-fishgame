using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 150f;

    private Rigidbody rb;
    private float xRotation = 0f;    // pitch
    private float baseY;             // constant height

    private Transform cameraTransform;

    [SerializeField] private bool free = true;

    public AudioClip walking;
    private bool isWalking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cameraTransform = transform.GetChild(0);

        rb.freezeRotation = true;   // prevent physics from tipping us over
        // Cursor.lockState = CursorLockMode.Locked;

        baseY = transform.position.y;  // lock height
    }

    void Update()
    {
        if (free) Look();
    }

    void FixedUpdate()
    {
        if (free) Move();
        LockHeight();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // --- YAW (left-right) ---
        transform.Rotate(Vector3.up * mouseX);

        // --- PITCH (up-down) ---
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }


    void Move()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        bool isMoving = (x != 0 || z != 0);

        if (isMoving && !isWalking)
        {
            Debug.Log("is walking");
            isWalking = true;
            SoundManager.Instance.PlayLoop(SoundManager.Instance.walking, walking, .75f);
        }
        else if (!isMoving && isWalking)
        {
            Debug.Log("is not walking");
            isWalking = false;
            SoundManager.Instance.StopWalking();
        }

        // camera-relative horizontal movement
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

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

    public void ToggleFreeLook(bool free, float rotation)
    {
        this.free = free;

        if (!free)
        {
            Vector3 rot = new Vector3(0, rotation, 0);
            transform.DORotate(rot, 0.5f);
            cameraTransform.DORotate(rot, 0.5f);
        }
    }
}
