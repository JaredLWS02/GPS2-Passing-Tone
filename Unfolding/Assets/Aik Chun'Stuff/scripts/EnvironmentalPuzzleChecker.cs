using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnvironmentalPuzzleChecker : MonoBehaviour
{
    [SerializeField] private List <GameObject> puzzleBlock;
    [Header("Correct Rotations for the Puzzles following order")]
    [SerializeField] private List<float> rotValue;
    [SerializeField] private NavMeshAgent player;
    private float correctRot;
    private bool isCorrect;
    // Start is called before the first frame update
    void Start()
    {
        correctRot = 0;
        isCorrect = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.areaMask);
        if (correctRot >= puzzleBlock.Count && !isCorrect)
        {
            player.areaMask = 9; // have to manually check for this number.
            isCorrect = true;
        }
    }

    public void checkCorrectRot()
    {
        if(!isCorrect)
        { 
            int i = 0;
            foreach (var rot in puzzleBlock)
            {
                if (rot.transform.rotation.x == rotValue[i])
                {
                    correctRot++;
                }
                i++;
            }
        }
        //else
        //{
        //    int i = 0;
        //    foreach (var rot in puzzleBlock)
        //    {
        //        if (rot.transform.eulerAngles[i] != rotValue[i])
        //        {
        //            correctRot--;
        //        }
        //        i++;
        //    }
        //}
    }
}
