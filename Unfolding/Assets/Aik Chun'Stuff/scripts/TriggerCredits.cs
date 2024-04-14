using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TriggerCredits : MonoBehaviour
{
    [SerializeField] private PlayerMovement frogMovement;
    [SerializeField] private CameraFollow followcam;
    [SerializeField] private GameObject gameplayCam;
    [SerializeField] private GameObject frog;
    [SerializeField] private UiTween tween;
    [SerializeField] private Animator princessFrog;
    [SerializeField] private Vector3 camDestination;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject Book;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip clip;

    [Header ("GameObjects for credit scene")]
    [SerializeField] private GameObject CreditObject;

    // Update is called once per frame
    void Update()
    {
        if(princessFrog.GetCurrentAnimatorStateInfo(0).IsTag("Kiss")) 
        {
            frog.transform.rotation = Quaternion.Euler(0, 0, 0);
            frogMovement.enabled = false;
            pauseCanvas.SetActive(false);
            followcam.enabled = false;
            LeanTween.moveLocal(cameraObject, camDestination, 1.0f).setDelay(0.1f);
            if(princessFrog.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                StartCoroutine(startCredits());
            }
            //StartCoroutine(startCredits());
        }
    }

    private IEnumerator startCredits()
    {
        tween.BlackenScreenTransition(1.5f);
        yield return new WaitForSecondsRealtime(1.6f);
        gameplayCam.SetActive(false);
        tween.UnBlackenScreenTransition(0.5f);
        CreditObject.SetActive(true);
        tween.TweenCredit();

        musicSource.clip = clip;
        musicSource.Play();
        Book.SetActive(false);
    }
}
