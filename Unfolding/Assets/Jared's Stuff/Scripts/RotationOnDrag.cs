using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnDrag : MonoBehaviour
{
    [SerializeField] private GameObject obj1, obj2, obj3;
    private float rotx1, rotx2, rotx3;
    float precision = 0.9999f;
    bool rotatable = true;
    bool rotN1, rotN2, rotN3 = true;
    float xVal1, xVal2, xVal3 = 0;
    Quaternion targetAngle;

    void Update()
    {
        if (rotatable == false && rotN1 == false)
        {
            obj1.transform.Rotate(rotx1, 0, 0);
            if (Mathf.Abs(Quaternion.Dot(obj1.transform.rotation, targetAngle)) > precision)
            {
                rotx1 = 0;
                rotatable = true;
                rotN1 = true;
            }
        }
        
        if (rotatable == false && rotN2 == false)
        {
            obj2.transform.Rotate(rotx2, 0, 0);
            if (Mathf.Abs(Quaternion.Dot(obj2.transform.rotation, targetAngle)) > precision)
            {
                rotx2 = 0;
                rotatable = true;
                rotN2 = true;
            }
        }
        
        if (rotatable == false && rotN3 == false)
        {
            obj3.transform.Rotate(rotx3, 0, 0);
            if (Mathf.Abs(Quaternion.Dot(obj3.transform.rotation, targetAngle)) > precision)
            {
                rotx3 = 0;
                rotatable = true;
                rotN3 = true;
            }
        }
    }

    public void rotateD1()
    {
        if (rotatable == true)
        {
            xVal1 = xVal1 - 120;
            targetAngle = Quaternion.Euler(-1 + xVal1, 0, 0);
            rotN1 = false;
            rotx1 = -0.5f;
            rotatable = false;
        }
    }

    public void rotateD2()
    {
        if (rotatable == true)
        {
            xVal2 = xVal2 - 120;
            targetAngle = Quaternion.Euler(-1 + xVal2, 0, 0);
            rotN2 = false;
            rotx2 = -0.5f;
            rotatable = false;
        }
    }

    public void rotateD3()
    {
        if (rotatable == true)
        {
            xVal3 = xVal3 - 120;
            targetAngle = Quaternion.Euler(-1 + xVal3, 0, 0);
            rotN3 = false;
            rotx3 = -0.5f;
            rotatable = false;
        }
    }

    public void rotateU1()
    {
        if (rotatable == true)
        {
            xVal1 = xVal1 + 120;
            targetAngle = Quaternion.Euler(1 + xVal1, 0, 0);
            rotN1 = false;
            rotx1 = 0.5f;
            rotatable = false;
        }
    }

    public void rotateU2()
    {
        if (rotatable == true)
        {
            xVal2 = xVal2 + 120;
            targetAngle = Quaternion.Euler(1 + xVal2, 0, 0);
            rotN2 = false;
            rotx2 = 0.5f;
            rotatable = false;
        }
    }

    public void rotateU3()
    {
        if (rotatable == true)
        {
            xVal3 = xVal3 + 120;
            targetAngle = Quaternion.Euler(1 + xVal3, 0, 0);
            rotN3 = false;
            rotx3 = 0.5f;
            rotatable = false;
        }
    }
}
