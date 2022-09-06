using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public GameObject fadeCameraObject;
    private void Start()
    {
        FadeOut();
    }

    public void ButtonLoadScene(string nameScene)
    {
        FadeIn();
        StartCoroutine(LoadNextScene(nameScene));
    }

    IEnumerator LoadNextScene(string scene)
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void FadeIn()
    {
        iTween.FadeTo(fadeCameraObject, iTween.Hash("alpha", 1f, "amount", 1f, "time", 1f));
    }

    public void FadeOut()
    {
        iTween.FadeTo(fadeCameraObject, iTween.Hash("alpha", 0f, "amount", 0f, "time", 1f));
    }
}
