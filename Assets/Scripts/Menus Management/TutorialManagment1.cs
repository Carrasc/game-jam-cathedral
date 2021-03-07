using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManagment1 : MonoBehaviour
{
    public void Next1()
    {
        SceneManager.LoadScene("Tutorial1");


    }

     public void Next2()
    {
        SceneManager.LoadScene("Tutorial2");


    }


     public void Next3()
    {
        SceneManager.LoadScene("Tutorial3");


    }

     public void Next4()
    {
        SceneManager.LoadScene("Tutorial4");


    }

    public void Next5()
    {
        SceneManager.LoadScene("Tutorial5");


    }

    public void Play()
    {
        SceneManager.LoadScene("MainMenu");


    }




    public void Return()
    {
        SceneManager.LoadScene("MainMenu");


    }




}