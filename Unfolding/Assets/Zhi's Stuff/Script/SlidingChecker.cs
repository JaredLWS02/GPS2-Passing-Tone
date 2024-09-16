using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingChecker : MonoBehaviour
{
    [SerializeField] private SlidingPuzzleHandler puzzle;
    [SerializeField] private GameObject fix;
    [SerializeField] private GameObject broken;
    void Update()
    {
        if (puzzle.win == true)
        {
            fix.SetActive(true);
            broken.SetActive(false);
        }
        else
        {
            broken.SetActive(true);
            fix.SetActive(false);
        }
    }
}
