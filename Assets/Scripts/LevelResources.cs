using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelResources : MonoBehaviour
{
    [Header("The level and difficulty text (on the canvas) and value")]
    public string levelNumber;
    public string difficultyNumber;
    public Text levelNumberText;
    public Text difficultyNumberText;
    public static string difficultyNumberGlobal;

    [Header("The amount of resources the player will have for the level")]
    [Space(10)]
    public int numOfBoxLevel;
    public int numOfStairLevel;
    public int numOfBridgeLevel;

    // These variables are just for other scripts to modify the numbers set above
    public static int numOfBox;
    public static int numOfStair;
    public static int numOfBridge;


    [Header("The text elements in the canvas (to update as the game goes)")]
    [Space(10)]
    public Text box;
    public Text stair;
    public Text bridge;
    public Text scoreText;
    public Text scoreFinalText;
    public Text mossNumberText;
    public Text crackNumberText;
    public Text poopNumberText;

    public static GameObject[] levelAgents;
    public static int score = 0;

    [Header("The amount of agents of each type in the level")]
    [Space(10)]
    public int mossAgentsLevel;
    public int crackAgentsLevel;
    public int poopAgentsLevel;

    // These variables are just for other scripts to modify the numbers set above
    public static int mossAgents;
    public static int crackAgents;
    public static int poopAgents;



    // Start is called before the first frame update
    void Start()
    {
        numOfBox = numOfBoxLevel;
        numOfStair = numOfStairLevel;
        numOfBridge = numOfBridgeLevel;

        levelNumberText.text = "Nivel - " + levelNumber;
        difficultyNumberText.text = "Dificultad - " + difficultyNumber;
        difficultyNumberGlobal = difficultyNumber;

        score = 0;

        mossAgents = mossAgentsLevel;
        crackAgents = crackAgentsLevel;
        poopAgents = poopAgentsLevel;
    }

    // Update is called once per frame
    void Update()
    {
        // Structures update
        if (box.text != "x" + numOfBox || stair.text != "x" + numOfStair || bridge.text != "x" + numOfBridge)
        {
            box.text = "x" + numOfBox;
            stair.text = "x" + numOfStair;
            bridge.text = "x" + numOfBridge;
            Debug.Log("updating text structs");
        }

        // If the score changed, so did the agents, so update the texts. This is done in an attempt to optimize (very poorly) and not update the texts every frame 
        if (scoreText.text != score.ToString())
        {
            // Score update
            scoreText.text = "" + score;
            scoreFinalText.text = "" + score;

            //Agents update
            mossNumberText.text = "" + mossAgents;
            crackNumberText.text = "" + crackAgents;
            poopNumberText.text = "" + poopAgents;
            Debug.Log("updating text score");
        }       

    }

}
