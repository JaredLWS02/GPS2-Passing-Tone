using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutSceneEnd : MonoBehaviour
{
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject cutSceneCam;
    [SerializeField] private GameObject cutScene;
    [SerializeField] private GameObject boat;
    [SerializeField] private Transform boatSpawn;
    [SerializeField] private Animator anim1;
    [SerializeField] private Animator anim2;
    [SerializeField] private Animator anim3;
    [SerializeField] private UiTween ui;
    [SerializeField] private AudioSource waves;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endScene()
    {
        StartCoroutine(transition());
    }

    private IEnumerator transition()
    {
        ui.BlackenScreenTransition();
        yield return new WaitForSecondsRealtime(0.5f);
        waves.Stop();
        cutSceneCam.SetActive(false);
        ui.UnBlackenScreenTransition(0.3f);
        mainCam.SetActive(true);
        anim1.enabled = false;
        anim2.enabled = false;
        anim3.enabled = false;
        cutScene.SetActive(false);
        boat.transform.position = boatSpawn.position;
        boat.transform.Rotate(Vector3.forward, 90);

    }
}
