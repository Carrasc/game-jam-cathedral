using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{
    public bool inicioJuego;
    public bool finalJuego;


    public Text timerTextValue;
    public float timer;

    public Text finalTimeText;

    public GameObject menuPausa;
    public GameObject menuFinJuego;
    public GameObject menuFinNivel;
    public GameObject menuInstrucciones;

    private int numberOfTutorialMenus;
    private int currentTutorialIndex;


    private bool progresoButtonActivado;
    private int contadorPausa;
    private bool pausa;

    [Header("Bonus score reference ")]
    public Text bonus;

    [Header("The next button for end of level")]
    public Button nextArrow;

    [Header("The score values for the level for each medal and passing score")]
    public  int passingScore;
    public  int bronzeScore;
    public  int silverScore;
    public  int goldScore;

    public Image medalReference;
    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    public Sprite noMedal;

    [Header("The text (TMP) rererences on the canvas for the final level details (score)")]
    public TextMeshProUGUI level1Score;
    public TextMeshProUGUI level2Score;
    public TextMeshProUGUI level3Score;

    [Header("The text (normal text) rererence on the canvas for the high score value")]
    public Text levelHighScore;

    [Header("The Image rererence on the canvas for the high score image (to show when new high score)")]
    public Image highScoreImage;

    [Header("The image rererences on the canvas for the final level details (medals)")]
    public Image level1Medal;
    public Image level2Medal;
    public Image level3Medal;


    // Start is called before the first frame update
    void Start()
    {
        //menuPausa = GameObject.Find("CanvasPausa");
        menuPausa.SetActive(false);
        menuFinJuego.SetActive(false);

        if (menuInstrucciones)
        {
            numberOfTutorialMenus = menuInstrucciones.transform.childCount;
            currentTutorialIndex = 0;
        }
        else
        {
            pausa = false;
        }
        

        //timerText = GameObject.Find("Timer").GetComponent<Text>();
        //timer = 70;
        inicioJuego = true;
    }

    // Update is called once per frame
    void Update()
    {  
        if (inicioJuego && !finalJuego && !pausa)
        {
            StructureController.canBuild = true;
            CharacterMovement.canMove = true;
            FuncionTiempo();
        }

        if (pausa)
        {
            StructureController.canBuild = false;
            CharacterMovement.canMove = false;
        }

        FuncionFinTiempo();
        FuncionPausaTeclado();

    }

    private void FuncionFinTiempo()
    {
        if(timer <= 1 || (LevelResources.mossAgents == 0 && LevelResources.crackAgents == 0 && LevelResources.poopAgents == 0) )
        {
            pausa = true;
            PlayerPrefs.SetInt("Level " + LevelResources.difficultyNumberGlobal, LevelResources.score);

            // Every 10 extra seconds is 50 points
            int bonusValue = ((int)(timer / 10)) * 50;
            bonus.text = "" + bonusValue;

            int score = LevelResources.score + bonusValue;

            //Debug.Log(PlayerPrefs.GetInt("Level " + LevelResources.difficultyNumberGlobal + " high score"));
            //Debug.Log(score);

            // Check if the score obtained is higher than the high score, and if it is, set the new high score for the level
            if (PlayerPrefs.HasKey("Level " + LevelResources.difficultyNumberGlobal + " high score"))
            {
                if (score >= PlayerPrefs.GetInt("Level " + LevelResources.difficultyNumberGlobal + " high score"))
                {
                    PlayerPrefs.SetInt("Level " + LevelResources.difficultyNumberGlobal + " high score", score);
                    levelHighScore.text = "" + score;
                    highScoreImage.gameObject.SetActive(true);
                    Debug.Log("NEW HIGH SCORE");
                }
                else
                {
                    levelHighScore.text = "" + PlayerPrefs.GetInt("Level " + LevelResources.difficultyNumberGlobal + " high score");
                    highScoreImage.gameObject.SetActive(false);
                    Debug.Log("NOT HIGH SCORE");
                }
            }
            else
            {
                PlayerPrefs.SetInt("Level " + LevelResources.difficultyNumberGlobal + " high score", score);
                levelHighScore.text = "" + score;
                highScoreImage.gameObject.SetActive(true);
            }
            
            //Debug.Log("HIGH SCORE: " + PlayerPrefs.GetInt("Level " + LevelResources.difficultyNumberGlobal + " high score"));

            // No passing score
            if (score < passingScore)
            {
                // Hide the next button
                nextArrow.gameObject.SetActive(false);
                medalReference.sprite = noMedal;
            }
            // Bronze medal score
            else if (score >= bronzeScore && score < silverScore)
            {
                nextArrow.gameObject.SetActive(true);
                medalReference.sprite = bronze;
                PlayerPrefs.SetString("Level " + LevelResources.difficultyNumberGlobal + " medal", "bronze");
            }
            // Silver medal score
            else if (score >= silverScore && score < goldScore)
            {
                nextArrow.gameObject.SetActive(true);
                medalReference.sprite = silver;
                PlayerPrefs.SetString("Level " + LevelResources.difficultyNumberGlobal + " medal", "silver");
            }
            // Gold medal score
            else if (score >= goldScore)
            {
                nextArrow.gameObject.SetActive(true);
                medalReference.sprite = gold;
                PlayerPrefs.SetString("Level " + LevelResources.difficultyNumberGlobal + " medal", "gold");
            }

           

            if (LevelResources.difficultyNumberGlobal == "3" && score > passingScore)
            {
                level1Score.text = "" + PlayerPrefs.GetInt("Level 1", 0);
                level2Score.text = "" + PlayerPrefs.GetInt("Level 2", 0);
                level3Score.text = "" + PlayerPrefs.GetInt("Level 3", 0);

                switch (PlayerPrefs.GetString("Level 1 medal", ""))
                {
                    case "bronze":
                        level1Medal.sprite = bronze;
                        break;
                    case "silver":
                        level1Medal.sprite = silver;
                        break;
                    case "gold":
                        level1Medal.sprite = gold;
                        break;
                    default:
                        break;
                  
                }

                switch (PlayerPrefs.GetString("Level 2 medal", ""))
                {
                    case "bronze":
                        level2Medal.sprite = bronze;
                        break;
                    case "silver":
                        level2Medal.sprite = silver;
                        break;
                    case "gold":
                        level2Medal.sprite = gold;
                        break;
                    default:
                        break;

                }

                switch (PlayerPrefs.GetString("Level 3 medal", ""))
                {
                    case "bronze":
                        level3Medal.sprite = bronze;
                        break;
                    case "silver":
                        level3Medal.sprite = silver;
                        break;
                    case "gold":
                        level3Medal.sprite = gold;
                        break;
                    default:
                        break;

                }

                menuFinNivel.SetActive(true);
            }
            else
            {
                menuFinJuego.SetActive(true);
                
            }
            
            int minutos = (int)timer / 60;
            int segundos = (int)timer % 60;
            timerTextValue.text = "--:--";
            finalTimeText.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');

            


            //Time.timeScale = 0;
        }
    }


    private void FuncionTiempo()
    {
        if (menuInstrucciones)
        {
            if (menuInstrucciones.activeSelf)
            {
                pausa = true;
            }
        }

        timer -= Time.deltaTime;
        int minutos = (int)timer / 60;
        int segundos = (int)timer % 60;
        //int milesimas = (int)timer / 1000;

        timerTextValue.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');
    }

    public void FuncionPausa()
    {
        pausa = !pausa;
        menuPausa.SetActive(pausa);

    }
    private void FuncionPausaTeclado()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FuncionPausa();
        }

    }

    public void NextTutorial()
    {
        menuInstrucciones.transform.GetChild(currentTutorialIndex).gameObject.SetActive(false);

        currentTutorialIndex = (currentTutorialIndex + 1) % numberOfTutorialMenus;

        menuInstrucciones.transform.GetChild(currentTutorialIndex).gameObject.SetActive(true);
    }

    public void PrevTutorial()
    {
        menuInstrucciones.transform.GetChild(currentTutorialIndex).gameObject.SetActive(false);

        currentTutorialIndex = (currentTutorialIndex - 1) % numberOfTutorialMenus;

        menuInstrucciones.transform.GetChild(currentTutorialIndex).gameObject.SetActive(true);
    }

    public void PlayGametutorialButton()
    {
        menuInstrucciones.SetActive(false);
        pausa = false;
    }

}
