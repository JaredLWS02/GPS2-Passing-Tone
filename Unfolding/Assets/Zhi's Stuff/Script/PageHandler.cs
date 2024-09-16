using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PageHandler : MonoBehaviour
{
    //public NavMeshData navMesh1;

    public GameObject frog;

    private Transform t;
    private bool doOnce;

    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        doOnce = false;
    }

    public void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        // Do raycast to selected page to select which page
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.touches[0].position != null)
            {
                ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (PageFlip.flipped == true && doOnce == false && GameEventManager.selectedPage == hit.collider.name && hit.collider.tag == "Edge")
                    {
                        // find in child which object to set the position to
                        Debug.Log("Called");
                        t = hit.transform.Find("Spawn Point").GetComponent<Transform>();
                        frog.transform.position = t.position;
                        doOnce = true;
                    }
                }
                else
                    return;
            }
        }
        else if (Input.GetMouseButtonUp(0) && hit.collider.tag == "Edge")
        {
            frog.transform.position = t.position;
            StartCoroutine(setpos());
        }

        if (PageFlip.flipped == false)
        {
            doOnce = false;
            if (t != null)
                frog.transform.position = t.position;
        }

        if (hit.transform != null && hit.collider.tag == "Edge")
            t = hit.transform.Find("Spawn Point").GetComponent<Transform>();

    }

    private IEnumerator setpos()
    {
        NavMeshHit hit;

        while (!NavMesh.SamplePosition(t.position, out hit, 0.3f, NavMesh.AllAreas))
        {
            yield return null;
        }
            frog.GetComponent<NavMeshAgent>().Warp(hit.position);
    }

    public void NavMap1()
    {
        return;
    }
}
