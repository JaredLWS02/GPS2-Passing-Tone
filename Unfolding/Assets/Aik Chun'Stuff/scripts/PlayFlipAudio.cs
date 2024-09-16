using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFlipAudio : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    public void playAudio(AudioClip clip)
    {
        sound.clip = clip;
        sound.Play();

    }

}
