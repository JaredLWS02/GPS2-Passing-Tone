using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float limit;

    [SerializeField] public bool reSize;
    private bool doOnce;
    private bool pageflip;
    void Start()
    {
        //offset = transform.position - player.transform.position;

    }

    void LateUpdate()
    {
        if(gameObject.transform.GetChild(0).gameObject.activeSelf)
        {

            if (GameEventManager.isTouchPage == false)
            {
                //doOnce = false;
                Vector3 desiredPosition = player.transform.position + offset;
                if(reSize)
                {
                    GetComponentInChildren<Camera>().fieldOfView = 52;
                }
                else
                {
                    GetComponentInChildren<Camera>().fieldOfView = 34;
                }
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                //transform.position = smoothedPosition;
                //if (pageflip)
                //{
                    //transform.position = new Vector3(-16.2f, 0.0f, 0.0f);
                    //moveCam(pos);
                //}
                //else
                //{
                    //if(!tweening)
                    //{
                        transform.position = new Vector3(Mathf.Clamp(smoothedPosition.x, -16.2f, limit), smoothedPosition.y, 0);
                    //}

                //}
            }
            else
            {
                //if(!doOnce)
                //{
                //    startpos = player.transform.position;
                //    doOnce = true;
                //}
                Vector3 desiredPosition = new Vector3(2.13f, 7.7f, -10.55f);
                GetComponentInChildren<Camera>().fieldOfView = 60;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                //transform.position = smoothedPosition;
                //transform.position = desiredPosition;
                moveCam(smoothedPosition, 0.05f);
                //pageflip = true;
            }

        }
    }

    private void moveCam(Vector3 target, float time)
    {
        LeanTween.moveLocal(gameObject, target, time);

    }

}
