using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideo : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;
    public Animator transitionAnim;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }
    void LoadScene(VideoPlayer vp)
    {
        StartCoroutine(LoadMenu());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(LoadMenu());
        }
    }

    IEnumerator LoadMenu()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneName);
    }
}
