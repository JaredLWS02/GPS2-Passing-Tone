using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [SerializeField] private GameObject frog;
    [SerializeField] private GameObject cam;
    [SerializeField] private List<Transform> npc;
    [SerializeField] private List<GameObject> Sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var npc in npc)
        {
            if (!npc.gameObject.activeInHierarchy)
            {
                continue;
            }

                if (cam.transform.localEulerAngles.y == 90.0f)
                {
                    if (frog.transform.position.x >= npc.position.x)
                    {
                        foreach (var sprite in Sprite)
                        {
                            sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Beaver");//go behind
                        }
                        break;
                    }
                    else
                    {
                        foreach (var sprite in Sprite)
                        {
                            sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Frog");//go infront
                        }
                        break;
                    }
                }
                else if (cam.transform.localEulerAngles.y == 270.0f)
                {
                    if (frog.transform.position.x <= npc.position.x)
                    {
                        foreach (var sprite in Sprite)
                        {
                            sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Beaver");
                        }
                        break;
                    }
                    else
                    {
                        foreach (var sprite in Sprite)
                        {
                            sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Frog");
                        }
                        break;
                    }
                }
                else if (cam.transform.localEulerAngles.y == 180.0f)
                {
                    if (frog.transform.position.z <= npc.position.z)
                {
                    foreach(var sprite in Sprite)
                    {
                        sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Beaver");
                    }
                    break;
                }
                else
                {
                    foreach (var sprite in Sprite)
                    {
                        sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Frog");
                    }
                    break;
                }

                }
                else
                {
                    if (frog.transform.position.z >= npc.position.z)
                    {
                        foreach (var sprite in Sprite)
                        {
                            sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Beaver");
                        }
                        break;
                    }
                    else
                    {
                        foreach (var sprite in Sprite)
                        {
                            sprite.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Frog");
                        }
                        break;
                    }
                }



        }
    }
}
