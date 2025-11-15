using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicFPSController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 6f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 150f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f; // pitch

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // lock mouse
    }

    void Update()
    {
        MouseLook();
        Movement();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Pitch (camera up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + mouseX, 0f);
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // Camera-relative movement but horizontal only
        Vector3 forward = transform.forward;
        Vector3 right   = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (right * x + forward * z) * speed;
        controller.Move(move * Time.deltaTime);
    }

}
