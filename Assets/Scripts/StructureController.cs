using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureController : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 objectPos;
    private float structureWidth;

    private GameObject currentStructure;
    public GameObject[] structures = new GameObject[3];

    // The sprites that will work as a guide (0 is box, 1 is stair, 2 is bridge and 4 is crane)
    private Sprite currentSprite;
    public Sprite[] structureSprites = new Sprite[3];

    public GameObject gridHelper;
    private GameObject gridInstance;

    private int structureSwitchCounter = 0;

    private float decimalOffsetX;
    private float decimalOffsetY;

    private Vector3 prevObjectPos;

    // The stack to keep track of the gameObjects spawned. This variable is public static so other scripts can modify it
    public static Stack<GameObject> spawnedStructures = new Stack<GameObject>();

    public static GameObject selectedObject;
    public static bool isSelecting;
    public GameObject selectorButton;
    private bool isSelectingActive = false;

    private GameObject structureToDelete;

    public static bool canBuild = true;

    public static Dictionary<string, int> structureCoordenates;

    void Start()
    {
        // Set the initial structure as the Box, and make it invisible just in case (its changed to visible if it can spawn. This is checked in the SpawnConditions script)
        currentStructure = null;
        currentSprite = structureSprites[0];

        // This works for the grid guide. Set the prevPos to a random value and instanciate the grid object
        prevObjectPos = new Vector3(-200f, -200f, 2.0f);
        gridInstance = Instantiate(gridHelper, new Vector3(-500f, -500f, 2.0f), Quaternion.identity);

        // Each level start, initialize the dictionary again
        structureCoordenates = new Dictionary<string, int>();
    }


    void Update()
    {
        if (selectedObject)
        {           
            if (Input.GetButtonDown("Jump"))
            {
                switch (selectedObject.tag)
                {
                    case "Box":
                        structureCoordenates.Remove("" + selectedObject.transform.position + selectedObject.tag);
                        selectedObject.GetComponent<SpawnConditionsBox>().DestroyBox();                       
                        break;
                    case "Stair":
                        structureCoordenates.Remove("" + selectedObject.transform.position + selectedObject.tag);
                        Destroy(selectedObject);
                        LevelResources.numOfStair++;
                        break;
                    case "Bridge":
                        structureCoordenates.Remove("" + selectedObject.transform.position + selectedObject.tag);
                        Destroy(selectedObject);                       
                        LevelResources.numOfBridge++;
                        break;
                    default:
                        break;
                }                
            }
        }

        if (currentStructure && canBuild)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 2.0f;
            objectPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Get the decimal part of the position
            decimalOffsetX = Mathf.Abs(objectPos.x % 1);
            decimalOffsetY = Mathf.Abs(objectPos.y % 1);

            // The width of the structure (get half, to make the objects place next to each other)
            structureWidth = currentStructure.GetComponent<BoxCollider2D>().size.x / 2;

            if (decimalOffsetX < structureWidth)
            {
                // if the decimal is lower that 0.5 (width / 2), then sum (or substract if the number is negative, hence the Mathf.Sign multiplication) whatever it needs to get to 0.5 (to have Grid Like positioning)
                objectPos.x = objectPos.x + Mathf.Sign(objectPos.x)*(structureWidth - decimalOffsetX);
            }
            else if (decimalOffsetX > structureWidth)
            {
                // if the decimal is more that 0.5 (width / 2), then substract (or sum if the number is negative, hence the Mathf.Sign multiplication) whatever it needs to get to 0.5(to have Grid Like positioning)
                objectPos.x = objectPos.x - Mathf.Sign(objectPos.x)*(decimalOffsetX - structureWidth);
            }

            // Same principle as with the X positioning, but with the Y Axis
            if (decimalOffsetY < 0.5)
            {
                objectPos.y = objectPos.y + Mathf.Sign(objectPos.y)*(0.5f - decimalOffsetY);
            }
            else if (decimalOffsetY > 0.5)
            {
                objectPos.y = objectPos.y - Mathf.Sign(objectPos.y)*(decimalOffsetY - 0.5f);
            }

            // If the position changed (from a grid perspective), move the grid Helper to that new grid
            if (prevObjectPos != objectPos)
            {
                gridInstance.transform.position = objectPos;
                prevObjectPos = objectPos;
            }

            // If he clicks mouse 1, instanciate the current structure selected (as invisible, the script SpawnConditions checks if it can spawn there and either make it visible or delete the gameObject)
            if (Input.GetButtonDown("Fire1"))
            {
                // Check if there is an object of the same type in that position
                try 
                {   
                    // If there is, dont do anything. Meaning dont spawn another one
                    if(structureCoordenates["" + objectPos + currentStructure.tag] == 1)
                    {
                        Debug.Log("There is someone here...");
                    }

                }

                // If there was no key (no object of that type in that position), mark that position as taken and instanciate the object to check for further spawn conditions 
                catch (KeyNotFoundException)
                {
                    structureCoordenates.Add("" + objectPos + currentStructure.tag, 1);
                    Instantiate(currentStructure, objectPos, Quaternion.identity);
                }
                
                
            }
               
        }

        // Quick switch structures, cycles to the next one
        if (Input.GetKeyDown("e"))
        {
            structureSwitchCounter++;
            currentStructure = structures[structureSwitchCounter % 3];
            currentSprite = structureSprites[structureSwitchCounter % 3];
            gridInstance.GetComponent<SpriteRenderer>().sprite = currentSprite;

            // Also stop selecting
            isSelecting = false;
            selectorButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void SetStructureBox()
    {
        // If the player clicks a structure to build, exit the selection tool
        isSelecting = false;
        selectorButton.GetComponent<Image>().color = Color.white;

        //selectedObject = null;
        // Set the new structure to the Box prefab AND change the sprite of the grid to the Box sprite
        structureSwitchCounter = 0;
        currentStructure = structures[0];
        currentSprite = structureSprites[0];
        gridInstance.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    public void SetStructureStairs()
    {
        isSelecting = false;
        selectorButton.GetComponent<Image>().color = Color.white;

        //selectedObject = null;
        // Set the new structure to the Stairs (function called when user clicks button) prefab AND change the sprite of the grid to the stairs sprite
        structureSwitchCounter = 1;
        currentStructure = structures[1];
        currentSprite = structureSprites[1];
        gridInstance.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    public void SetStructureBridge()
    {
        isSelecting = false;
        selectorButton.GetComponent<Image>().color = Color.white;

        //selectedObject = null;
        // Set the new structure to the Bridge (function called when user clicks button) prefab AND change the sprite of the grid to the bridge sprite
        structureSwitchCounter = 2;
        currentStructure = structures[2];
        currentSprite = structureSprites[2];
        gridInstance.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    public void UndoButton()
    {
        if (selectedObject)
        {
            switch (selectedObject.tag)
            {
                case "Box":
                    structureCoordenates.Remove("" + selectedObject.transform.position + selectedObject.tag);
                    selectedObject.GetComponent<SpawnConditionsBox>().DestroyBox();
                    break;
                case "Stair":
                    structureCoordenates.Remove("" + selectedObject.transform.position + selectedObject.tag);
                    Destroy(selectedObject);
                    LevelResources.numOfStair++;
                    break;
                case "Bridge":
                    structureCoordenates.Remove("" + selectedObject.transform.position + selectedObject.tag);
                    Destroy(selectedObject);
                    LevelResources.numOfBridge++;
                    break;
                default:
                    break;
            }
        }
        
    }

    public void SelectorToolButton()
    {
        isSelectingActive = !isSelectingActive;

        isSelecting = !isSelecting;
        currentStructure = null;
        gridInstance.GetComponent<SpriteRenderer>().sprite = null;

        if (isSelecting)
        {
            selectorButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            selectorButton.GetComponent<Image>().color = Color.white;
        }

        
    }


}
