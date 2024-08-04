using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Managers : MonoBehaviour
{
    [System.Serializable]
    public class LevelButton
    {
        public int level;
        public Button levelSelect;
    }

    public LevelButton[] levelButtons;
    public Button goToHomeButton;

    private void Awake()
    {
        foreach(LevelButton levelButton in levelButtons)
        {
            levelButton.levelSelect.onClick.AddListener(() => SelectLevel(levelButton.level));
            levelButton.levelSelect.enabled = false;
        }
        goToHomeButton.onClick.AddListener(SceneNavigation_Manager.instance.GoToHomeScene);
    }
    private void Start()
    {
        int unlockedLevel = GameManager.instance.unlockedLevel;
        for(int i = 0; i <= unlockedLevel; i++)
        {
            if (levelButtons[i] == null)
            {
                break;
            }
            levelButtons[i].levelSelect.enabled = true;
        }
    }

    public void SelectLevel(int level)
    {
        SceneNavigation_Manager.instance.GoToLevel(level + 1);
    }
}
