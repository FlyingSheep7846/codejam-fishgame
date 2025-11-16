using UnityEngine;

public class Bobbing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Bobbing Settings")]
    public float bobHeight = 0.25f;       // how high it moves up/down
    public float bobSpeed = 1.2f;         // how fast it bobs

    [Header("Rotation Settings")]
    public float tiltAmount = 2f;         // degrees of tilt
    public float tiltSpeed = 0.8f;        // speed of rotation swaying

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localRotation;
    }

    void Update()
    {
        float t = Time.time;

        // Vertical bobbing (sin wave)
        float newY = startPos.y + Mathf.Sin(t * bobSpeed) * bobHeight;

        // Gentle roll and pitch (cos + sin offset)
        float rotX = Mathf.Sin(t * tiltSpeed) * tiltAmount;
        float rotZ = Mathf.Cos(t * tiltSpeed * 0.7f) * tiltAmount;

        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
        transform.localRotation = Quaternion.Euler(rotX, startRot.eulerAngles.y, rotZ);
    }
}
