using System.Collections;
using DG.Tweening;
using TMPro;
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
    public float QteInterval;
    public float failedDecreaseAmount = 0.15f;

    KeyCode[] possibleQtes = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D};
    private KeyCode currentQte = KeyCode.None;

    public FishingRodController fishingRodController;
    public TextMeshProUGUI text;

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

    public void InitializeQTE()
    {
        isInQte = false;
        qteIntervalTimer = 5f;

        this.enabled = true;
    }

    void StartQTE()
    {
        isInQte = true;
        timer = QteTime;
        currentQte = possibleQtes[Random.Range(0, 4)];

        text.text = currentQte.ToString();

        qteCg.alpha = 1f;
        slider.value = 1f;
    }

    void CompleteQTE()
    {
        isInQte = false;
        qteIntervalTimer = QteInterval;
        qteCg.DOFade(0f,0.5f);
    }

    void FailQTE()
    {
        isInQte = false;
        qteIntervalTimer = QteInterval;
        fishingRodController.DecreaseProgressBar(failedDecreaseAmount);
        qteCg.DOFade(0f,0.5f);
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
