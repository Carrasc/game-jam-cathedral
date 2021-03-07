using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    private GameObject topMostObject;
    private int currentLargestLayer;

    public Sprite selectedSpriteConstructor;
    public Sprite selectedSpriteRestorer;
    public Sprite selectedSpriteBox;
    public Sprite selectedSpriteStair;
    public Sprite selectedSpriteBridge;
    public Sprite selectedSpriteCrane;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && StructureController.isSelecting)
        {
            topMostObject = null;
            currentLargestLayer = 0;

            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.layer > currentLargestLayer)
                {
                    topMostObject = hit.collider.gameObject;
                    currentLargestLayer = hit.collider.gameObject.layer;
                }
                //Debug.Log("Target layer: " + hit.collider.gameObject.layer + "Target name: " + hit.collider.gameObject.name);
            }

            if (topMostObject)
            {
                Debug.Log("Top object: " + topMostObject.name);
                StructureController.selectedObject = topMostObject;

                if (topMostObject.CompareTag("Box"))
                {
                    topMostObject.GetComponent<SpriteRenderer>().sprite = selectedSpriteBox;
                    // Make the sorting order high, so the player can see the highlight above other structures
                    topMostObject.GetComponent<SpriteRenderer>().sortingOrder = 1000;
                }
                else if (topMostObject.CompareTag("Stair"))
                {
                    topMostObject.GetComponent<SpriteRenderer>().sprite = selectedSpriteStair;
                    // Make the sorting order high, so the player can see the highlight above other structures
                    topMostObject.GetComponent<SpriteRenderer>().sortingOrder = 1000;
                }
                else if (topMostObject.CompareTag("Bridge"))
                {
                    topMostObject.GetComponent<SpriteRenderer>().sprite = selectedSpriteBridge;
                    // Make the sorting order high, so the player can see the highlight above other structures
                    topMostObject.GetComponent<SpriteRenderer>().sortingOrder = 1000;
                }

                //Debug.Log("Top most object: " + topMostObject.name);
                //Debug.Log("Test, new sprite: " + topMostObject.GetComponent<SpriteRenderer>().sprite);
            }
            

        }
    }
}
