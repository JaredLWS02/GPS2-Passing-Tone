using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class rotOnDrag : MonoBehaviour
{
    [SerializeField] private GameObject obj1;
    [SerializeField] private float rotationXSpeed;
    [SerializeField] private NavMeshSurface mesh;
    [SerializeField] private NavMeshGenerator NavMeshGen;
    public float rotY, rotZ; 
    private float rotx;
    private float precision = 0.9999f;
    private bool rotAble = true;
    private float xVal = 0;
    private Quaternion targetAngle;
    [SerializeField] private Behaviour rotS;
    [SerializeField] private AudioSource puzzleSound;

    private void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        if (rotAble == false)
        {
            obj1.transform.Rotate(rotx, 0, 0);
            if (Mathf.Abs(Quaternion.Dot(obj1.transform.rotation, targetAngle)) > precision)
            {
                mesh.enabled = true;
                NavMeshGen.enabled = true;
                mesh.UpdateNavMesh(mesh.navMeshData);
                rotx = 0;
                rotAble = true;
                //checker.checkCorrectRot();
            }
        }
    }
    //obj 1
    public void rotatoD()
    {
        Debug.Log("Did rotate");
        if (rotAble == true)
        {
            NavMeshGen.enabled = false;
            mesh.enabled = false;
            xVal = xVal - 120;
            targetAngle = Quaternion.Euler(-1 + xVal, rotY, rotZ);
            rotx = -rotationXSpeed;
            rotAble = false;
            if(puzzleSound != null)
            {
                puzzleSound.Play();
            }
        }
    }

    public void rotatoU()
    {
        if (rotAble == true)
        {
            NavMeshGen.enabled = false;
            mesh.enabled = false;
            xVal = xVal + 120;
            targetAngle = Quaternion.Euler(1 + xVal, rotY, rotZ);
            rotx = rotationXSpeed;
            rotAble = false;
            if (puzzleSound != null)
            {
                puzzleSound.Play();
            }

        }
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Entered");
    //        obj1.GetComponent<SwipeDetectionTestNew>().enabled = false;
    //        rotS.enabled = false;
    //    }
    //}

    //private void OnTriggerExit(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Left");
    //        rotS.enabled = true;
    //    }
    //}
}
