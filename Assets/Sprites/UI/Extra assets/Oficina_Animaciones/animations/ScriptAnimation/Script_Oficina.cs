using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;




public class Script_Oficina : MonoBehaviour
{
    public static bool gameP;


    //VAriables de los paneles 
    public GameObject Agent1, Agent2, Agent3, Book, Restorer1, Restorer2, Church, Pergamino1, Pergamino2, Computer, Creditos, Restauracion;
    


    // Start is called before the first frame update
    private void Start()
    {

        //Al iniciar la pantalla los paneles estan en false(ocultos)
        Agent1.SetActive(false);
        Agent2.SetActive(false);
        Agent3.SetActive(false);
        Book.SetActive(false);
        Restorer1.SetActive(false);
        Restorer2.SetActive(false);
        Church.SetActive(false);
        Pergamino1.SetActive(false);
        Pergamino2.SetActive(false);
        Computer.SetActive(false);
        Restauracion.SetActive(false);
        Creditos.SetActive(false);
        
    }


    //Esta funcion es la del boton de abrir y cerrar el Agent1
    public void switchAgent1()
    {
        
        if(gameP)
        {   
            QuitAgent1();
            
        }
        else
        {

            
            ShowAgent1();
            
        }
    }


    void QuitAgent1()
    {
        Agent1.SetActive(false);
       
        gameP = false;
    }


    void ShowAgent1()
    {
        
        Agent1.SetActive(true);
        
        gameP = true;
    }
    



////////////////////////////////////////////////////////////////

    //Esta funcion es la del boton de abrir y cerrar el Agent2
    
    public void switchAgent2()
    {
        if(gameP)
        {
            QuitAgent2();
        }
        else
        {
            ShowAgent2();
        }
    }


    void QuitAgent2()
    {
        Agent2.SetActive(false);
        
        gameP = false;
    }


    void ShowAgent2()
    {
        Agent2.SetActive(true);
        
        gameP = true;
    }
////////////////////////////////////////////////////////////////
    
    //Esta funcion es la del boton de abrir y cerrar el Agent3
    
    public void switchAgent3()
    {
        if(gameP)
        {
            QuitAgent3();
        }
        else
        {
            ShowAgent3();
        }
    }


    void QuitAgent3()
    {
        Agent3.SetActive(false);
        
        gameP = false;
    }


    void ShowAgent3()
    {
        Agent3.SetActive(true);
        
        gameP = true;
    }
////////////////////////////////////////////////////////////////
//Esta funcion es la del boton de abrir y cerrar el Book
    public void switchBook()
    {
        
        if(gameP)
        {   
            QuitBook();
            
        }
        else
        {

            
            ShowBook();
            
        }
    }


    void QuitBook()
    {
        Book.SetActive(false);
       
        gameP = false;
    }


    void ShowBook()
    {
        
        Book.SetActive(true);
        
        gameP = true;
    }
    



////////////////////////////////////////////////////////////////


//Esta funcion es la del boton de abrir y cerrar el Restorer1
    public void switchRestorer1()
    {
        
        if(gameP)
        {   
            QuitRestorer1();
            
        }
        else
        {

            
            ShowRestorer1();
            
        }
    }


    void QuitRestorer1()
    {
        Restorer1.SetActive(false);
       
        gameP = false;
    }


    void ShowRestorer1()
    {
        
        Restorer1.SetActive(true);
        
        gameP = true;
    }
    



////////////////////////////////////////////////////////////////
//Esta funcion es la del boton de abrir y cerrar el Restorer2
    public void switchRestorer2()
    {
        
        if(gameP)
        {   
            QuitRestorer2();
            
        }
        else
        {

            
            ShowRestorer2();
            
        }
    }


    void QuitRestorer2()
    {
        Restorer2.SetActive(false);
       
        gameP = false;
    }


    void ShowRestorer2()
    {
        
        Restorer2.SetActive(true);
        
        gameP = true;
    }
    



///////////////////////////////////////////////////////

//Esta funcion es la del boton de abrir y cerrar el Church
    public void switchChurch()
    {
        
        if(gameP)
        {   
            QuitChurch();
            
        }
        else
        {

            
            ShowChurch();
            
        }
    }


    void QuitChurch()
    {
        Church.SetActive(false);
       
        gameP = false;
    }


    void ShowChurch()
    {
        
        Church.SetActive(true);
        
        gameP = true;
    }
    



////////////////////////////////////////////////////////////////

//Esta funcion es la del boton de abrir y cerrar el Pergamino1
    
    public void switchPergamino1()
    {
        if(gameP)
        {
            QuitPergamino1();
        }
        else
        {
            ShowPergamino1();
        }
    }


    void QuitPergamino1()
    {
        Pergamino1.SetActive(false);
        
        gameP = false;
    }


    void ShowPergamino1()
    {
        Pergamino1.SetActive(true);
        
        gameP = true;
    }
////////////////////////////////////////////////////////////////
//Esta funcion es la del boton de abrir y cerrar el Pergamino1
    
    public void switchPergamino2()
    {
        if(gameP)
        {
            QuitPergamino2();
        }
        else
        {
            ShowPergamino2();
        }
    }


    void QuitPergamino2()
    {
        Pergamino2.SetActive(false);
        gameP = false;
    }


    void ShowPergamino2()
    {
        Pergamino2.SetActive(true);
        
        gameP = true;
    }

    
////////////////////////////////////////////////////////////////

//Esta funcion es la del boton de abrir y cerrar el Computador
    
    public void switchComputer()
    {
        if(gameP)
        {
            QuitComputer();
        }
        else
        {
            ShowComputer();
        }
    }


    void QuitComputer()
    {
        Computer.SetActive(false);
        
        gameP = false;
    }


    void ShowComputer()
    {
        Computer.SetActive(true);
        
        gameP = true;
    }
////////////////////////////////////////////////////////////////
    public void switchRestauracion()
    {
        
        if(gameP)
        {   
            
            ShowRestauracion();
        }
        else
        {

            QuitRestauracion();
            
            
        }
    }


    public void QuitRestauracion()
    {
        Restauracion.SetActive(false);
       
        gameP = true;
    }


    void ShowRestauracion()
    {
        
        Restauracion.SetActive(true);
        
        gameP = true;
    }

    ////////////////////////////////////////////////////////////////
    public void switchCreditos()
    {
        
        if(gameP)
        {   
            
            ShowCreditos();
        }
        else
        {

            QuitCreditos();
            
            
        }
    }


    public void QuitCreditos()
    {
        Creditos.SetActive(false);
       
        gameP = true;
    }


    void ShowCreditos()
    {
        
        Creditos.SetActive(true);
        
        gameP = true;
    }
    


}
    
