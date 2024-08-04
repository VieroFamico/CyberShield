using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthManager : MonoBehaviour
{
    public static BaseHealthManager instance;

    public Animator losePanel;
    public Animator winPanel;
    public Button retryButton;
    public Button goBackToLevelSelectionButton;
    public HorizontalLayoutGroup healthDisplay;
    public List<GameObject> healthImages;

    public int HealthPoint = 5;
    private bool noMoreWaves = false;
    private void Awake()
    {
        instance = this;
        for(int i  = 0; i < healthDisplay.transform.childCount; i++)
        {
            healthImages.Add(healthDisplay.transform.GetChild(i).gameObject);
        }
        healthDisplay.enabled = false;

        retryButton.onClick.AddListener(SceneNavigation_Manager.instance.RetryScene);
        goBackToLevelSelectionButton.onClick.AddListener(SceneNavigation_Manager.instance.GoToLevelSelect);
    }
    private void Update()
    {
        if(noMoreWaves)
        {
            Base_Enemy enemies = FindAnyObjectByType<Base_Enemy>();

            if(enemies == null)
            {
                Win();
            }
        }
    }
    public void TakeDamage(int damage)
    {
        HealthPoint -= damage;
        Destroy(healthImages[HealthPoint]);

        if (HealthPoint <= 0)
        {
            Lose();
        }
    }

    public void EndingWaves()
    {
        noMoreWaves = true;
    }

    private void Lose()
    {
        losePanel.SetTrigger("Show");
        //Invoke("Pause", 1f);
    }

    
    public void Win()
    {
        winPanel.SetTrigger("Show");
        //Invoke("Pause", 1f);
    }

}
