using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorColorHighlight : MonoBehaviour
{
    public Sprite defaultSprite;
    public int defaultLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Restorer") || gameObject.CompareTag("Player"))
        {
            if (StructureController.selectedObject != gameObject && gameObject.GetComponent<Animator>().GetBool("isSelected") == true)
            {
                gameObject.GetComponent<Animator>().SetBool("isSelected", false);
            }

            
        }
        else
        {
            if (StructureController.selectedObject != gameObject && gameObject.GetComponent<SpriteRenderer>().sprite != defaultSprite)
            {
                // If the gameObject is not the selected object, but it had a different sprite from the default one (it was selected before), return it to its normal state
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = defaultLayer;

            }
        }
        
       
    }
}
