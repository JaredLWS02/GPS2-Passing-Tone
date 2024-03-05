using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    [SerializeField] private float swipeDist;


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
        if (isRotate)
        {
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
                    playerAnim.SetTrigger("isMoving");
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
            yield return null;
        }
        playerAnim.SetTrigger("Stop");
        targetMark.gameObject.SetActive(false);
        isChecking = false;
        isMoving = false;
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
    //private IEnumerator visualizeMovement()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    int i = 1;
    //    //lr.positionCount = amtOfCorners;

    //    int amtOfCorners = player.path.corners.Length;
    //    points = player.path.corners.ToList();
    //    while (i < amtOfCorners)
    //    {
    //        for (int j = 0; j < points.Count; j++)
    //        {
    //            lr.SetPosition(j, points[j]);
    //        }
    //        i++;
    //    }
    //}
    //void checkForSlopes()
    //{
    //    if (points[0].y < points[1].y) // going up
    //    {
    //        Vector3 distance = points[1] - points[0];

    //        //float length = distance.magnitude;


    //        if(startpos.y < transform.position.y)
    //        {
    //            startpos = new Vector3(transform.position.x,transform.position.y / 2,transform.position.z);
    //        }

    //        //for(int i = 0; i < 2; i++)
    //        //{
    //            NavMesh.(1, startpos);
    //        //}

    //        lr.SetPositions(points.ToArray());

    //    }
    //    else if (points[0].y > points[1].y) // going down
    //    {

    //    }
    //    else
    //    {
    //        lr.SetPositions(points.ToArray());
    //    }

    //}
}
