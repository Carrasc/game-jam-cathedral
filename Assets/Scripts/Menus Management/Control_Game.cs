using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_Game : MonoBehaviour
{
    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit_Game()
    {
        Application.Quit();
    }

    public void ReturnMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextScene());
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadSameScene());
    }

    IEnumerator LoadNextScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadSameScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadMainMenu()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
