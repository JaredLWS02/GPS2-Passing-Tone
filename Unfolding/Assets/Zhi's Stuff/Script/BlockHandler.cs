using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BlockHandler : MonoBehaviour
{
    private GameObject temp;
    public void OnClick()
    {
        temp = this.gameObject;
        GameEventManager.isTouchObject = true;
        GetComponentInParent<SlidingPuzzleHandler>().OnClick(temp);
    }
}
