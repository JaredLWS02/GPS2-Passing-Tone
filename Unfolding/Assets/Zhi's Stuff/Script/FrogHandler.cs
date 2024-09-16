using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHandler : MonoBehaviour
{
    private Animator animator;
    private bool doOnce;

    private void Awake()
    {
        doOnce = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameEventManager.isTouchPage == true && doOnce == false)
        {
            animator.Play("Frog_Squish");
            doOnce = true;
        }
        else if (GameEventManager.isTouchPage == false && PageFlip.flipped == true && doOnce == true)
        {
            animator.Play("Frog_Up");
            doOnce= false;
        }
    }

    //public void FrogUp()
    //{
    //    if (doOnce == true)
    //    {
    //        animator.Play("Frog_Up");
    //        doOnce = false;
    //    }
    //}
}
