using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int unlockedLevel = 0;
    public string unlockedLevelName = "UnlockedLevel";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        unlockedLevel = PlayerPrefs.GetInt(unlockedLevelName, 0);
    }

    public void UnlockNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;

        if(currentLevel == 4)
        {
            SceneNavigation_Manager.instance.GoToEndingScene();
        }

        if(unlockedLevel >= currentLevel)
        {
            return;
        }
        unlockedLevel = currentLevel;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(unlockedLevelName, unlockedLevel);
    }
}
