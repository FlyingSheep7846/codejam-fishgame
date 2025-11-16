using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private RectTransform pagesParent;
    public List<CanvasGroup> pages;
    [SerializeField] CanvasGroup cg;

    [SerializeField] CanvasGroup startInstruction;
    [SerializeField] CanvasGroup buttons;

    private bool startVisible = false; 

    private bool Debounce = false;

    private int currentSlide = 0;

    public static bool TUTORIAL_PLAYED = false;

    private PlayerController playerController;
    
    void Awake()
    {
        pages = pagesParent.GetComponentsInChildren<CanvasGroup>().ToList();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (TUTORIAL_PLAYED) StartGame();
    }

    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        pages[0].DOFade(1f, 0.5f);
        buttons.DOFade(1f, 1f);
    }

    public void NextPage(int direction)
    {
        if (Debounce) return;
        Debounce = true;

        int newSlide = Mathf.Clamp(currentSlide + direction, 0, pages.Count - 1);
        if (newSlide == currentSlide) return;

        pages[currentSlide].DOFade(0f, 0.5f).OnComplete(
            () =>
            {
                pages[newSlide].DOFade(1f, 0.5f).OnComplete(
                    () =>
                    {
                        Debounce = false;
                        currentSlide = newSlide;
                        CheckIfLastSlide();
                    }
                );
            }
        );
    }

    private void CheckIfLastSlide()
    {
        if (startVisible) return;

        if (currentSlide == pages.Count - 1)
        {
            startInstruction.DOFade(1f, 1f);
            startVisible = true;
            this.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // start game
            StartGame();
            cg.DOFade(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
        }
    }

    private void StartGame()
    {
        TUTORIAL_PLAYED = true;
        Timer.INSTANCE.running = true;
        playerController.ToggleFreeLook(true, 0);
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
