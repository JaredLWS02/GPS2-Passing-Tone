using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private UnityEvent swiped;
    [SerializeField] private bool left, right, up, down;

    //Position of where the player is touching
    private Ray ray;
    private RaycastHit hit;

    //Position of where the player is swiping to
    private Vector2 startPos, endPos, direction;
    private bool clicked;

    private GameObject gameObjectHit;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Puzzle")
                {
                    startPos = Input.GetTouch(0).position;
                    clicked = true;
                    GameEventManager.isTouchObject = true; 

                }
            }
        }
        if (clicked == true && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && Physics.Raycast(ray, out hit))
        {
            endPos = Input.touches[0].position;
            direction = startPos - endPos;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //For Left and Right detection
            {
                if (direction.x > 0 && hit.collider.GetComponent<SwipeDetection>().left == true)
                {
                    Debug.Log("Left");
                    swiped.Invoke();
                }
                else if (direction.x < 0 && hit.collider.GetComponent<SwipeDetection>().right == true)
                {
                    Debug.Log("Right");
                    swiped.Invoke();
                }

            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) //For Up and Down
            {
                if (direction.y < 0 && hit.collider.GetComponent<SwipeDetection>().up == true)
                {
                    Debug.Log("Up");
                    swiped.Invoke();
                }
                else if (direction.y > 0 && hit.collider.GetComponent<SwipeDetection>().down == true)
                {
                    Debug.Log("Down");
                    swiped.Invoke();
                }
            }
            clicked = false;
            GameEventManager.isTouchObject = false;
        }

        #region Mouse Input
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Puzzle")
                {
                    startPos = Input.mousePosition;
                    clicked = true;
                    GameEventManager.isTouchObject = true;

                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && Physics.Raycast(ray, out hit))
        {
            endPos = Input.mousePosition;
            direction = startPos - endPos;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //For Left and Right detection
            {
                if (direction.x > 0 && hit.collider.GetComponent<SwipeDetection>().left == true)
                {
                    Debug.Log("Left");
                    swiped.Invoke();
                }
                else if (direction.x < 0 && hit.collider.GetComponent<SwipeDetection>().right == true)
                {
                    Debug.Log("Right");
                    swiped.Invoke();
                }

            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) //For Up and Down
            {
                if (direction.y < 0 && hit.collider.GetComponent<SwipeDetection>().up == true)
                {
                    Debug.Log("Up");
                    swiped.Invoke();
                }
                else if (direction.y > 0 && hit.collider.GetComponent<SwipeDetection>().down == true)
                {
                    Debug.Log("Down");
                    swiped.Invoke();
                }
            }
            clicked = false;
            GameEventManager.isTouchObject = false;
        }
        #endregion
    }

    public void ColourChange()
    {
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                hit.collider.GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
    }
}
