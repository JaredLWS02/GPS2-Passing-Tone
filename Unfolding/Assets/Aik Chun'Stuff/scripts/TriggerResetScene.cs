using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerResetScene : MonoBehaviour
{
    [SerializeField] private UiTween tween;
    // Start is called before the first frame update
    private enum Scene
    {
        MainScene
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlackenScreen()
    {
        tween.BlackenScreenTransition(0.5f);

    }

    private void ResetScene()
    {
        SceneManager.LoadScene(Scene.MainScene.ToString());
    }
}
