
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    //window
    public GameObject window;
    //indicator
    public GameObject indicator;
    //text
    public TMP_Text dialogueText;
    //dialogue lists
    public List<string> dialogues;
    //writing speed
    public float writingSpeed;
    // Sound effect
    public AudioClip dialogueSound;
    // Sound effect for dialogue trigger
    public AudioClip triggerSound;
    

    //Index on dialogue
    private int index;
    //character index
    private int charIndex;
    //Started boolean
    private bool started;
    //wait for next boolean
    private bool waitForNext;
    



    [SerializeField] private DialogueTrigger tri;
    private void Awake()
    {
        
        ToggleWindow(false);
        ToggleIndicator(false);
        
    }

    private void ToggleWindow(bool show) //
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show) //
    {
        indicator.SetActive(show);
    }


    public void StartDialogue()//
    {
        if (started)
        {
            return;
        }

        started = true;//Boolean to indicate that we have started

        ToggleWindow(true); //Show the window

        ToggleIndicator(false); //hide the indicator

        // Play the dialogue trigger sound
        if (triggerSound != null)
        {
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
        }


        index = 0; // Start with the first dialogue
        charIndex = 0; // Reset character index
        dialogueText.text = string.Empty; // Clear dialogue text

        //GetDialogue(0); //Start with first dialogue

        StartCoroutine(Writing());//Start writing
    }


    private void GetDialogue(int i) //
    {
        index = i; //start index at zero
        charIndex = 0; //Reset the character index           
        StartCoroutine(Writing()); //Start writing
        dialogueText.text = string.Empty; //clear the dialogue component text

    }

    public void EndDialogue() //
    {
        //started is disable
        started = false;
        //disable wait for next
        waitForNext = false;
        //Hide the window
        ToggleWindow(false);
        //Stop all Ienumerators
        StopAllCoroutines();

        //ToggleIndicator(false);
        tri.enablePlayer();
    }




    IEnumerator Writing()//writing logic  //
    {
        yield return new WaitForSecondsRealtime(writingSpeed);

        string currentDialogue = dialogues[index];
        dialogueText.text += currentDialogue[charIndex]; //Write the character
        charIndex++; //increase the character index 

        if (dialogueSound != null)
        {
            AudioSource.PlayClipAtPoint(dialogueSound, transform.position);
        }


        //make sure you have reached the end of the sentence
        if (charIndex <= currentDialogue.Length - 1)
        {
            //Wait x seconds
            yield return new WaitForSecondsRealtime(writingSpeed);

            //restart same process
            StartCoroutine(Writing());
        }
        else
        {
            waitForNext = true; //End this sentence and wait for the next one
        }

    }

    void Update() //
    {
        if (!started)
            return;

        if (waitForNext && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            waitForNext = false;

            index++;

            //Check if we are in the scope of dialogues list
            if (index < dialogues.Count)
            {
                //ifso fetch the next dialogue
                GetDialogue(index);
            }
            else
            {
                // If not, end the dialogue process
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }
}