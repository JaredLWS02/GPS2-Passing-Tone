using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public bool isRotate
    {
        get;
        set;
    }
    private RaycastHit hit;
    public bool tapToMove;
    private bool startMove;

    public bool isMoving;

    private Animator playerAnim;
    private bool isChecking;

    private Animator targetMarkAnim;

    private Vector2 startDist;
    private Vector2 endDist;

    [SerializeField] private NavMeshAgent player;
    [SerializeField] private GameObject targetMark;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private List<AudioClip> walkingClip;
    [SerializeField] private AudioSource walkingAudioSource;

    private int counter;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        targetMarkAnim = targetMark.GetComponent<Animator>();
        isRotate = false;
        tapToMove = true;
        startMove = false;
        isMoving = false;
    }

    private void OnEnable()
    {
        isChecking = false;
    }

    //private void OnDisable()
    //{
    //    if(targetMark.activeSelf)
    //    {
    //        targetMark.SetActive(false);
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        if (isRotate || GameEventManager.isTouchPage || GameEventManager.isPuzzling)
        {
            //if(GameEventManager.isTouchPage)
            playerAnim.SetBool("ismoving", false);
            //{
            //StopAllCoroutines();
            //playerAnim.ResetTrigger("isMoving");
            //playerAnim.ResetTrigger("Stop");
            //}
            return;
        }

        if (!GameEventManager.isTouchObject)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startDist = Input.GetTouch(0).position;
                    tapToMove = true;
                    startMove = true;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    endDist = Input.GetTouch(0).position;
                    if ((endDist - startDist).magnitude >= 100.0f)
                    {
                        tapToMove = false;
                    }
                }

                if (startMove)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Ended && tapToMove)
                    {
                        Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                        if (Physics.Raycast(touchRay, out hit, rayDistance, groundLayer))
                        {
                            Debug.Log(hit.collider.gameObject.name);
                            if (!hit.collider.CompareTag("Obstacles"))
                            {
                                if (player.hasPath)
                                {
                                    StopCoroutine(Move());
                                    StopCoroutine(checkMove());
                                    //playerAnim.SetBool("isMoving", false);
                                    //playerAnim.ResetTrigger("isMoving");
                                    //playerAnim.ResetTrigger("Stop");
                                    StartCoroutine(Move());
                                }
                                else
                                {
                                    StartCoroutine(Move());
                                }
                            }
                            //StartCoroutine(visualizeMovement());

                        }
                    }

                }
            }

        }
        else
        {
            startMove = false;
        }

    }
    private IEnumerator Move()
    {
            player.SetDestination(hit.point);
            while (player.pathPending)
            {
                yield return null;
            }

            if (player.path.status == NavMeshPathStatus.PathComplete)
            {
                isMoving = true;
                yield return new WaitForSeconds(0.05f);
                targetMark.transform.position = player.pathEndPosition;
                targetMark.gameObject.SetActive(true);
                targetMarkAnim.Play("blooming animation",0,0);

                if (playerCamera.transform.localEulerAngles.y == 90.0f || playerCamera.transform.localEulerAngles.y == 270.0f)
                {
                    checkRotZ();
                }
                else
                {
                    checkRotX();
                }

                if (!isChecking)
                {
                    playerAnim.SetBool("ismoving", true);
                    StartCoroutine(checkMove());
                }
            }
            else
            {
                player.ResetPath();
            }
    }
    private IEnumerator checkMove()
    {
        isChecking = true;
        while (player.hasPath && player.velocity.magnitude > 0)
        {
            //if (!walkingAudioSource.isPlaying)
            //{
            //    if (counter >= walkingClip.Count)
            //    {
            //        counter = 0;
            //    }

            //    walkingAudioSource.clip = walkingClip[counter];
            //    walkingAudioSource.Play();
            //    counter++;
            //}
            yield return null;
        }
        //playerAnim.SetTrigger("Stop");
        playerAnim.SetBool("ismoving", false);
        targetMark.gameObject.SetActive(false);
        isChecking = false;
        isMoving = false;
        walkingAudioSource.Stop();
        counter = 0;
    }

    private void checkRotX()
    {
        float playerposX = gameObject.transform.position.x;
        if (playerposX > player.pathEndPosition.x)// if going left
        {
            if (gameObject.transform.localEulerAngles.y <= 0)// if already facing right
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y + 180, 0); //face left
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y, 0);//keep facing left
            }
        }
        else if (playerposX < player.pathEndPosition.x) // if going right
        {
            if (gameObject.transform.localEulerAngles.y <= 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y, 0); // keep facing right
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y - 180, 0); // face right

            }
        }
    }

    private void checkRotZ()
    {
        float playerposZ = gameObject.transform.position.z;
        if (playerposZ < player.pathEndPosition.z)// if going left
        {
            if (gameObject.transform.localEulerAngles.y < 270)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y + 180, 0); // keep facing left
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y, 0); // face left
            }
        }
        else if (playerposZ > player.pathEndPosition.z) // if going right
        {
            if (gameObject.transform.localEulerAngles.y > 90)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y - 180, 0); // keep facing right
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y, 0); // face right

            }
        }
    }
    
    private void PlayWalking1()
    {
        if(counter >= walkingClip.Count)
        {
            counter = 0;
        }
        walkingAudioSource.clip = walkingClip[counter];
        walkingAudioSource.Play();
        counter++;
    }
}
