using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DragDrop : MonoBehaviour
{
    public GameObject objectToDrag;
    public GameObject ObjectDragToPosition;
    public float Dropdistance;
    public bool islocked = false;
    public TangramTrigger triggertangramthingy;

    public AudioSource pickUpSound;
    public AudioSource dropSound;

    private Vector3 objectInitPos;
    private Quaternion objectInitRot;
    private bool isRotate = true;


    // Start is called before the first frame update
    void Start()
    {
        objectInitPos = objectToDrag.transform.position;
        objectInitRot = objectToDrag.transform.rotation;
        Debug.Log("Initial Rotation: " + objectInitRot.eulerAngles);
    }


    public void RotateObject()
    {
        if (isRotate)
        {
            if (!islocked)
            {
                objectToDrag.transform.Rotate(Vector3.forward, 90f); // Rotate clockwise by 90 degrees
                                                                     // Play pick up sound
                if (pickUpSound != null)
                {
                    pickUpSound.Play();
                }
            }

        }

    }

    public void DragObject()
    {
        isRotate = false;
        if (!islocked)
        {

            objectToDrag.transform.position = Input.mousePosition;
            
        }
    }

    public void DropObject()
    {

        float Distance = Vector3.Distance(objectToDrag.transform.localPosition, ObjectDragToPosition.transform.localPosition);
        if (Distance < Dropdistance && IsCorrectRotation())
        {
            LockObject();
            triggertangramthingy.CheckPuzzleCompletion();

            // Play drop sound
            if (dropSound != null)
            {
                dropSound.Play();
            }
        }
        else
        {
            ResetObject();
        }
    }

    private bool IsCorrectRotation()
    {
        // Check if the object's rotation is close to the desired rotation (within a small tolerance)
        return Quaternion.Angle(objectToDrag.transform.rotation, ObjectDragToPosition.transform.rotation) <= 0f;
    }

    public void LockObject()
    {
        islocked = true;
        objectToDrag.transform.position = ObjectDragToPosition.transform.position;
        gameObject.GetComponent<Image>().raycastTarget = false;
        // Play pick up sound
        if (pickUpSound != null)
        {
            pickUpSound.Play();
        }


    }

    public void UnlockObject()
    {
        islocked = false;
    }

    private void ResetObject()
    {
        isRotate = true;
        objectToDrag.transform.position = objectInitPos;
        objectToDrag.transform.rotation = objectInitRot;
    }
}