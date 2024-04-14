using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TriggerCredits : MonoBehaviour
{
    [SerializeField] private PlayerMovement frogMovement;
    [SerializeField] private CameraFollow followcam;
    [SerializeField] private UiTween tween;
    [SerializeField] private Animator princessFrog;
    [SerializeField] private Vector3 camDestination;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject pauseCanvas;

    // Update is called once per frame
    void Update()
    {
        if(princessFrog.GetCurrentAnimatorStateInfo(0).IsTag("Kiss")) 
        {
            frogMovement.enabled = false;
            pauseCanvas.SetActive(false);
            followcam.enabled = false;
            LeanTween.moveLocal(cameraObject, camDestination, 1.0f).setDelay(0.1f);
            if(princessFrog.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                startCredits();
            }
            //StartCoroutine(startCredits());
        }
    }

    private IEnumerator startCredits()
    {
        tween.BlackenScreenTransition(1.5f);
        yield return new WaitForSeconds(0.1f);
        tween.UnBlackenScreenTransition(0.5f);
    }

    private void blackenScreen()
    {

    }
}
