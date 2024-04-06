using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TangramTrigger : MonoBehaviour
{
    public Canvas tangramCanvas;
    public List<GameObject> tangramPieces;
    public Transform respawnPoint;
    public GameObject cutsceenTrigger;
    public NavMeshAgent player;
    public Mesh fixedboat;
    public GameObject boat;


    private bool puzzleComplete = false;
    private bool canvasOpened = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !puzzleComplete && !canvasOpened)
        {
            // Activate the tangram canvas
            tangramCanvas.gameObject.SetActive(true);
            canvasOpened = true;
            GameEventManager.isPuzzling = true;

            // Disable this trigger object
            gameObject.SetActive(false);
            player.ResetPath();

            
        }
    }
    public void  CheckPuzzleCompletion()
    {
        foreach (GameObject piece in tangramPieces)
        {
            if (!piece.GetComponent<DragDrop>().islocked)
            {
                Debug.Log("Piece is not locked: " + piece.name);
                return; // Exit function if any piece is not locked
            }
        }
        // If all pieces are locked, mark puzzle as complete
        Debug.Log("Puzzle complete!");
        SetPuzzleComplete(true);
    }

  

    public void SetPuzzleComplete(bool complete)
    {
        puzzleComplete = complete;

        // If puzzle is complete, deactivate the canvas and disable the object
        if (complete)
        {
            GameEventManager.isPuzzling = false;
            tangramCanvas.gameObject.SetActive(false);
            cutsceenTrigger.SetActive(true);
            boat.GetComponent<MeshFilter>().mesh = fixedboat;
            Debug.Log("Tangram canvas disabled.");
        }
    
    }

    private void RespawnPlayer()
    {
        // Move the player to the respawn point
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
        }
    }
}
