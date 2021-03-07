using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheetsManagment : MonoBehaviour
{
    public void Restauration()
    {
        SceneManager.LoadScene("Restauration");


    }



    public void Agents()
    {
        SceneManager.LoadScene("Agents");


    }


     public void Dates()
    {
        SceneManager.LoadScene("Historic_dates");


    }


    public void Return()
    {
        SceneManager.LoadScene("Extra_content");


    }




}