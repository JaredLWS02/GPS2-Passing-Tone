using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotOnDrag2 : MonoBehaviour
{
    [SerializeField] private GameObject obj2;
    private float rotY, rotZ;
    private float rotx2;
    private float precision = 0.9999f;
    private bool rotAble = true;
    private float xVal = 0;
    private Quaternion targetAngle;

    private void Update()
    {
        if (rotAble == false)
        {
            obj2.transform.Rotate(rotx2, 0, 0);
            if (Mathf.Abs(Quaternion.Dot(obj2.transform.rotation, targetAngle)) > precision)
            {
                rotx2 = 0;
                rotAble = true;
            }
        }
    }
    //obj3
    public void rotatoD1()
    {
        if (rotAble == true)
        {
            xVal = xVal - 120;
            targetAngle = Quaternion.Euler(-1 + xVal, rotY, rotZ);
            rotx2 = -0.5f;
            rotAble = false;
        }
    }

    public void rotatoU1()
    {
        if (rotAble == true)
        {
            xVal = xVal + 120;
            targetAngle = Quaternion.Euler(1 + xVal, rotY, rotZ);
            rotx2 = 0.5f;
            rotAble = false;
        }
    }
}
