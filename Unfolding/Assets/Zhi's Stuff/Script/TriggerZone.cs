using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriggerZone : MonoBehaviour
{
    public GameObject puzzle;
    public NavMeshAgent player;

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            puzzle.SetActive(true);
            player.ResetPath();
        }
    }
}
