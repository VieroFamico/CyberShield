using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        unlockedLevel++;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(unlockedLevelName, unlockedLevel);
    }
}
