using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation_Manager : MonoBehaviour
{
    public static SceneNavigation_Manager instance;

    public GameObject loadingScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void GoToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(GoToSceneWithAnimation(currentSceneIndex + 1));
    }
    public void GoPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(GoToSceneWithAnimation(currentSceneIndex - 1));
    }

    public void GoToLevel(int levelSceneIndex)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(GoToSceneWithAnimation(currentSceneIndex + levelSceneIndex));
    }

    public void GoToHomeScene()
    {
        StartCoroutine(GoToSceneWithAnimation(0));
    }

    private IEnumerator GoToSceneWithAnimation(int targetIndex)
    {
        loadingScreen.GetComponent<Animator>().SetTrigger("Show");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(targetIndex);

        yield return new WaitForSeconds(0.2f);
        loadingScreen.GetComponent<Animator>().SetTrigger("Hide");
    }

     
}
