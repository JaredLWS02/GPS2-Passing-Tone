using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTween : MonoBehaviour
{
    [Header("Cam Ui")]
    [SerializeField] private  GameObject cam;
    [SerializeField] private Vector3 endpos;
    [SerializeField] private float camspeed;
    private Vector3 startpos;

    [Header("PlayCam Ui")]
    [SerializeField] private Vector3 endposition;
    [SerializeField] private float speed;


    [Header("Main Menu Ui")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject OptionButton;
    [SerializeField] private GameObject QuitButton;
    [SerializeField] private LeanTweenType MainMenutype;
    [SerializeField] private float buttonSpeed;
    private Vector3 playPos;
    private Vector3 OptionPos;
    private Vector3 QuitPos;

    [Header("Settings Ui")]
    [SerializeField] private GameObject BackButton;
    [SerializeField] private GameObject MusicSlider;
    [SerializeField] private GameObject SfxSlider;
    [SerializeField] private LeanTweenType Settingstype;
    [SerializeField] private float optionSpeed;
    private float backPos;

    [Header("Sreen Transition Ui")]
    [SerializeField] private  GameObject canvasScreenTransistion;
    [SerializeField] private Image panel;
    [SerializeField] private float blackenSpeed;

    public bool playUI;

    // Start is called before the first frame update
    void Start()
    {
        UnBlackenScreenTransition();
        if(playUI)
        {
            setObjectPos();
        }
    }


    #region screenTransition

    public void UnBlackenScreenTransition()
    {
        panel.CrossFadeAlpha(0, blackenSpeed, false);
    }
    public void UnBlackenScreenTransition(float speed)
    {
        panel.CrossFadeAlpha(0, speed, false);
    }
    public void BlackenScreenTransition()
    {
        panel.CrossFadeAlpha(1, blackenSpeed, false);
    }

    public void BlackenScreenTransition(float speed)
    {
        panel.CrossFadeAlpha(1, speed, false);
    }

    #endregion

    public void setObjectPos()
    {
        startpos = cam.transform.position;
        playPos = playButton.transform.position;
        OptionPos = OptionButton.transform.position;
        QuitPos = QuitButton.transform.position;
        backPos = BackButton.transform.position.z;

        TweenMainMenu();
    }

    public void Settings()
    {
        LeanTween.moveLocalX(cam,endpos.x, camspeed);
    }

    public void MainMenu()
    {
        LeanTween.moveLocalX(cam, startpos.x, camspeed);
    }



    #region PlayTween

    public void TweenCam()
    {
        LeanTween.move(cam, endposition, speed);
        cam.GetComponent<Camera>().fieldOfView = 60;
        LeanTween.rotateX(cam, 14.86f, speed);
    }
    #endregion


    #region mainMenuTween

    public void TweenMainMenu()
    {
        if(playButton.transform.position != playPos || OptionButton.transform.position != OptionPos || QuitButton.transform.position != QuitPos)
        {
            playButton.transform.position = playPos;
            OptionButton.transform.position = OptionPos;
            QuitButton.transform.position = QuitPos;
        }

        //if(!playButton.activeSelf && !OptionButton.activeSelf && !QuitButton.activeSelf)
        //{
        //    playButton.SetActive(true);
        //    OptionButton.SetActive(true);
        //    QuitButton.SetActive(true);
        //}

        LeanTween.moveLocalX(playButton, 249, buttonSpeed).setEase(MainMenutype).setOnComplete(moveOption);
    }

    private void moveOption()
    {
        LeanTween.moveLocalX(OptionButton, 262, buttonSpeed).setEase(MainMenutype).setOnComplete(moveQuit);
    }

    private void moveQuit()
    {
        LeanTween.moveLocalX(QuitButton, 241, buttonSpeed).setEase(MainMenutype);

    }

    #endregion


    #region settingsTween
    public void TweenSettings()
    {
        BackButton.transform.position = new Vector3 (BackButton.transform.position.x, -1.2f, backPos);
        MusicSlider.transform.localScale = Vector3.zero;
        SfxSlider.transform.localScale = Vector3.zero;

        LeanTween.moveLocalZ(BackButton, 10.3f, optionSpeed).setEase(MainMenutype);
        LeanTween.scale(MusicSlider, Vector3.one, optionSpeed).setEase(Settingstype);
        LeanTween.scale(SfxSlider, Vector3.one, optionSpeed).setDelay(buttonSpeed /2).setEase(Settingstype);
    }

    #endregion
}
