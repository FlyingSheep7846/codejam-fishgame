using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayCutscene()
    {
        animator.SetTrigger("Animate");
    }

    public void ShowText()
    {
        // GameManager.INSTANCE
    }
}
