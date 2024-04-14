using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PageFlip : MonoBehaviour
{
    [SerializeField] private GameObject MainGamePlayCamera;
    [SerializeField] private TextMeshProUGUI DebuggingText;
    [SerializeField] private float sensitivity = 5000;
    [SerializeField] private Animator nextPageAnimator;
    [SerializeField] private Animator prevPageAnimator;
    [SerializeField] private AudioSource pageflipSource;

    public static bool clicked;
    public static bool flipped;

    private Ray ray;
    private RaycastHit hit;

    private Vector2 startPos, pos;
    private float frame;
    private Animator anim;

    public bool canPlayAudio = false;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        frame = 0;
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        if (!GameEventManager.isPuzzling && MainGamePlayCamera.transform.localEulerAngles.y == 0 )
        {
            if (DebuggingText != null)
                DebuggingText.text = GameEventManager.selectedPage;
            GetTouch();
            if (hit.collider != null)
            {
                Flipping();
            }
        }
    }

    private void GetTouch()
    {
        //if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.touches[0].position != null)
                ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Input.mousePosition != null && Input.touches[0].position == null)
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider && hit.collider.tag == "Edge")
                {
                    GameEventManager.selectedPage = hit.collider.name;
                    clicked = true;
                    startPos = Input.touches[0].position;
                    GameEventManager.isTouchPage = true;

                }
            }
        }
        //else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        else if (Input.GetMouseButtonUp(0))
        {
            GameEventManager.isTouchPage = false;
            clicked = false;
        }
            if ((frame >= 0.001 && frame <= 0.999) && !flipped && !clicked)
            {
                pageflipSource.Play();
            }

    }
    private void Flipping()
    {
        Touch touch;

        #region Flipping Base

        //if (clicked && Input.touchCount> 0 && hit.collider.name == GameEventManager.selectedPage)
        if (Input.touchCount > 0 && clicked && hit.collider.name == GameEventManager.selectedPage)
        {
            touch = Input.GetTouch(0);
            if (hit.collider.gameObject.name == this.name)
            {
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    pos = touch.position - startPos;
                    frame += (-pos.x) / sensitivity;

                    anim.Play("IFlip", 0, frame);
                    if (nextPageAnimator != null)
                        nextPageAnimator.Play("IOpen", 0, frame);
                    if (prevPageAnimator != null)
                        prevPageAnimator.Play("IClose", 0, frame);

                }
            }
        }
        else
        {
            if (frame > 0.5)
            {
                frame += 0.01f;
            }
            else if (frame < 0.5)
            {
                frame -= 0.01f;
            }
            //if (hit.collider.name == GameEventManager.selectedPage)
            //{
                anim.Play("IFlip", 0, frame);

                if (nextPageAnimator != null)
                    nextPageAnimator.Play("IOpen", 0, frame);
                if (prevPageAnimator != null)
                    prevPageAnimator.Play("IClose", 0, frame);
            //}

            clicked = false;
        }
        #endregion


        #region Limit Flip
        if (frame >= 0.999 || frame <= 0.001)
        {
            flipped = true;
        }
        else
        {
            flipped = false;
        }

        if (frame >= 1)
        {
            frame = 1;
        }
        else if (frame <= 0)
        {
            frame = 0;
        }

        #endregion
    }


    // BUGS I FOUND
    /* - When flipping the other way, it will instantly snap to the end, probably due to how it is coded in positional based, rather than like an additive type of way. I'm not sure why it won't let me but
     * I'll just have make do with this for now.
     *  - Loading and deloading assets in. I have to find a way to instance stuff so that that pages can be renewed on the other side
     *   - Yes, I know, the mouse doesn't work, bohoo.
     */
}
