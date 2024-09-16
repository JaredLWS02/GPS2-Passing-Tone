using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBig : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    public void BecomeBig()
    {
        player.transform.localScale = new Vector3(0.24f, 0.24f, 0.24f);
        cam.fieldOfView = 70;
    }
}
