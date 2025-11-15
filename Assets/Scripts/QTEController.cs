using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QTEController : MonoBehaviour
{
    float timer = 0f;
    float qteIntervalTimer = 0f;
    [SerializeField] CanvasGroup qteCg;
    [SerializeField] Slider slider;

    // editable values
    public float QteTime;
    public float failedDecreaseAmount = 0.15f;

    KeyCode[] possibleQtes = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D};
    private KeyCode currentQte = KeyCode.None;

    private FishingRodController _fishingRodController;
    private FishingRodController fishingRodController
    {
        get
        {
            if (_fishingRodController == null) 
                _fishingRodController = GetComponent<FishingRodController>();

            return _fishingRodController;
        }
    }


    private bool isInQte = false;


    // Update is called once per frame
    void Update()
    {
        // code for is in qte
        if (isInQte){
            timer -= Time.deltaTime;
            slider.value = timer / QteTime;
        
            CheckForInput(currentQte);

            if (timer <= 0f)
            {
                Debug.Log("failed fish");
                FailQTE();
            }

        // code for is waiting for next qte
        } else
        {
            qteIntervalTimer -= Time.deltaTime;
            if (qteIntervalTimer <= 0f)
            {
                StartQTE();
            }
        }
    }

    void StartQTE()
    {
        timer = 1.5f;
        currentQte = possibleQtes[Random.Range(0, 4)];
        isInQte = true;
    }

    void CompleteQTE()
    {
        isInQte = false;

    }

    void FailQTE()
    {
        isInQte = false;
        _fishingRodController.DecreaseProgressBar(failedDecreaseAmount);
        

    }

    void CheckForInput(KeyCode qte)
    {
        if (qte == KeyCode.None) return;

        if (Input.GetKeyDown(qte))
        {
            CompleteQTE();
        }

        // Check if ANY other QTE key was pressed
        foreach (var key in possibleQtes)
        {
            if (key == currentQte) continue; // skip the correct one

            if (Input.GetKeyDown(key))
            {
                Debug.Log("Wrong key!");
                FailQTE();
                return;
            }
        }
    }
}
