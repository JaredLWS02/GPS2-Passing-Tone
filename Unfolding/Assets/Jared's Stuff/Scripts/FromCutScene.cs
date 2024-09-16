using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromCutScene : MonoBehaviour
{
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject cutSceneCam;
    [SerializeField] private GameObject cutScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "cutScene")
        {
            Debug.Log("Touched");
            mainCam.SetActive(true);
            cutSceneCam.SetActive(false);
            cutScene.SetActive(false);
        }
    }
}
