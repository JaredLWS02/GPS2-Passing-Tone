using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField] private NavMeshSurface nav;
    [SerializeField] private NavMeshAgent player;

    private bool doOnce;

    // Start is called before the first frame update
    void Awake()
    {
        //doOnce = true;

        if (nav.navMeshData != null)
        {
            nav.UpdateNavMesh(nav.navMeshData);
        }
        else
        {
            nav.BuildNavMesh();
            Debug.Log("You forgot to reference the navmesh. But it not big deal");
        }
    }

    private void Update()
    {
        if (PageFlip.flipped == true && doOnce == false)
        {
            Debug.Log("generated nav mesh");
            if (!nav.isActiveAndEnabled)
            {
                StartCoroutine(enableNavMesh());
            }

                doOnce = true;
        }
        else if (PageFlip.flipped == false && doOnce == true)
        {
            //StartCoroutine(enableNavMesh());

            doOnce = false;
        }

        if(PageFlip.clicked)
        {
            player.enabled = false;
            nav.enabled = false;
        }
        else
        {
            if(PageFlip.flipped)
            {
                player.enabled = true;
                nav.enabled = true;
            }
        }
    }

    private IEnumerator enableNavMesh()
    {
        nav.enabled = true;
        //yield return new WaitForSeconds(0.02f);
        //nav.BuildNavMesh();
        nav.UpdateNavMesh(nav.navMeshData);
        yield return new WaitForSeconds(0.2f);
        player.enabled = true;

    }
}
