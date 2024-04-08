using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class triggerReSize : MonoBehaviour
{
    [SerializeField] private GameObject frog;
    [SerializeField] private Vector3 frogscale;
    [SerializeField] private Camera mainGamePlayCam;
    [SerializeField] private CameraFollow camfollow;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            camfollow.reSize = true;
            GetComponent<BoxCollider>().enabled = false;
            frog.transform.localScale = frogscale;

        }
    }

}
