using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class tweakVFX : MonoBehaviour
{
    private float startTime;
    public Color startColor;
    public Color endColor;
    private float time;
    public GameObject volume;
    public GameObject waterObj;
    private Material water;

    public Color startHorizon;
    public Color endHorizon;
    
    public Color startCloud;
    public Color endCloud;

    public Color startNadir;
    public Color endNadir;

    //private GameObject skyObj;

    private Material sky;


    public GameObject light;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        water = waterObj.GetComponent<MeshRenderer>().material;
        sky = RenderSettings.skybox;
        startTime = Timer.INSTANCE.GetTimeRemaining();
        
    }

    // Update is called once per frame
    void Update()
    {
        time = Timer.INSTANCE.GetTimeRemaining();

        if (volume.GetComponent<Volume>().profile.TryGet(out Vignette vignette))
        {
            float t = 1f - (time / startTime);      
            float intensity = Mathf.Lerp(0f, .3f, t);

            vignette.intensity.value = intensity;
            
        }
        
        if (volume.GetComponent<Volume>().profile.TryGet(out ChromaticAberration cA))
        {
            float t = 1f - (time / startTime);      
            float intensity = Mathf.Lerp(.3f, 1f, t);

            cA.intensity.value = intensity;
            
        }
        if (volume.GetComponent<Volume>().profile.TryGet(out Bloom bloom))
        {
            float t = 1f - (time / startTime);      
            float intensity = Mathf.Lerp(0f, 9f, t);

            bloom.intensity.value = intensity;
            
        }
        
        
        float x = 1f - (time / startTime);      
        water.SetColor("_Color_36218622185947c6a5ae36366d8e21d8", Color.Lerp(startColor, endColor, x)); 
        
        sky.SetColor("_HorizonColor", Color.Lerp(startHorizon, endHorizon, x));
        sky.SetColor("_NadirColor", Color.Lerp(startNadir, endNadir, x));
        sky.SetColor("_CloudColor", Color.Lerp(startCloud, endCloud, x));


        light.transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(50, -37.2f, 0), new Vector3(-72, -37.2f, 0), x));


    }
}
