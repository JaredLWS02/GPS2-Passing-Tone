using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotOnDrag3 : MonoBehaviour
{
    [SerializeField] private GameObject obj3;
    private float rotY, rotZ;
    private float rotx3;
    private float precision = 0.9999f;
    private bool rotAble = true;
    private float xVal = 0;
    private Quaternion targetAngle;

    private void Update()
    {

        if (rotAble == false)
        {
            obj3.transform.Rotate(rotx3, 0, 0);
            if (Mathf.Abs(Quaternion.Dot(obj3.transform.rotation, targetAngle)) > precision)
            {
                rotx3 = 0;
                rotAble = true;
            }
        }

    }
    //obj2
    public void rotatoD2()
    {
        if (rotAble == true)
        {
            xVal = xVal - 120;
            targetAngle = Quaternion.Euler(-1 + xVal, rotY, rotZ);
            rotx3 = -0.5f;
            rotAble = false;
        }
    }

    public void rotatoU2()
    {
        if (rotAble == true)
        {
            xVal = xVal + 120;
            targetAngle = Quaternion.Euler(1 + xVal, rotY, rotZ);
            rotx3 = 0.5f;
            rotAble = false;
        }
    }
}
