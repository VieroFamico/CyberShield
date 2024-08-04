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
        Time.timeScale = 1f;
    }

    public void GoToHomeScene()
    {
        StartCoroutine(GoToSceneWithAnimation(0));
    }

    public void RetryScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(GoToSceneWithAnimation(currentSceneIndex));
    }
    public void GoToLevelSelect()
    {
        StartCoroutine(GoToSceneWithAnimation(1));
    }

    public void GoToEndingScene()
    {
        StartCoroutine(EndingScene());
    }


    private IEnumerator GoToSceneWithAnimation(int targetIndex)
    {
        loadingScreen.GetComponent<Animator>().SetTrigger("Show");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(targetIndex);

        yield return new WaitForSeconds(0.2f);
        loadingScreen.GetComponent<Animator>().SetTrigger("Hide");
    }

    public IEnumerator EndingScene()
    {
        GoToSceneWithAnimation(7);

        yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(10f);

        GoToSceneWithAnimation(0);
    }
     
}
