using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUi;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject camUI;
    [SerializeField] private GameObject settingsUi;
    [SerializeField] private GameObject gameplayCamera;
    [SerializeField] private UiTween tween;

    [SerializeField] private Animator page1Anim;
    [SerializeField] private Animator page2Anim;
    [SerializeField] private AudioSource uiSound;

    [Header("Speed of Flipping Page (To Settings)")]
    [Range(0.0f, 5.0f)]
    [SerializeField] private float speedSettings;

    [Header("Speed of Flipping Page (To Play)")]
    [Range(0.0f, 5.0f)]
    [SerializeField] private float speedPlay;

    [Header("Speed for screen transitioning")]
    [Range(0.0f, 2.0f)]
    [SerializeField] private float BlackenscreenSpeed;
    [Range(0.0f, 2.0f)]
    [SerializeField] private float UnBlackenscreenSpeed;

    [Header("Things to enable for GamePlay")]
    [SerializeField] private List<GameObject> gameplayObjects = new List<GameObject>();

    [Header("Pages to be enable")]
    [SerializeField] private List<GameObject> pages = new List<GameObject>();

    [Header("Things to disable for GamePlay")]
    [SerializeField] private List<GameObject> disableObject = new List<GameObject>();


    private bool running;

    //private void Start()
    //{
    //    tween.UnBlackenScreenTransition();
    //}
    public void StartGame()
    {
        uiSound.Play();
        StartCoroutine(playGame());
    }

    public void ShowSettings()
    {
        uiSound.Play();
        StartCoroutine(Settings());
    }

    public void BackMainMenu()
    {
        uiSound.Play();
        StartCoroutine(Back());
    }
    public void Exitgame()
    {
        uiSound.Play();
        Application.Quit();
    }

    private IEnumerator playGame()
    {
        //turn off all main menu ui
        //camUI.SetActive(false);
        //gameplayCamera.SetActive(true);
        tween.TweenCam();
        mainMenuUi.SetActive(false);
        //GameEventManager.isTouchPage = true;

        foreach(var page in pages)
        {
            page.SetActive(true);
        }
        tween.BlackenScreenTransition(BlackenscreenSpeed);
        //play flip page animation
        StartCoroutine(StartGameflipPages(speedPlay));
        while(running)
        {
            yield return null;
        }
        tween.UnBlackenScreenTransition(UnBlackenscreenSpeed);
        page1Anim.Play("IClose",0);

        camUI.SetActive(false);
        gameplayCamera.SetActive(true);

        // turn on all gameplay related objects
        mainMenuCanvas.SetActive(false);
        //GameEventManager.isTouchPage = false;

        foreach (var item in gameplayObjects)
        {
            item.SetActive(true);
        }

        foreach(var item in disableObject)
        {
            item.SetActive(false);
        }
    }

    private IEnumerator Settings()
    {
        //turn off all main menu ui
        mainMenuUi.SetActive(false);
        tween.Settings();

        StartCoroutine(flipPage(speedSettings));
        while(running)
        {
            yield return null;
        }
        // turn on all settings related objects
        settingsUi.SetActive(true);
        tween.TweenSettings();
    }

    private IEnumerator Back()
    {
        // turn off settings Ui
        settingsUi.SetActive(false);
        tween.MainMenu();

        StartCoroutine(flipBackPage(speedSettings));
        while (running)
        {
            yield return null;
        }
        //turn off all main menu ui
        mainMenuUi.SetActive(true);
        tween.TweenMainMenu();
    }



    #region flipPageAnimation
    private IEnumerator flipPage(float speed)
    {
        running = true;
        page1Anim.Play("IFlip", 0, 0);
        page1Anim.SetFloat("speedMultiplier", speed);
        while (page1Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.999f)
        {
            yield return null;
        }
        page1Anim.SetFloat("speedMultiplier", 0);
        
        running = false;
    }

    private IEnumerator flipBackPage(float speed)
    {
        running = true;
        page1Anim.Play("IFlipBack", 0, 0);
        page1Anim.SetFloat("speedMultiplier", speed);
        while (page1Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.999f )
        {
            yield return null;
        }
        page1Anim.SetFloat("speedMultiplier", 0);

        running = false;
    }

    private IEnumerator StartGameflipPages(float speed)
    {
        running = true;
        page1Anim.Play("IFlip", 0, 0);
        page1Anim.SetFloat("speedMultiplier", speed);
        page2Anim.Play("IOpen", 0, 0);
        page2Anim.SetFloat("speedMultiplier", speed);
        while (page1Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.999f && page2Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.999f)
        {
            yield return null;
        }
        page1Anim.SetFloat("speedMultiplier", 0);
        page2Anim.SetFloat("speedMultiplier", 0);
        page2Anim.Play("IFlip", 0, 0);
        running = false;
    }
    #endregion

}
