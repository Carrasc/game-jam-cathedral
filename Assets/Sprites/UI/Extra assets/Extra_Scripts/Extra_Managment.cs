using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Extra_Managment : MonoBehaviour
{
    public void Datos()
    {
        SceneManager.LoadScene("Datos_Historicos");
        

    }



    public void Oficina()
    {
        SceneManager.LoadScene("Oficina");
        

    }

    

    public void Creditos()
    {
        SceneManager.LoadScene("Credits");


    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");


    }

}
