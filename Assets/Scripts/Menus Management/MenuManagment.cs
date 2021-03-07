using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagment : MonoBehaviour
{
    public Animator transitionAnim;

    public void Play()
    {
        
        StartCoroutine(LoadPlay());

    }



    public void Tutorial()
    {
        StartCoroutine(LoadTutorial());

    }

    

    public void Extra()
    {
        StartCoroutine(LoadExtra());

    }

    IEnumerator LoadPlay()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level1");
    }

    IEnumerator LoadExtra()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Oficina");
    }

    IEnumerator LoadTutorial()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Tutorial1");
    }



















}