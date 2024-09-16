using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraRotation : MonoBehaviour
{
    private Vector2 SwipeStartPos;
    private Vector2 SwipeEndPos;
    private bool isRotating = false;
    private bool startRot;
    private float startRotation;
    private float EndRotation;

    [SerializeField] private float MinSwipeDistance;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float RotationAngle;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private GameObject player;

    [Header("Assign objects other that player that you want to rotate")]
    [SerializeField] private List<GameObject> thingsToRotate = new List<GameObject>();
    [SerializeField] private GameObject guiderotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.eulerAngles.y;
        startRot = true;
        guiderotation.SetActive(true);
    }

    private void Update()
    {

    }
    private void LateUpdate()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        if (!GameEventManager.isTouchObject && !GameEventManager.isTouchPage && !GameEventManager.isPuzzling)
        {
            if (!isRotating && !pm.isMoving)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        startRotation = transform.eulerAngles.y;
                        SwipeStartPos = Input.GetTouch(0).position;
                        startRot = true;
                        pm.isRotate = false;
                    }

                    if (Input.touchCount < 2)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Ended && startRot)
                        {
                            SwipeEndPos = Input.GetTouch(0).position;
                            float swipedistance = SwipeEndPos.x - SwipeStartPos.x;

                            if (swipedistance > MinSwipeDistance || swipedistance < -MinSwipeDistance)
                            {
                                isRotating = true;

                                if (swipedistance > 0)
                                {
                                    StartCoroutine(RotateCamRight());
                                }
                                else
                                {
                                    StartCoroutine(RotateCamLeft());
                                }
                                guiderotation.SetActive(false);

                            }

                        }

                    }
                }
            }
        }
        else
        {
            startRot = false;
        }
    }

    private IEnumerator RotateCamRight()
    {
        pm.isRotate = true;
        EndRotation = startRotation + RotationAngle;
        while(isRotating)
        {
            transform.Rotate(0, (float)(RotationSpeed * Time.deltaTime), 0);
            player.transform.Rotate(0, (float)(RotationSpeed * Time.deltaTime), 0);

            if (transform.eulerAngles.y >= EndRotation - 10.0f)
            {
                if (EndRotation >= 360)
                {
                    startRotation = 0;
                    EndRotation = 0;
                }

                isRotating = false;
                pm.isRotate = false;

                transform.rotation = Quaternion.Euler(0, EndRotation, 0);
                player.transform.rotation = Quaternion.Euler(0, EndRotation, 0);

                foreach (GameObject trans in thingsToRotate)
                {
                    if(trans != null)
                    {
                        trans.transform.rotation = Quaternion.Euler(0, EndRotation, 0);
                    }
                }
            }
            yield return null;
        }
    }

    private IEnumerator RotateCamLeft()
    {
        pm.isRotate = true;
        if (startRotation == 0)
        {
            EndRotation = 360 + (-RotationAngle);
        }
        else
        {
            EndRotation = startRotation - RotationAngle;
        }

        while(isRotating)
        {
            transform.Rotate(0, (float)(-RotationSpeed * Time.deltaTime), 0);
            player.transform.Rotate(0, (float)(-RotationSpeed * Time.deltaTime), 0);
            if (transform.eulerAngles.y <= EndRotation + 10.0f)
            {
                if (EndRotation <= -360)
                {
                    startRotation = 0;
                    EndRotation = 0;
                }
                isRotating = false;
                pm.isRotate = false;

                transform.rotation = Quaternion.Euler(0, EndRotation, 0);
                player.transform.rotation = Quaternion.Euler(0, EndRotation, 0);

                foreach (GameObject trans in thingsToRotate)
                {
                    if (trans != null)
                    {
                        trans.transform.rotation = Quaternion.Euler(0, EndRotation, 0);
                    }
                }
            }
            yield return null;
        }

    }

}
