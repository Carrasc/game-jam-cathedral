using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Datos_Managment : MonoBehaviour
{
    public void Historia()
    {
        SceneManager.LoadScene("Historia1");
        

    }



    public void Restauracion()
    {
        SceneManager.LoadScene("Restauracion1");
        

    }

    

    public void Arquitecto()
    {
        SceneManager.LoadScene("Arquitecto1");


    }

    public void Return()
    {
        SceneManager.LoadScene("Contenido_Extra");


    }
}
