using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtraManagment : MonoBehaviour
{
    public void Cinematic()
    {
        SceneManager.LoadScene("Cinematic");


    }



    public void Technical_Sheets()
    {
        SceneManager.LoadScene("Technical_sheets");


    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");


    }




}