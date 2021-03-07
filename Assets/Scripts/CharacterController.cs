using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    public static GameObject activeCharacter;
    private GameObject[] characters = new GameObject[2];    // The constructor and the restorer

    private int characterSwitch = 0; // 0 is for constructor and 1 for restorer

    public Transform spawnPos;
    public GameObject constructorPrefab;
    public GameObject restorerPrefab;

    public Button constructorButton;
    public Button restorerButton;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(spawnPos.position.x, spawnPos.position.y, spawnPos.position.z);
        characters[0] = Instantiate(constructorPrefab, position, Quaternion.identity);

        position = new Vector3(spawnPos.position.x + 2f, spawnPos.position.y, spawnPos.position.z);
        characters[1] = Instantiate(restorerPrefab, position, Quaternion.identity);

        activeCharacter = characters[0];
        constructorButton.GetComponent<Image>().color = Color.gray;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            characterSwitch++;
            // Switch the character to the next one (the mod 2 cycles when the counter is going to go above the number of elements, which is 2)
            activeCharacter = characters[characterSwitch % 2];

            if (activeCharacter == characters[0])
            {
                constructorButton.GetComponent<Image>().color = Color.gray;
                restorerButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                constructorButton.GetComponent<Image>().color = Color.white;
                restorerButton.GetComponent<Image>().color = Color.gray;
            }
            
        }
    }

    public void ConstructorButton()
    {
        characterSwitch = 0;
        activeCharacter = characters[characterSwitch];
        constructorButton.GetComponent<Image>().color = Color.gray;
        restorerButton.GetComponent<Image>().color = Color.white;
    }

    public void RestorerButton()
    {
        characterSwitch = 1;
        activeCharacter = characters[characterSwitch];
        constructorButton.GetComponent<Image>().color = Color.white;
        restorerButton.GetComponent<Image>().color = Color.gray;
    }
}
