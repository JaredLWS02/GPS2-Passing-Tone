using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamRot : MonoBehaviour
{
    [SerializeField] private CameraRotation camrot;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private PlayerMovement pm;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            camrot.enabled = true;
            GetComponent<BoxCollider>().enabled = false;
            source.clip = musicClip;
            source.Play();
        }
    }
}
