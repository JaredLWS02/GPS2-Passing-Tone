using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowDrag : MonoBehaviour
{
    [SerializeField] private GameObject sensor;
    private SpriteRenderer sr;
    public Behaviour rotS;

    private void Awake()
    {
        sr = sensor.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Entered");
            rotS.enabled = false;
            sr.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Left");
            rotS.enabled = true;
            sr.color = Color.green;
        }
    }
}
