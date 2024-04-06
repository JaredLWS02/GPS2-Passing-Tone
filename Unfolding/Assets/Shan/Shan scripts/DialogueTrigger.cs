using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Camera mainCamera;
    public Camera npcCamera;
    public GameObject playerObject;
    public Collider playerCollider; // Collider to disable player detection
    public Transform respawnPoint; // New spawn point for the player
    public Transform dialoguePlayerPosition; // New position for the player during dialogue


    private bool playerDetected;
    private bool dialogueInProgress;
    private NavMeshAgent playerNavMeshAgent; // Reference to the player's NavMeshAgent
    public GameObject tangramPuzzle;
    //private Vector3 originalPlayerPosition; // To store the original position of the player

    private void Start()
    {
        // Get reference to the NavMeshAgent component
        playerNavMeshAgent = playerObject.GetComponent<NavMeshAgent>();
    }
    //Detect trigger with player
    private void OnTriggerEnter(Collider collision)
    {
        //if we triggered the player enable player detected and show indicator
        if (collision.tag == "Player" && !dialogueInProgress)
        {
            Debug.Log("Player entered trigger zone.");
            StartDialogue();
        }
    }
    /*private void OnTriggerEnter(Collider collision)
    {
        //if we triggered the player enable player detected and shhow indicator
        if (collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
            // Activate NPC camera
            npcCamera.gameObject.SetActive(true);
            // Deactivate main camera
            mainCamera.gameObject.SetActive(false);
            // Start dialogue immediately
            dialogueScript.StartDialogue();

            // Store the original position of the player
            //originalPlayerPosition = playerObject.transform.position;

            // Position the player at the dialoguePlayerPosition
            playerObject.transform.position = dialoguePlayerPosition.position;

            // Disable player object and its collider
            //playerObject.SetActive(false);
            playerCollider.enabled = false;
        }
    }*/

    public void StartDialogue()
    {
        if (!dialogueInProgress)
        {
            playerDetected = true;
            dialogueInProgress = true;
            dialogueScript.ToggleIndicator(playerDetected);
            // Activate NPC camera
            npcCamera.gameObject.SetActive(true);
            // Deactivate main camera
            mainCamera.gameObject.SetActive(false);
            // Start dialogue immediately
            dialogueScript.StartDialogue();

            Debug.Log("Player position before dialogue: " + playerObject.transform.position);

            // Disable NavMeshAgent
            playerNavMeshAgent.enabled = false;

            // Position the player at the dialoguePlayerPosition
            playerObject.transform.position = dialoguePlayerPosition.position;

            // Disable player collider
            playerCollider.enabled = false;
        }
    }
    private void EndDialogue()
    {
        dialogueInProgress = false;
        enablePlayer();
    }

    private void OnTriggerExit(Collider collision)
    {
        //if we lost trigger with the player disable player detected and hide indicator
        if (collision.tag == "Player" && !dialogueInProgress)
        {
            EndDialogue();
        }
    }

    /*private void OnTriggerExit(Collider collision)
    {
        //if we lost trigger with the player disable player detected and hide indicator
        if (collision.tag == "Player")
        {
            enablePlayer();
        }
    }*/

    public void enablePlayer()
    {
        if(tangramPuzzle != null)
        {
            tangramPuzzle.SetActive(true);
        }
        playerDetected = false;
        dialogueScript.ToggleIndicator(playerDetected);
        //dialogueScript.EndDialogue();
        // Activate main camera
        mainCamera.gameObject.SetActive(true);
        // Deactivate NPC camera
        npcCamera.gameObject.SetActive(false);

        // Re-enable NavMeshAgent
        playerNavMeshAgent.enabled = true;

        // Re-enable player object and its collider
        playerObject.SetActive(true);
        playerCollider.enabled = true;

        // Respawn player at the new spawn point
        playerObject.transform.position = respawnPoint.position;

        Debug.Log("Player respawned at respawn point: " + playerObject.transform.position);
    }
    //While detected if we interact start the dialogue
    /*private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogue();
        }
    }*/

}
