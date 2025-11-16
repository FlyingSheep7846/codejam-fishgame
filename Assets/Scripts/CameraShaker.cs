using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float shakeM = .02f;
    public float shakeF = 25f;

    private Vector3 pos;
    public bool isShaking = false;
    private float noiseTime;

    public static CameraShaker Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            if (Time.time >= noiseTime)
            {
                float x = Random.Range(-1f, 1f) * shakeM;
                float y = Random.Range(-1f, 1f) * shakeM;

                transform.localPosition = pos + new Vector3(x, y, 0f);

                noiseTime = Time.time + (1f / shakeF); // frequency!
            }
        }
        else
        {
            transform.localPosition = pos;
        }
    }

    public void ShakeCamera()
    {
        isShaking = true;
    }

    public void StopShaking()
    {
        isShaking=false;
        transform.localPosition = pos;
    }
}
