using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    private Animator animator;

    [Header("Text References")]
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI subtractText;
    [SerializeField] TextMeshProUGUI descriptionText;

    private CanvasGroup amountCg;
    private CanvasGroup subtractCg;

    private string SUCCESS_TEXT = "THE MONSTER WAS SATISFIED TODAY.";
    private string FAILED_TEXT = "THE MONSTER IS STILL HUNGRY...";

    public AudioClip seagull;
    public AudioClip monsterGurgle;
    public AudioClip monsterMunch;

    private int fishHeld;
    private int fishNeeded;
 


    void Awake()
    {
        animator = GetComponent<Animator>();

        amountCg = amountText.GetComponent<CanvasGroup>();
        subtractCg = subtractText.GetComponent<CanvasGroup>();
    }

    private void SetFishValues()
    {
        fishHeld = FishManager.INSTANCE.getFishCount();
        fishNeeded = DayManager.INSTANCE.fishNeeded[DayManager.INSTANCE.currentDay];
    }

    public void PlayCutscene()
    {
        SetFishValues();
        animator.SetTrigger("Animate");

        amountText.text = FishManager.INSTANCE.getFishCount().ToString();
        subtractText.text = (-DayManager.INSTANCE.fishNeeded[DayManager.INSTANCE.currentDay]).ToString();
        descriptionText.text = "";

        amountCg.alpha = 0f;
        subtractCg.alpha = 0f;
    }

    public void ShowText()
    {
        StartCoroutine("ShowTextProcess");   
    }

    IEnumerator ShowTextProcess()
    {
        bool success = fishHeld - fishNeeded >= 0;

        amountCg.DOFade(1f, 0.7f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.7f);

        subtractCg.DOFade(1f, 0.7f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.7f);

        StartCoroutine(TypewriterSubtract(subtractText, fishHeld, fishHeld - fishNeeded, 1f));
        yield return StartCoroutine(TypewriterSubtract(subtractText, -fishNeeded, 0, 1f));


        yield return new WaitForSecondsRealtime(0.4f);

        yield return StartCoroutine(TypewriterProcess(descriptionText, 
            success ? SUCCESS_TEXT : FAILED_TEXT, 1f));
    }

    IEnumerator TypewriterProcess(TextMeshProUGUI tmp, string text, float duration)
    {
        tmp.text = "";
        float wait = duration / text.Length;
    
        for (int i = 0; i < text.Length; i++)
        {
            tmp.text += text[i];
            yield return new WaitForSecondsRealtime(wait);
        }
    }

    IEnumerator TypewriterSubtract(TextMeshProUGUI tmp, int startValue, int endValue, float duration)
    {
        float elapsed = 0f;

        // Prevent divide-by-zero
        duration = Mathf.Max(0.0001f, duration);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // t goes 0 → 1
            float t = elapsed / duration;

            // Lerp from startValue down to endValue
            int current = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, t));

            tmp.text = current.ToString();
            yield return null;
        }

        // Ensure exact end value
        tmp.text = endValue.ToString();
    }

    public void ShowTextDone()
    {
        UIOverlays.INSTANCE.TransitionDay();
    }

    public void PlaySeagull()
    {
        SoundManager.Instance.PlayClip(seagull, .25f);
    }

    public void PlayGurgle()
    {
        SoundManager.Instance.PlayClip(monsterGurgle, .25f);
    }

    public async void Failed()
    {
        SetFishValues();
        if (fishHeld < fishNeeded)
        {
            SoundManager.Instance.PlayClip(monsterMunch, .75f);
            await Task.Delay(2000);
            SceneManager.LoadScene("Death");
        }
    }

}
