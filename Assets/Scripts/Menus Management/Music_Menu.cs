using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Menu : MonoBehaviour
{   
    private Music_Menu instace;
    public AudioSource Music;
    public Music_Menu Instance
    {
        get
        {
            return instace;
        }




    }

    private void Awake(){
        if(FindObjectsOfType(GetType()).Length>1)
        {
            Destroy(gameObject);
        }

        if(instace!= null && instace!= this)
        {
            Destroy(gameObject);
            return;
        }


        else
        {
            instace=this;
        }


        DontDestroyOnLoad(gameObject);
    }
}
